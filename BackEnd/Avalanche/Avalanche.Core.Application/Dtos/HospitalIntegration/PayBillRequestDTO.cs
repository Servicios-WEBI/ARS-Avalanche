using Avalanche.Core.Application.Constants;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Avalanche.Core.Application.Dtos.HospitalIntegration
{
    public class PayBillRequestDTO
    {
        [SwaggerSchema(Description = "Número de solicitud")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe de ingresar el número de solicitud")]
        public int AuthorizationNumber { get; set; }

        [SwaggerSchema(Description = "Monto aprobado de solicitud")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe de ingresar el monto aprobado de solicitud")]
        public double Amount { get; set; }
    }
}
