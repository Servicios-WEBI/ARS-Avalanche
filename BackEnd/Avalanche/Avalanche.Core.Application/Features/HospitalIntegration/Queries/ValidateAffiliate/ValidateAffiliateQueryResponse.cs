using Avalanche.Core.Application.Dtos.Common;
using Avalanche.Core.Application.Dtos.PlanCoverage;
using Swashbuckle.AspNetCore.Annotations;

namespace Avalanche.Core.Application.Features.HospitalIntegration.Queries.ValidateAffiliate
{
    public class ValidateAffiliateQueryResponse : ErrorDTO
    {
        [SwaggerSchema(Description = "Nombre del Afiliado")]
        public bool Exists { get; set; }

        [SwaggerSchema(Description = "Nombre del Afiliado")]
        public string? Name { get; set; }

        [SwaggerSchema(Description = "Fecha de afiliación")]
        public DateOnly? AffiliateDate { get; set; }

        [SwaggerSchema(Description = "Número de la póliza")]
        public string? Number { get; set; }

        [SwaggerSchema(Description = "Estado de la póliza")]
        public string? PolicyStatus { get; set; }

        [SwaggerSchema(Description = "Plan de la póliza")]
        public string? Plan { get; set; }

        [SwaggerSchema(Description = "Coberturas")]
        public List<PlanCoverageResponseDTO>? Coverages {  get; set; }
    }
}
