using Avalanche.Core.Application.Dtos.Reports;

namespace Avalanche.Core.Application.Features.Reports.Queries.GetAnalystPerformance
{
    public class GetAnalystPerformanceQueryResponse
    {
        public BaseReportDTO Period { get; set; }
        public List<AnalystPerformanceDTO> Analysts { get; set; }
    }

    public class AnalystPerformanceDTO
    {
        public string AnalystId { get; set; }
        public string AnalystName { get; set; }
        public int TotalProcessed { get; set; }
        public double ApprovedAmount { get; set; }
        public double RejectedAmount { get; set; }
    }
}
