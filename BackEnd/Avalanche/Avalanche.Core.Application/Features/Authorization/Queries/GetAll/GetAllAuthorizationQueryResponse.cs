using Swashbuckle.AspNetCore.Annotations;

namespace Avalanche.Core.Application.Features.Authorization.Queries.GetAll
{
    public class GetAllAuthorizationQueryResponse
    {
        [SwaggerSchema(Description = "Listado de autorizaciones")]
        public List<GetAllAuthorizationQueryResponseChild> Authorizations { get; set; }
    }

    public class GetAllAuthorizationQueryResponseChild
    {
        [SwaggerSchema(Description = "ID único de la autorización")]
        public string Id { get; set; }

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

        [SwaggerSchema(Description = "Analista asignado")]
        public string AssignedAnalyst { get; set; }

        [SwaggerSchema(Description = "Id del analista asignado")]
        public string AssignedAnalystId { get; set; }

        [SwaggerSchema(Description = "Hospital donde se realiza la solicitud")]
        public string Hospital { get; set; }
    }
}
