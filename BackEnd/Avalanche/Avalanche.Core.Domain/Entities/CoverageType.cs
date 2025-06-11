using Avalanche.Core.Domain.Common;

namespace Avalanche.Core.Domain.Entities
{
    public class CoverageType : AuditableBaseEntity
    {
        public string Name { get; set; }
        public List<Coverage> Coverages { get; set; }

        public CoverageType()
        {
            this.Id = "";
        }
    }
}
