using Avalanche.Core.Domain.Common;

namespace Avalanche.Core.Domain.Entities
{
    public class Policy : AuditableBaseEntity
    {
        public string Number { get; set; }
        public DateOnly EffectiveStartDate { get; set; }
        public DateOnly EffectiveEndDate { get; set; }
        public Status Status { get; set; }
        public string StatusId { get; set; }
        public Client Client { get; set; }
        public string ClientId { get; set; }
        public Plan Plan { get; set; }
        public string PlanId { get; set; }
        public List<AffiliatePolicy> AffiliatePolicies { get; set; }
        public List<Authorization> Authorizations { get; set; }
    }
}
