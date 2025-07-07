using AutoMapper;
using Avalanche.Core.Application.Dtos.Account;
using Avalanche.Core.Application.Dtos.Common;
using Avalanche.Core.Application.Dtos.Hospital;
using Avalanche.Core.Application.Interfaces.Repositories;
using Avalanche.Core.Application.Interfaces.Services;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Avalanche.Core.Application.Features.Hospital.Command.Add
{
    public class AddHospitalCommand : IRequest<HospitalDTO>
    {
        [SwaggerSchema(Description = "Nombre del hospital")]
        [Required(ErrorMessage = "Debe ingresar el nombre del hospital")]
        public string Name { get; set; }

        [SwaggerParameter(Description = "Correo")]
        [Required(ErrorMessage = "Debe de ingresar el correo")]
        [EmailAddress(ErrorMessage = "Debe ingresar un correo electrónico válido")]
        public string Email { get; set; }

        [SwaggerSchema(Description = "Tipo de institución")]
        [Required(ErrorMessage = "Debe ingresar el tipo de institución")]
        public string InstitutionTypeId { get; set; }

        [SwaggerSchema(Description = "Estado de la institución")]
        [Required(ErrorMessage = "Debe ingresar el estado de la institución")]
        public string StatusId { get; set; }
    }

    public class AddHospitalCommandHandler : IRequestHandler<AddHospitalCommand, HospitalDTO>
    {
        private readonly IHospitalRepository _hospitalRepository;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AddHospitalCommandHandler(IHospitalRepository hospitalRepository, IAccountService accountService, IMapper mapper)
        {
            _hospitalRepository = hospitalRepository;
            _accountService = accountService;
            _mapper = mapper;
        }

        public async Task<HospitalDTO> Handle(AddHospitalCommand command, CancellationToken cancellationToken)
        {
            try
            {
                HospitalDTO response = new();
                command.Name = command.Name.ToUpper();
                var valueToAdd = _mapper.Map<Domain.Entities.Hospital>(command);
                var entity = await _hospitalRepository.AddAsync(valueToAdd);

                RegisterRequest register = new()
                {
                    FirstName = command.Name,
                    LastName = command.Name,
                    Address = "",
                    Email = command.Email,
                    PhoneNumber = "",
                    UrlImage = "",
                    UserName = "usr_" + Guid.NewGuid().ToString("N").Substring(0, 8),
                    Password = "ARS@" + Guid.NewGuid().ToString().Substring(0, 8)
                };

                try
                {
                    var registerResponse = await _accountService.RegisterUserAsync(register, Enums.Roles.Guest);
                    if (registerResponse.Status == "Fallido")
                    {
                        throw new Exception("Hubo un error al crear el usuario del hospital");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Hubo un error al crear el usuario del hospital");
                }

                response = _mapper.Map<HospitalDTO>(entity);
                response.UserName = register.UserName;
                response.Status = "Exitoso";
                response.Details = [new ErrorDetailsDTO { Code = "000", Message = "Se insertó correctamente el hospital" }];
                return response;
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
