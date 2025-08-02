using Swashbuckle.AspNetCore.Annotations;

namespace Avalanche.Core.Application.Features.HospitalIntegration.Queries.CheckAuthorization
{
    public class CheckAuthorizationQueryResponse
    {
        [SwaggerSchema(Description = "Objeto de solicitud")]
        public CheckAuthorizationQueryResponseChild Authorization { get; set; }
    }

    public class CheckAuthorizationQueryResponseChild
    {
        [SwaggerSchema(Description = "Número de solicitud")]
        public int Number { get; set; }

        [SwaggerSchema(Description = "Fecha de aplicación de autorización")]
        public DateOnly ApplicationDate { get; set; }

        [SwaggerSchema(Description = "Estado de la autorización")]
        public string Status { get; set; }

        [SwaggerSchema(Description = "Tipo de autorización")]
        public string AuthorizationType { get; set; }

        [SwaggerSchema(Description = "Monto solicitado")]
        public double ApplicationAmount { get; set; }

        [SwaggerSchema(Description = "Monto aprobado")]
        public double? ApprovedAmount { get; set; }

        [SwaggerSchema(Description = "Afiliado")]
        public string Affiliate { get; set; }

        [SwaggerSchema(Description = "Póliza")]
        public string Policy { get; set; }

        [SwaggerSchema(Description = "Hospital donde se realiza la solicitud")]
        public string Hospital { get; set; }
    }
}
