using Avalanche.Core.Domain.Common;

namespace Avalanche.Core.Domain.Entities
{
    public class AffiliatePolicy : AuditableBaseEntity
    {
        public bool IsPrincipal { get; set; }
        public DateOnly AffiliationDate { get; set; }
        public DateOnly? DesAffiliationDate { get; set; }
        public string Status { get; set; }
        public Affiliate Affiliate { get; set; }
        public string AffiliateId { get; set; }
        public Policy Policy { get; set; }
        public string PolicyId { get; set; }

        public AffiliatePolicy() {
            this.Id = "";
        }
    }
}
