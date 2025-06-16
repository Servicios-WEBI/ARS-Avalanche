using Swashbuckle.AspNetCore.Annotations;

namespace Avalanche.Core.Application.Features.Coverage.Queries.GetAll
{
    public class GetAllCoverageQueryResponse
    {
        [SwaggerSchema(Description = "Listado de coberturas")]
        public List<GetAllCoverageQueryResponseChild> Coverages { get; set; }
    }

    public class GetAllCoverageQueryResponseChild
    {
        [SwaggerSchema(Description = "Identificador único")]
        public string Id { get; set; }

        [SwaggerSchema(Description = "Nombre de la cobertura")]
        public string Name { get; set; }

        [SwaggerSchema(Description = "Descipción")]
        public string? Description { get; set; }
    }
}
