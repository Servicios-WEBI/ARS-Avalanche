using AutoMapper;
using Avalanche.Core.Application.Constants;
using Avalanche.Core.Application.Dtos.Common;
using Avalanche.Core.Application.Dtos.Policy;
using Avalanche.Core.Application.Helpers;
using Avalanche.Core.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Avalanche.Core.Application.Features.Policy.Command.Update
{
    public class UpdatePolicyCommand : IRequest<PolicyDTO>
    {
        [SwaggerParameter(Description = "Id")]
        [Required(ErrorMessage = "Debe de ingresar el identificador único")]
        public string Id { get; set; }

        [SwaggerParameter(Description = "Plan")]
        [Required(ErrorMessage = "Debe de ingresar el plan")]
        public string PlanId { get; set; }

        [SwaggerParameter(Description = "Fecha de fin de vigencia")]
        public DateOnly? EffectiveEndDate { get; set; }

        [SwaggerParameter(Description = "Estado")]
        [Required(ErrorMessage = "Debe de ingresar el estado")]
        public string StatusId { get; set; }
    }

    public class UpdatePolicyCommandHandler : IRequestHandler<UpdatePolicyCommand, PolicyDTO>
    {
        private readonly IPolicyRepository _policyRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly IMapper _mapper;

        public UpdatePolicyCommandHandler(IPolicyRepository policyRepository, IStatusRepository statusRepository, IMapper mapper)
        {
            _policyRepository = policyRepository;
            _statusRepository = statusRepository;
            _mapper = mapper;
        }

        public async Task<PolicyDTO> Handle(UpdatePolicyCommand command, CancellationToken cancellationToken)
        {
            try
            {
                PolicyDTO response = new();

                var valueToUpdate = await _policyRepository.GetByIdAsync(command.Id);

                if (valueToUpdate == null)
                    throw new Exception(ErrorMessages.NotFound);

                var status = await _statusRepository.GetByIdAsync(command.StatusId);

                valueToUpdate.PlanId = command.PlanId;
                valueToUpdate.StatusId = status.Id;
                valueToUpdate.EffectiveEndDate = command.EffectiveEndDate;

                await _policyRepository.UpdateAsync(valueToUpdate, valueToUpdate.Id);

                response = _mapper.Map<PolicyDTO>(valueToUpdate);
                response.PolicyStatus = status.Name;
                response.Status = "Exitoso";
                response.Details = [new ErrorDetailsDTO { Code = "000", Message = "Se modificó correctamente la poliza" }];
                return response;
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
