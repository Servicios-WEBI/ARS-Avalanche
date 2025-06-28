using Avalanche.Core.Application.Dtos.PlanCoverage;
using Swashbuckle.AspNetCore.Annotations;

namespace Avalanche.Core.Application.Features.Plan.Queries.GetCoveragesById
{
    public class GetCoveragesByIdPlanQueryResponse
    {
        [SwaggerSchema(Description = "Identificador único")]
        public string Id { get; set; }

        [SwaggerSchema(Description = "Nombre del plan")]
        public string Name { get; set; }

        [SwaggerSchema(Description = "Descipción")]
        public string? Description { get; set; }

        [SwaggerSchema(Description = "Costo mensual")]
        public double MonthlyCost { get; set; }

        [SwaggerSchema(Description = "Coberturas")]
        public List<PlanCoverageResponseDTO> Coverages {  get; set; }
    }
}
