using Swashbuckle.AspNetCore.Annotations;

namespace Avalanche.Core.Application.Dtos.PlanCoverage
{
    public class PlanCoverageResponseDTO
    {
        [SwaggerSchema(Description = "Nombre de cobertura")]
        public string Name { get; set; }

        [SwaggerSchema(Description = "Descripción de cobertura")]
        public string? Description { get; set; }

        [SwaggerSchema(Description = "Monto limite de cobertura")]
        public double AmountLimit { get; set; }

        [SwaggerSchema(Description = "Limite anual de cobertura")]
        public int YearFrequencyLimit { get; set; }

        [SwaggerSchema(Description = "Porcentaje de cobertura")]
        public double CoveragePercentage { get; set; }
    }
}
