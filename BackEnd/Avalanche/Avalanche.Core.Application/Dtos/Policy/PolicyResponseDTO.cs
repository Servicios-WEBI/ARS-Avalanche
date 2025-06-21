using Avalanche.Core.Application.Dtos.Common;
using Swashbuckle.AspNetCore.Annotations;

namespace Avalanche.Core.Application.Dtos.Policy
{
    public class PolicyResponseDTO
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

        [SwaggerSchema(Description = "Número de documento del cliente")]
        public string ClientDocumentNumber { get; set; }

        [SwaggerSchema(Description = "Plan")]
        public string Plan { get; set; }

        [SwaggerSchema(Description = "Afiliados")]
        public List<AffiliatesResponseDTO> Affiliates { get; set; }
    }
}
