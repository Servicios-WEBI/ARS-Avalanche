using Avalanche.Core.Domain.Common;

namespace Avalanche.Core.Domain.Entities
{
    public class Hospital : AuditableBaseEntity
    {
        public string Name { get; set; }
        public string InstitutionType { get; set; }
        public string Status { get; set; }
        public List<Authorization> Authorizations { get; set; }
        public Hospital() {
            this.Id = "";
        }
    }
}
