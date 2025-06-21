using Swashbuckle.AspNetCore.Annotations;

namespace Avalanche.Core.Application.Features.Client.Queries.GetAll
{
    public class GetAllClientQueryResponse
    {
        [SwaggerSchema(Description = "Listado de clientes")]
        public List<GetAllClientQueryResponseChild> Clients { get; set; }
    }

    public class GetAllClientQueryResponseChild
    {
        [SwaggerSchema(Description = "Identificador único")]
        public string Id { get; set; }

        [SwaggerSchema(Description = "Primer nombre")]
        public string FirstName { get; set; }

        [SwaggerSchema(Description = "Segundo nombre")]
        public string? MiddleName { get; set; }

        [SwaggerSchema(Description = "Apellido")]
        public string LastName { get; set; }

        [SwaggerSchema(Description = "Tipo de documento")]
        public string DocumentType { get; set; }

        [SwaggerSchema(Description = "Número de documento")]
        public string DocumentNumber { get; set; }

        [SwaggerSchema(Description = "Número de telefono")]
        public string Phone { get; set; }

        [SwaggerSchema(Description = "Correo electrónico")]
        public string Email { get; set; }

        [SwaggerSchema(Description = "Dirección")]
        public string? Address { get; set; }

        [SwaggerSchema(Description = "Fecha de afiliación")]
        public DateOnly CustomerSince { get; set; }

        [SwaggerSchema(Description = "Estado")]
        public string Status { get; set; }
    }
}
