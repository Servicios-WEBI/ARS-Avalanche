namespace Avalanche.Core.Application.Dtos.PlanCoverage
{
    public class PlanCoverageSeedDTO
    {
        public string Id { get; set; }
        public double AmountLimit { get; set; }
        public int YearFrequencyLimit { get; set; }
        public double CoveragePercentage { get; set; }
        public string PlanName { get; set; }
        public string CoverageName { get; set; }
    }
}
