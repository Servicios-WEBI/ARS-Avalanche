using Avalanche.Core.Application.Dtos.Reports;

namespace Avalanche.Core.Application.Features.Reports.Queries.GetAuthorizationSummary
{
    public class GetAuthorizationSummaryQueryResponse
    {
        public BaseReportDTO Period { get; set; }
        public int TotalCount { get; set; }
        public double ApprovalRate { get; set; }
        public double RejectionRate { get; set; }
        public double ApprovedAmount { get; set; }
        public double RejectedAmount { get; set; }
        public int PreviousPeriodCount { get; set; }
        public double PreviousApprovedAmount { get; set; }
        public double PreviousRejectedAmount { get; set; }
    }
}
