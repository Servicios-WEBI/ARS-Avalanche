using Avalanche.Core.Application.Dtos.Common;

namespace Avalanche.Core.Application.Dtos.Affiliate
{
    public class AffiliatePolicyDTO : ErrorDTO
    {
        public string AffiliateId { get; set; }
        public string PolicyId { get; set; }
        public DateOnly AffiliationDate { get; set; }
        public string AffiliateStatus { get; set; }
        public bool IsPrincipal { get; set; }
    }
}
