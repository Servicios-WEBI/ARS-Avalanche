using Avalanche.Core.Application.Dtos.Common;

namespace Avalanche.Core.Application.Dtos.Authorization
{
    public class AuthorizationDTO : ErrorDTO
    {
        public string Id { get; set; }
        public DateOnly ApplicationDate { get; set; }
        public string Status { get; set; }
        public string AuthorizationType { get; set; }
        public double ApplicationAmount { get; set; }
        public double? ApprovedAmount { get; set; }
        public string Affiliate { get; set; }
        public string Policy { get; set; }
        public string Hospital { get; set; }
    }
}
