using Avalanche.Core.Domain.Common;

namespace Avalanche.Core.Domain.Entities
{
    public class PlanCoverage : AuditableBaseEntity
    {
        public double AmountLimit { get; set; }
        public int YearFrequencyLimit { get; set; }
        public double CoveragePercentage {  get; set; }
        public Plan Plan { get; set; }
        public string PlanId { get; set; }
        public Coverage Coverage { get; set; }
        public string CoverageId { get; set; }

        public PlanCoverage() {
            this.Id = "";
        }
    }
}
