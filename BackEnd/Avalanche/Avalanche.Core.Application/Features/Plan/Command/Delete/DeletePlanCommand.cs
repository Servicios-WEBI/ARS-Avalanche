using AutoMapper;
using Avalanche.Core.Application.Constants;
using Avalanche.Core.Application.Dtos.Common;
using Avalanche.Core.Application.Dtos.Plan;
using Avalanche.Core.Application.Interfaces.Repositories;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Avalanche.Core.Application.Features.Plan.Command.Delete
{
    public class DeletePlanCommand : IRequest<PlanDTO>
    {
        [SwaggerParameter(Description = "Id")]
        [Required(ErrorMessage = "Debe de ingresar el identificador único")]
        public string Id { get; set; }
    }

    public class DeletePlanCommandHandler : IRequestHandler<DeletePlanCommand, PlanDTO>
    {
        private readonly IPlanRepository _planRepository;
        private readonly IMapper _mapper;

        public DeletePlanCommandHandler(IPlanRepository planRepository, IMapper mapper)
        {
            _planRepository = planRepository;
            _mapper = mapper;
        }

        public async Task<PlanDTO> Handle(DeletePlanCommand command, CancellationToken cancellationToken)
        {
            try
            {
                PlanDTO response = new();
                var valueToDelete = await _planRepository.GetByIdAsync(command.Id);

                if (valueToDelete == null)
                    throw new Exception(ErrorMessages.NotFound);

                await _planRepository.DeleteAsync(valueToDelete);

                response = _mapper.Map<PlanDTO>(valueToDelete);
                response.Status = "Exitoso";
                response.Details = [new ErrorDetailsDTO { Code = "000", Message = "Se eliminó correctamente el plan" }];
                return response;
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
