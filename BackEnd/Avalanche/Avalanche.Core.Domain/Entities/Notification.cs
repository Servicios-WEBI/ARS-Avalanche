using Avalanche.Core.Domain.Common;

namespace Avalanche.Core.Domain.Entities
{
    public class Notification : AuditableBaseEntity
    {
        public Authorization Authorization { get; set; }
        public string AuthorizationId { get; set; }
        public Analyst Analyst { get; set; }
        public string AssignedAnalyst { get; set; }
        public DateTime NotificationDate { get; set; }
    }
}
