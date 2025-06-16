using Swashbuckle.AspNetCore.Annotations;

namespace Avalanche.Core.Application.Features.Plan.Queries.GetAll
{
    public class GetAllPlanQueryResponse
    {
        [SwaggerSchema(Description = "Listado de planes")]
        public List<GetAllPlanQueryResponseChild> Plans { get; set; }
    }

    public class GetAllPlanQueryResponseChild
    {
        [SwaggerSchema(Description = "Identificador único")]
        public string Id { get; set; }

        [SwaggerSchema(Description = "Nombre del cine")]
        public string Name { get; set; }

        [SwaggerSchema(Description = "Descipción")]
        public string? Description { get; set; }

        [SwaggerSchema(Description = "Costo mensual")]
        public double MonthlyCost { get; set; }
    }
}
