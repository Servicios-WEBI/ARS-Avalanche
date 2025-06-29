using Swashbuckle.AspNetCore.Annotations;

namespace Avalanche.Core.Application.Features.Status.Queries.GetAll
{
    public class GetAllStatusQueryResponse
    {
        [SwaggerSchema(Description = "Listado de estados")]
        public List<GetAllStatusQueryResponseChild> Statuses { get; set; }
    }

    public class GetAllStatusQueryResponseChild
    {
        [SwaggerSchema(Description = "Identificador único")]
        public string Id { get; set; }

        [SwaggerSchema(Description = "Nombre del estado")]
        public string Name { get; set; }
    }
}
