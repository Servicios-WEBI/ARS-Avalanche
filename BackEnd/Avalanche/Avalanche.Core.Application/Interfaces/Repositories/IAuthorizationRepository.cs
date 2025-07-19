using Avalanche.Core.Application.Features.Reports.Queries.GetAuthorizationSummary;
using Avalanche.Core.Domain.Entities;

namespace Avalanche.Core.Application.Interfaces.Repositories
{
    public interface IAuthorizationRepository : IGenericRepository<Authorization>
    {
        Task<GetAuthorizationSummaryQueryResponse> GetAuthorizationSummaryAsync(DateOnly start, DateOnly end, DateOnly prevStart, DateOnly prevEnd);
    }
}
