using Avalanche.Core.Domain.Common;

namespace Avalanche.Core.Domain.Entities
{
    public class InstitutionType : AuditableBaseEntity
    {
        public string Name { get; set; }
        public List<Hospital> Hospitals { get; set; }
    }
}
