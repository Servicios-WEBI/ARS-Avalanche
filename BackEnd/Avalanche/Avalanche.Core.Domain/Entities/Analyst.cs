using Avalanche.Core.Domain.Common;

namespace Avalanche.Core.Domain.Entities
{
    public class Analyst : AuditableBaseEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public List<Authorization> Authorizations { get; set; }
        public List<Notification> Notifications { get; set; }
    }
}
