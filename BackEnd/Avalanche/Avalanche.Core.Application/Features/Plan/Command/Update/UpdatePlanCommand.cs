using AutoMapper;
using Avalanche.Core.Application.Constants;
using Avalanche.Core.Application.Dtos.Common;
using Avalanche.Core.Application.Dtos.Plan;
using Avalanche.Core.Application.Interfaces.Repositories;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Avalanche.Core.Application.Features.Plan.Command.Update
{
    public class UpdatePlanCommand : IRequest<PlanDTO>
    {
        [SwaggerParameter(Description = "Id")]
        [Required(ErrorMessage = "Debe de ingresar el identificador único")]
        public string Id { get; set; }

        [SwaggerParameter(Description = "Nombre")]
        [Required(ErrorMessage = "Debe de ingresar el nombre del plan")]
        public string Name { get; set; }

        [SwaggerParameter(Description = "Descipción")]
        public string? Description { get; set; }

        [SwaggerParameter(Description = "Costo mensual")]
        [Required(ErrorMessage = "Debe de ingresar el costo mensual del plan")]
        public double MonthlyCost { get; set; }
    }

    public class UpdatePlanCommandHandler : IRequestHandler<UpdatePlanCommand, PlanDTO>
    {
        private readonly IPlanRepository _planRepository;
        private readonly IMapper _mapper;

        public UpdatePlanCommandHandler(IPlanRepository planRepository, IMapper mapper)
        {
            _planRepository = planRepository;
            _mapper = mapper;
        }

        public async Task<PlanDTO> Handle(UpdatePlanCommand command, CancellationToken cancellationToken)
        {
            try
            {
                PlanDTO response = new();

                var valueToUpdate = await _planRepository.GetByIdAsync(command.Id);

                if (valueToUpdate == null)
                    throw new Exception(ErrorMessages.NotFound);

                valueToUpdate.Name = command.Name;
                valueToUpdate.Description = command.Description;
                valueToUpdate.MonthlyCost = command.MonthlyCost;

                await _planRepository.UpdateAsync(valueToUpdate, valueToUpdate.Id);

                response = _mapper.Map<PlanDTO>(valueToUpdate);
                response.Status = "Exitoso";
                response.Details = [new ErrorDetailsDTO { Code = "000", Message = "Se modificó correctamente el plan" }];
                return response;
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
