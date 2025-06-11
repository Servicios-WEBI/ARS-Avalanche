using Avalanche.Core.Domain.Common;

namespace Avalanche.Core.Domain.Entities
{
    public class Affiliate : AuditableBaseEntity
    {
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public DocumentType DocumentType { get; set; }
        public string DocumentTypeId { get; set; }
        public string DocumentNumber { get; set; }
        public string BirthDate { get; set; }
        public string Gender { get; set; }
        public DateOnly AffiliateDate { get; set; }
        public Status Status { get; set; }
        public string StatusId { get; set; }
        public Client Client { get; set; }
        public string ClientId { get; set; }
        public List<AffiliatePolicy> AffiliatePolicies { get; set; }
        public List<Authorization> Authorizations { get; set; }

        public Affiliate() {
            this.Id = "";
        }
    }
}
