using Avalanche.Core.Application.Dtos.Reports;

namespace Avalanche.Core.Application.Features.Reports.Queries.GetAuthorizationDistribution
{
    public class GetAuthorizationDistributionQueryResponse
    {
        public BaseReportDTO Period { get; set; }
        public List<LabelCountDTO> ByType { get; set; } = new();
        public List<LabelCountDTO> ByHospital { get; set; } = new();
        public List<LabelCountDTO> ByPlan { get; set; } = new();
    }
}
