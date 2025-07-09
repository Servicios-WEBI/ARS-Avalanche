using AutoMapper;
using Avalanche.Core.Application.Dtos.Common;
using Avalanche.Core.Application.Dtos.Plan;
using Avalanche.Core.Application.Dtos.PlanCoverage;
using Avalanche.Core.Application.Interfaces.Repositories;
using Avalanche.Core.Domain.Entities;
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

        [SwaggerParameter(Description = "Coberturas")]
        [Required(ErrorMessage = "Debe de ingresar los datos de la cobertura")]
        public List<PlanCoverageDTO> Coverages { get; set; }
    }

    public class AddPlanCommandHandler : IRequestHandler<AddPlanCommand, PlanDTO>
    {
        private readonly IPlanRepository _planRepository;
        private readonly IPlanCoverageRepository _planCoverageRepository;
        private readonly IMapper _mapper;

        public AddPlanCommandHandler(IPlanRepository planRepository, IPlanCoverageRepository planCoverageRepository, IMapper mapper)
        {
            _planRepository = planRepository;
            _planCoverageRepository = planCoverageRepository;
            _mapper = mapper;
        }

        public async Task<PlanDTO> Handle(AddPlanCommand command, CancellationToken cancellationToken)
        {
            try
            {
                PlanDTO response = new();
                var valueToAdd = _mapper.Map<Domain.Entities.Plan>(command);
                var entity = await _planRepository.AddAsync(valueToAdd);

                var planCoverage = _mapper.Map<List<PlanCoverage>>(command.Coverages);

                try
                {
                    planCoverage.ForEach(p => p.PlanId = entity.Id);

                    var planCoverages = await _planCoverageRepository.AddManyAsync(planCoverage);
                }
                catch (Exception ex)
                {
                    throw new Exception("Hubo un error al relacionar el plan con las coberturas");
                }

                response = _mapper.Map<PlanDTO>(entity);
                response.Coverages = _mapper.Map<List<PlanCoverageDTO>>(planCoverage);
                response.Status = "Exitoso";
                response.Details = [new ErrorDetailsDTO { Code = "000", Message = "Se insertó correctamente el plan" }];
                return response;
                
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
