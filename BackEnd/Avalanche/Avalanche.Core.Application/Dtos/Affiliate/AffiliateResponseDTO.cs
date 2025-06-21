using Swashbuckle.AspNetCore.Annotations;

namespace Avalanche.Core.Application.Dtos.Affiliate
{
    public class AffiliateResponseDTO
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

        [SwaggerSchema(Description = "Fecha de nacimiento")]
        public DateOnly BirthDate { get; set; }

        [SwaggerSchema(Description = "Género")]
        public string Gender { get; set; }

        [SwaggerSchema(Description = "Fecha de afiliación")]
        public DateOnly AffiliateDate { get; set; }

        [SwaggerSchema(Description = "Estado")]
        public string Status { get; set; }

        [SwaggerSchema(Description = "Es principal")]
        public bool? IsPrincipal { get; set; }

        [SwaggerSchema(Description = "Identificador único del cliente")]
        public string ClientId { get; set; }

        [SwaggerSchema(Description = "Nombre del cliente")]
        public string ClientName { get; set; }

        [SwaggerSchema(Description = "Poliza del afiliado")]
        public AffiliatePolicyResponseDTO Policy { get; set; }
    }
}
