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
        public List<GetCoveragesByIdPolicyQueryResponseChild> Coverages {  get; set; }
    }
    public class GetCoveragesByIdPolicyQueryResponseChild
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
