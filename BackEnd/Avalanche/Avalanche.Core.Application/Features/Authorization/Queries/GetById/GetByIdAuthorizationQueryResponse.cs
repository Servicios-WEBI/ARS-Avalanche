using Avalanche.Core.Application.Dtos.HospitalIntegration;
using Swashbuckle.AspNetCore.Annotations;

namespace Avalanche.Core.Application.Features.Authorization.Queries.GetById
{
    public class GetByIdAuthorizationQueryResponse
    {
        [SwaggerSchema(Description = "Autorización")]
        public GetByIdAuthorizationQueryResponseChild Authorization { get; set; }
    }

    public class GetByIdAuthorizationQueryResponseChild
    {
        [SwaggerSchema(Description = "Número de la solicitud.")]
        public string Id { get; set; }

        [SwaggerSchema(Description = "Nombre del afiliado")]
        public string Affiliate { get; set; }

        [SwaggerSchema(Description = "Id del afiliado")]
        public string AffiliateId { get; set; }

        [SwaggerSchema(Description = "Tipo de documento del afiliado.")]
        public string DocumentType { get; set; }

        [SwaggerSchema(Description = "Número del documento del afiliado.")]
        public string DocumentNumber { get; set; }

        [SwaggerSchema(Description = "Número de la póliza asociada a la solicitud.")]
        public string? PolicyNumber { get; set; }

        [SwaggerSchema(Description = "Id de la póliza asociada a la solicitud.")]
        public string? PolicyId { get; set; }

        [SwaggerSchema(Description = "Plan del afiliado")]
        public string Plan { get; set; }

        [SwaggerSchema(Description = "Tipo de autorización solicitada.")]
        public string AuthorizationType { get; set; }

        [SwaggerSchema(Description = "Id del tipo de autorización solicitada.")]
        public string AuthorizationTypeId { get; set; }

        [SwaggerSchema(Description = "Monto solicitado en la solicitud.")]
        public double ApplicationAmount { get; set; }

        [SwaggerSchema(Description = "Monto aprobado para la solicitud.")]
        public double? ApprovedAmount { get; set; }

        [SwaggerSchema(Description = "Estado actual de la solicitud.")]
        public string Status { get; set; }

        [SwaggerSchema(Description = "Id del estado actual de la solicitud.")]
        public string StatusId { get; set; }

        [SwaggerSchema(Description = "Fecha en que se realizó la solicitud de autorización.")]
        public DateOnly ApplicationDate { get; set; }

        [SwaggerSchema(Description = "Analista asignado")]
        public string AssignedAnalyst { get; set; }

        [SwaggerSchema(Description = "Id del analista asignado")]
        public string AssignedAnalystId { get; set; }

        [SwaggerSchema(Description = "Hospital que realizó la solicitud.")]
        public string Hospital { get; set; }

        [SwaggerSchema(Description = "Id del Hospital que realizó la solicitud.")]
        public string HospitalId { get; set; }
    }
}