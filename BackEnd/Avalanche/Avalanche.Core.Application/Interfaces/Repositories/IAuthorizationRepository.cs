using Avalanche.Core.Application.Features.Reports.Queries.GetAnalystPerformance;
using Avalanche.Core.Application.Features.Reports.Queries.GetAuthorizationDistribution;
using Avalanche.Core.Application.Features.Reports.Queries.GetAuthorizationSummary;
using Avalanche.Core.Domain.Entities;

namespace Avalanche.Core.Application.Interfaces.Repositories
{
    public interface IAuthorizationRepository : IGenericRepository<Authorization>
    {
        Task<GetAuthorizationSummaryQueryResponse> GetAuthorizationSummaryAsync(DateOnly start, DateOnly end, DateOnly prevStart, DateOnly prevEnd);
        Task<GetAnalystPerformanceQueryResponse> GetAnalystPerformanceAsync(DateOnly start, DateOnly end, string? analystId, int top = 10);
        Task<GetAuthorizationDistributionQueryResponse> GetAuthorizationDistributionAsync(DateOnly start, DateOnly end);
    }
}
