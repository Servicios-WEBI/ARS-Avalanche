namespace Avalanche.Core.Application.Dtos.Reports
{
    public class AnalystPerformanceDTO
    {
        public string AnalystId { get; set; }
        public string AnalystName { get; set; }
        public int TotalProcessed { get; set; }
        public double ApprovedAmount { get; set; }
        public double RejectedAmount { get; set; }
    }
}
