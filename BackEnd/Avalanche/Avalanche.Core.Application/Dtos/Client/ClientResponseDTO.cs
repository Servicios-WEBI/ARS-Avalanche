using Avalanche.Core.Application.Dtos.Common;
using Swashbuckle.AspNetCore.Annotations;

namespace Avalanche.Core.Application.Dtos.Client
{
    public class ClientResponseDTO
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

        [SwaggerSchema(Description = "Número de poliza")]
        public string PolicyNumber { get; set; }

        [SwaggerSchema(Description = "Estadp de poliza")]
        public string PolicyStatus { get; set; }

        [SwaggerSchema(Description = "Plan de poliza")]
        public string PolicyPlan { get; set; }

        [SwaggerSchema(Description = "Afiliados del cliente")]
        public List<AffiliatesResponseDTO> Affiliates { get; set; }
    }
}
