using Avalanche.Core.Application.Dtos.Common;

namespace Avalanche.Core.Application.Dtos.Plan
{
    public class PlanDTO : ErrorDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public double MonthlyCost { get; set; }
    }
}
