using AutoMapper;
using Avalanche.Core.Application.Dtos.Common;
using Avalanche.Core.Application.Dtos.Plan;
using Avalanche.Core.Application.Interfaces.Repositories;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Avalanche.Core.Application.Features.Plan.Command.Add
{
    public class AddPlanCommand : IRequest<PlanDTO>
    {
        [SwaggerParameter(Description = "Nombre")]
        [Required(ErrorMessage = "Debe de ingresar el nombre del plan")]
        public string Name { get; set; }

        [SwaggerParameter(Description = "Descipción")]
        public string? Description { get; set; }

        [SwaggerParameter(Description = "Costo mensual")]
        [Required(ErrorMessage = "Debe de ingresar el costo mensual del plan")]
        public double MonthlyCost { get; set; }
    }

    public class AddPlanCommandHandler : IRequestHandler<AddPlanCommand, PlanDTO>
    {
        private readonly IPlanRepository _planRepository;
        private readonly IMapper _mapper;

        public AddPlanCommandHandler(IPlanRepository planRepository, IMapper mapper)
        {
            _planRepository = planRepository;
            _mapper = mapper;
        }

        public async Task<PlanDTO> Handle(AddPlanCommand command, CancellationToken cancellationToken)
        {
            try
            {
                PlanDTO response = new();
                var valueToAdd = _mapper.Map<Domain.Entities.Plan>(command);
                var entity = await _planRepository.AddAsync(valueToAdd);

                response = _mapper.Map<PlanDTO>(entity);
                response.Status = "Exitoso";
                response.Details = [new ErrorDetailsDTO { Code = "000", Message = "Se insertó correctamente el plan" }];
                return response;
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
