using Avalanche.Core.Application.Dtos.Common;

namespace Avalanche.Core.Application.Dtos.Authorization
{
    public class AuthorizationDTO : ErrorDTO
    {
        public string Id { get; set; }
        public DateOnly ApplicationDate { get; set; }
        public string StatusId { get; set; }
        public string AuthorizationTypeId { get; set; }
        public double ApplicationAmount { get; set; }
        public double? ApprovedAmount { get; set; }
        public string AffiliateId { get; set; }
        public string PolicyId { get; set; }
        public string HospitalId { get; set; }
    }
}
