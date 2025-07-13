using Avalanche.Core.Application.Dtos.Common;
using Swashbuckle.AspNetCore.Annotations;

namespace Avalanche.Core.Application.Dtos.HospitalIntegration
{
    public class AuthorizationResponseDTO : ErrorDTO
    {
        [SwaggerSchema(Description = "Número de la solicitud.")]
        public string Id { get; set; }

        [SwaggerSchema(Description = "Tipo de documento del afiliado.")]
        public string DocumentType { get; set; }

        [SwaggerSchema(Description = "Número del documento del afiliado.")]
        public string DocumentNumber { get; set; }

        [SwaggerSchema(Description = "Número de la póliza asociada a la solicitud.")]
        public string? PolicyNumber { get; set; }

        [SwaggerSchema(Description = "Tipo de autorización solicitada.")]
        public string AuthorizationType { get; set; }

        [SwaggerSchema(Description = "Monto solicitado en la solicitud.")]
        public double ApplicationAmount { get; set; }

        [SwaggerSchema(Description = "Monto aprobado para la solicitud.")]
        public double? ApprovedAmount { get; set; }

        [SwaggerSchema(Description = "Estado actual de la solicitud.")]
        public string AuthorizationStatus { get; set; }

        [SwaggerSchema(Description = "Fecha en que se realizó la solicitud de autorización.")]
        public DateOnly ApplicationDate { get; set; }

        [SwaggerSchema(Description = "Hospital que realizó la solicitud.")]
        public string Hospital { get; set; }
    }
}
