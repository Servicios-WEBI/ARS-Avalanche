using Swashbuckle.AspNetCore.Annotations;

namespace Avalanche.Core.Application.Features.Policy.Queries.GetAll
{
    public class GetAllPolicyQueryResponse
    {
        [SwaggerSchema(Description = "Listado de polizas")]
        public List<GetAllPolicyQueryResponseChild> Policies { get; set; }
    }

    public class GetAllPolicyQueryResponseChild
    {
        [SwaggerSchema(Description = "Identificador único")]
        public string Id { get; set; }

        [SwaggerSchema(Description = "Número de la póliza")]
        public string Number { get; set; }

        [SwaggerSchema(Description = "Fecha de inicio de vigencia")]
        public DateOnly EffectiveStartDate { get; set; }

        [SwaggerSchema(Description = "Fecha de fin de vigencia")]
        public DateOnly? EffectiveEndDate { get; set; }

        [SwaggerSchema(Description = "Estado actual")]
        public string Status { get; set; }

        [SwaggerSchema(Description = "Cliente")]
        public string Client { get; set; }

        [SwaggerSchema(Description = "Plan")]
        public string Plan { get; set; }

    }
}
