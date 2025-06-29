using Avalanche.Core.Application.Dtos.PlanCoverage;
using Swashbuckle.AspNetCore.Annotations;

namespace Avalanche.Core.Application.Features.Policy.Queries.GetCoveragesById
{
    public class GetCoveragesByIdPolicyQueryResponse
    {
        [SwaggerSchema(Description = "Identificador único")]
        public string Id { get; set; }

        [SwaggerSchema(Description = "Número de la póliza")]
        public string Number { get; set; }

        [SwaggerSchema(Description = "Plan de la póliza")]
        public string Plan { get; set; }

        [SwaggerSchema(Description = "Coberturas")]
        public List<PlanCoverageResponseDTO> Coverages {  get; set; }
    }
}
