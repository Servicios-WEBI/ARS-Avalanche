using Avalanche.Core.Domain.Common;

namespace Avalanche.Core.Domain.Entities
{
    public class Hospital : AuditableBaseEntity
    {
        public string Name { get; set; }
        public InstitutionType InstitutionType { get; set; }
        public string InstitutionTypeId { get; set; }
        public Status Status { get; set; }
        public string StatusId { get; set; }
        public List<Authorization> Authorizations { get; set; }
    }
}
