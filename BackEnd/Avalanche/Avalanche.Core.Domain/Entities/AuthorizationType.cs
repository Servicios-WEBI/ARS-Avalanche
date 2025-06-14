using Avalanche.Core.Domain.Common;

namespace Avalanche.Core.Domain.Entities
{
    public class AuthorizationType : AuditableBaseEntity
    {
        public string Name { get; set; }
        public List<Authorization> Authorizations { get; set; }
    }
}
