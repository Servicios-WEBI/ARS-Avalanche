using Avalanche.Core.Application.Dtos.Reports;

namespace Avalanche.Core.Application.Features.Reports.Queries.GetAnalystPerformance
{
    public class GetAnalystPerformanceQueryResponse
    {
        public BaseReportDTO Period { get; set; }
        public List<AnalystPerformanceDTO> Analysts { get; set; }
    }
}
