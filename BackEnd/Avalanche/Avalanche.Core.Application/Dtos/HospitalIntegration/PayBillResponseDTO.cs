using Avalanche.Core.Application.Dtos.Common;
using Swashbuckle.AspNetCore.Annotations;

namespace Avalanche.Core.Application.Dtos.HospitalIntegration
{
    public class PayBillResponseDTO : ErrorDTO
    {
        [SwaggerSchema(Description = "Número de transferencia")]
        public string? TransferenceId { get; set; }

        [SwaggerSchema(Description = "Monto total solicitado")]
        public double TotalAmount { get; set; }

        [SwaggerSchema(Description = "Monto pagado")]
        public double PaidAmount { get; set; }

        [SwaggerSchema(Description = "Monto aprobado de solicitud")]
        public double RefusedAmount { get; set; }

        [SwaggerSchema(Description = "Listado de recibos")]
        public List<BillDTO>? Bills { get; set; }
    }

    public class BillDTO
    {
        [SwaggerSchema(Description = "Número de solicitud")]
        public string AuthorizationNumber { get; set; }

        [SwaggerSchema(Description = "Estado de recibo")]
        public string Status { get; set; }

        [SwaggerSchema(Description = "Detalle de decisión")]
        public string Details { get; set; }
    }
}
