using Avalanche.Core.Domain.Common;

namespace Avalanche.Core.Domain.Entities
{
    public class Coverage : AuditableBaseEntity
    {
        public string Name { get; set; }
        public CoverageType CoverageType { get; set; }
        public string CoverageTypeId { get; set; }
        public string? Description { get; set; }
        public List<PlanCoverage> PlanCoverages { get; set; }

        public Coverage() {
            this.Id = "";
        }
    }
}
