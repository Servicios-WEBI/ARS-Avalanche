using AutoMapper;
using Avalanche.Core.Application.Constants;
using Avalanche.Core.Application.Dtos.Common;
using Avalanche.Core.Application.Dtos.Authorization;
using Avalanche.Core.Application.Interfaces.Repositories;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Avalanche.Core.Application.Features.Authorization.Command.Update
{
    public class UpdateAuthorizationCommand : IRequest<AuthorizationDTO>
    {
        [SwaggerParameter(Description = "Id")]
        [Required(ErrorMessage = "Debe de ingresar el identificador único")]
        public string Id { get; set; }

        [SwaggerParameter(Description = "Identificador del estado de la autorización")]
        [Required(ErrorMessage = "Debe ingresar el estado de la autorización")]
        public string StatusId { get; set; }

        [SwaggerParameter(Description = "Identificador del tipo de autorización")]
        [Required(ErrorMessage = "Debe ingresar el tipo de autorización")]
        public string AuthorizationTypeId { get; set; }

        [SwaggerParameter(Description = "Monto solicitado en la aplicación")]
        [Required(ErrorMessage = "Debe ingresar el monto solicitado")]
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

    public class UpdateAuthorizationCommandHandler : IRequestHandler<UpdateAuthorizationCommand, AuthorizationDTO>
    {
        private readonly IAuthorizationRepository _authorizationRepository;
        private readonly IMapper _mapper;

        public UpdateAuthorizationCommandHandler(IAuthorizationRepository authorizationRepository, IMapper mapper)
        {
            _authorizationRepository = authorizationRepository;
            _mapper = mapper;
        }

        public async Task<AuthorizationDTO> Handle(UpdateAuthorizationCommand command, CancellationToken cancellationToken)
        {
            try
            {
                AuthorizationDTO response = new();

                var valueToUpdate = await _authorizationRepository.GetByIdAsync(command.Id);

                if (valueToUpdate == null)
                    throw new Exception(ErrorMessages.NotFound);

                valueToUpdate.StatusId = command.StatusId;
                valueToUpdate.AuthorizationTypeId = command.AuthorizationTypeId;
                valueToUpdate.ApplicationAmount = command.ApplicationAmount;
                valueToUpdate.ApprovedAmount = command.ApprovedAmount;
                valueToUpdate.AffiliateId = command.AffiliateId;
                valueToUpdate.PolicyId = command.PolicyId;
                valueToUpdate.HospitalId = command.HospitalId;

                await _authorizationRepository.UpdateAsync(valueToUpdate, valueToUpdate.Id);

                response = _mapper.Map<AuthorizationDTO>(valueToUpdate);
                response.Status = "Exitoso";
                response.Details = [new ErrorDetailsDTO { Code = "000", Message = "Se modificó correctamente la autorización" }];
                return response;
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
