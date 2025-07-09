using Avalanche.Core.Application.Dtos.Common;
using Avalanche.Core.Application.Dtos.PlanCoverage;

namespace Avalanche.Core.Application.Dtos.Plan
{
    public class PlanDTO : ErrorDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public double MonthlyCost { get; set; }
        public List<PlanCoverageDTO> Coverages { get; set; }
    }
}
