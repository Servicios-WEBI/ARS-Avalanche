using Swashbuckle.AspNetCore.Annotations;

namespace Avalanche.Core.Application.Dtos.HospitalIntegration
{
    public class PayBillRequestDTO
    {
        [SwaggerSchema(Description = "Número de solicitud")]
        public string AuthorizationNumber { get; set; }

        [SwaggerSchema(Description = "Monto aprobado de solicitud")]
        public double Amount { get; set; }
    }
}
