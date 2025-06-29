using AutoMapper;
using Avalanche.Core.Application.Dtos.Common;
using Avalanche.Core.Application.Dtos.Authorization;
using Avalanche.Core.Application.Interfaces.Repositories;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Avalanche.Core.Application.Features.Authorization.Command.Add
{
    public class AddAuthorizationCommand : IRequest<AuthorizationDTO>
    {
        [SwaggerParameter(Description = "Identificador del estado de la autorización")]
        [Required(ErrorMessage = "Debe ingresar el estado de la autorización.")]
        public string StatusId { get; set; }

        [SwaggerParameter(Description = "Identificador del tipo de autorización")]
        [Required(ErrorMessage = "Debe ingresar el tipo de autorización.")]
        public string AuthorizationTypeId { get; set; }

        [SwaggerParameter(Description = "Monto solicitado en la aplicación")]
        [Required(ErrorMessage = "Debe ingresar el monto solicitado.")]
        public double ApplicationAmount { get; set; }

        [SwaggerParameter(Description = "Monto aprobado para la solicitud")]
        public double? ApprovedAmount { get; set; }

        [SwaggerParameter(Description = "Identificador del afiliado")]
        [Required(ErrorMessage = "Debe ingresar el afiliado")]
        public string AffiliateId { get; set; }

        [SwaggerParameter(Description = "Identificador de la póliza asociada")]
        [Required(ErrorMessage = "Debe ingresar la póliza")]
        public string PolicyId { get; set; }

        [SwaggerParameter(Description = "Identificador del hospital donde se realiza la solicitud")]
        [Required(ErrorMessage = "Debe ingresar el hospital")]
        public string HospitalId { get; set; }
    }

    public class AddAuthorizationCommandHandler : IRequestHandler<AddAuthorizationCommand, AuthorizationDTO>
    {
        private readonly IAuthorizationRepository _authorizationRepository;
        private readonly IMapper _mapper;

        public AddAuthorizationCommandHandler(IAuthorizationRepository authorizationRepository, IMapper mapper)
        {
            _authorizationRepository = authorizationRepository;
            _mapper = mapper;
        }

        public async Task<AuthorizationDTO> Handle(AddAuthorizationCommand command, CancellationToken cancellationToken)
        {
            try
            {
                AuthorizationDTO response = new();
                var valueToAdd = _mapper.Map<Domain.Entities.Authorization>(command);
                valueToAdd.ApplicationDate = DateOnly.FromDateTime(DateTime.UtcNow);

                var entity = await _authorizationRepository.AddAsync(valueToAdd);

                response = _mapper.Map<AuthorizationDTO>(entity);
                response.Status = "Exitoso";
                response.Details = [new ErrorDetailsDTO { Code = "000", Message = "Se insertó correctamente la autorización" }];
                return response;
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
