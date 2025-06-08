using Avalanche.Core.Domain.Common;

namespace Avalanche.Core.Domain.Entities
{
    public class Plan : AuditableBaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public double MonthlyCost { get; set; }
        public List<Policy> Policies { get; set; }
        public List<PlanCoverage> PlanCoverages { get; set; }

        public Plan() {
            this.Id = "";
        }
    }
}
