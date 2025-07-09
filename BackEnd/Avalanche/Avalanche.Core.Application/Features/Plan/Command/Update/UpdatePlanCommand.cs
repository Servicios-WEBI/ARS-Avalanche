using AutoMapper;
using Avalanche.Core.Application.Constants;
using Avalanche.Core.Application.Dtos.Common;
using Avalanche.Core.Application.Dtos.Plan;
using Avalanche.Core.Application.Dtos.PlanCoverage;
using Avalanche.Core.Application.Interfaces.Repositories;
using Avalanche.Core.Application.Interfaces.Services;
using Avalanche.Core.Domain.Entities;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

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

        [SwaggerParameter(Description = "Coberturas")]
        [Required(ErrorMessage = "Debe de ingresar los datos de la cobertura")]
        public List<PlanCoverageDTO> Coverages { get; set; }
    }

    public class UpdatePlanCommandHandler : IRequestHandler<UpdatePlanCommand, PlanDTO>
    {
        private readonly IPlanRepository _planRepository;
        private readonly IPlanCoverageRepository _planCoverageRepository;
        private readonly IPlanCoverageComparisonService _comparisonService;
        private readonly IMapper _mapper;

        public UpdatePlanCommandHandler(IPlanRepository planRepository, IPlanCoverageRepository planCoverageRepository,
            IPlanCoverageComparisonService comparisonService, IMapper mapper)
        {
            _planRepository = planRepository;
            _planCoverageRepository = planCoverageRepository;
            _comparisonService = comparisonService;
            _mapper = mapper;
        }

        public async Task<PlanDTO> Handle(UpdatePlanCommand command, CancellationToken cancellationToken)
        {
            try
            {
                PlanDTO response = new();

                var valueToUpdate = await _planRepository.GetByIdWithIncludeAsync(p => p.Id == command.Id, new List<Expression<Func<Domain.Entities.Plan, object>>>
                {
                    m => m.PlanCoverages
                });

                if (valueToUpdate == null)
                    throw new Exception(ErrorMessages.NotFound);

                valueToUpdate.Name = command.Name;
                valueToUpdate.Description = command.Description;
                valueToUpdate.MonthlyCost = command.MonthlyCost;

                await _planRepository.UpdateAsync(valueToUpdate, valueToUpdate.Id);

                var planCoverage = _mapper.Map<List<PlanCoverage>>(command.Coverages);

                try
                {
                    planCoverage.ForEach(p => p.PlanId = valueToUpdate.Id);

                    if (!_comparisonService.AreListsEqual(valueToUpdate.PlanCoverages, command.Coverages))
                    {
                        await _planCoverageRepository.DeleteManyAsync(valueToUpdate.PlanCoverages);
                        await _planCoverageRepository.AddManyAsync(planCoverage);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Hubo un error al actualizar el plan con las coberturas");
                }

                response = _mapper.Map<PlanDTO>(valueToUpdate);
                response.Coverages = _mapper.Map<List<PlanCoverageDTO>>(planCoverage);
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
