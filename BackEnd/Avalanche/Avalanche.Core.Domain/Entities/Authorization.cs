using Avalanche.Core.Domain.Common;

namespace Avalanche.Core.Domain.Entities
{
    public class Authorization : AuditableBaseEntity
    {
        public DateOnly ApplicationDate { get; set; }
        public Status Status { get; set; }
        public string StatusId { get; set; }
        public AuthorizationType AuthorizationType { get; set; }
        public string AuthorizationTypeId { get; set; }
        public double ApplicationAmount { get; set; }
        public double? ApprovedAmount { get; set; }
        public Affiliate Affiliate { get; set; }
        public string AffiliateId { get; set; }
        public Policy Policy { get; set; }
        public string PolicyId { get; set; }
        public Hospital Hospital { get; set; }
        public string HospitalId { get; set; }
        public Analyst Analyst { get; set; }
        public string AssignedAnalyst { get; set; }
        public List<Notification> Notifications { get; set; }
    }
}
