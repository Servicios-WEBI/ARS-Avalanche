using Avalanche.Core.Domain.Common;

namespace Avalanche.Core.Domain.Entities
{
    public class Status : AuditableBaseEntity
    {
        public string Name { get; set; }
        public List<Affiliate> Affiliates { get; set; }
        public List<AffiliatePolicy> AffiliatePolicies { get; set; }
        public List<Authorization> Authorizations { get; set; }
        public List<Client> Clients { get; set; }
        public List<Policy> Policies { get; set; }
        public List<Hospital> Hospitals { get; set; }

        public Status()
        {
            this.Id = "";
        }
    }
}
