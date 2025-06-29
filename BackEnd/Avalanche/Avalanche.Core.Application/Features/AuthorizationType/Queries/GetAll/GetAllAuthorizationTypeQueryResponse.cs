using Swashbuckle.AspNetCore.Annotations;

namespace Avalanche.Core.Application.Features.AuthorizationType.Queries.GetAll
{
    public class GetAllAuthorizationTypeQueryResponse
    {
        [SwaggerSchema(Description = "Listado de tipos de autorizaciones")]
        public List<GetAllAuthorizationTypeQueryResponseChild> AuthorizationTypes { get; set; }
    }

    public class GetAllAuthorizationTypeQueryResponseChild
    {
        [SwaggerSchema(Description = "Identificador único")]
        public string Id { get; set; }

        [SwaggerSchema(Description = "Nombre del tipo de autorización")]
        public string Name { get; set; }
    }
}
