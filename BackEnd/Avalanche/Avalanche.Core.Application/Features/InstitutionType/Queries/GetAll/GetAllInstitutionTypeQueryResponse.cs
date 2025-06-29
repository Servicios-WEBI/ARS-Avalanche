using Swashbuckle.AspNetCore.Annotations;

namespace Avalanche.Core.Application.Features.InstitutionType.Queries.GetAll
{
    public class GetAllInstitutionTypeQueryResponse
    {
        [SwaggerSchema(Description = "Listado de tipos de institución")]
        public List<GetAllInstitutionTypeQueryResponseChild> InstitutionTypes { get; set; }
    }

    public class GetAllInstitutionTypeQueryResponseChild
    {
        [SwaggerSchema(Description = "Identificador único")]
        public string Id { get; set; }

        [SwaggerSchema(Description = "Nombre del tipo de institución")]
        public string Name { get; set; }
    }
}
