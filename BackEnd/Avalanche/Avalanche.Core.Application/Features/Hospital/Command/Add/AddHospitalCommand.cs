using AutoMapper;
using Avalanche.Core.Application.Dtos.Account;
using Avalanche.Core.Application.Dtos.Common;
using Avalanche.Core.Application.Dtos.Email;
using Avalanche.Core.Application.Dtos.Hospital;
using Avalanche.Core.Application.Interfaces.Helpers;
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
    }

    public class AddHospitalCommandHandler : IRequestHandler<AddHospitalCommand, HospitalDTO>
    {
        private readonly IHospitalRepository _hospitalRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly IAccountService _accountService;
        private readonly IEmailService _emailService;
        private readonly IEmailHelper _emailHelper;
        private readonly IMapper _mapper;

        public AddHospitalCommandHandler(IHospitalRepository hospitalRepository, IAccountService accountService, IStatusRepository statusRepository,
            IEmailService emailService, IEmailHelper emailHelper, IMapper mapper)
        {
            _hospitalRepository = hospitalRepository;
            _statusRepository = statusRepository;
            _accountService = accountService;
            _emailService = emailService;
            _emailHelper = emailHelper;
            _mapper = mapper;
        }

        public async Task<HospitalDTO> Handle(AddHospitalCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var active = await _statusRepository.GetByPropertyAsync(s => s.Name == "Activo");

                HospitalDTO response = new();
                command.Name = command.Name.ToUpper();

                RegisterRequest register = new()
                {
                    FirstName = command.Name,
                    LastName = "",
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
                        response.Status = registerResponse.Status;
                        response.Details = registerResponse.Details;
                        return response;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Hubo un error al crear el usuario del hospital");
                }

                var valueToAdd = _mapper.Map<Domain.Entities.Hospital>(command);
                valueToAdd.StatusId = active.Id;

                var entity = await _hospitalRepository.AddAsync(valueToAdd);

                try
                {
                    UserWelcomeEmail dto = new()
                    {
                        FullName = entity.Name,
                        UserName = register.UserName,
                        Password = register.Password
                    };

                    await _emailService.SendAsync(new EmailRequest()
                    {
                        To = entity.Email,
                        Body = _emailHelper.MakeEmailForHospital(dto),
                        Subject = "¡Bienvenido al sistema Avalanche!"
                    });
                }
                catch (Exception ex)
                {
                    throw new Exception("Hubo un error enviando el correo al hospital");
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
