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
        public string Status { get; set; }

        [SwaggerParameter(Description = "Identificador del tipo de autorización")]
        [Required(ErrorMessage = "Debe ingresar el tipo de autorización")]
        public string AuthorizationType { get; set; }

        [SwaggerParameter(Description = "Monto solicitado en la aplicación")]
        [Required(ErrorMessage = "Debe ingresar el monto solicitado")]
        public double ApplicationAmount { get; set; }

        [SwaggerParameter(Description = "Monto aprobado para la solicitud")]
        public double? ApprovedAmount { get; set; }

        [SwaggerParameter(Description = "Identificador del afiliado")]
        [Required(ErrorMessage = "Debe ingresar el afiliado")]
        public string Affiliate { get; set; }

        [SwaggerParameter(Description = "Identificador de la póliza asociada")]
        [Required(ErrorMessage = "Debe ingresar la póliza")]
        public string Policy { get; set; }

        [SwaggerParameter(Description = "Identificador del hospital donde se realiza la solicitud")]
        [Required(ErrorMessage = "Debe ingresar el hospital")]
        public string Hospital { get; set; }
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

                valueToUpdate.StatusId = command.Status;
                valueToUpdate.AuthorizationTypeId = command.AuthorizationType;
                valueToUpdate.ApplicationAmount = command.ApplicationAmount;
                valueToUpdate.ApprovedAmount = command.ApprovedAmount;
                valueToUpdate.AffiliateId = command.Affiliate;
                valueToUpdate.PolicyId = command.Policy;
                valueToUpdate.HospitalId = command.Hospital;

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
