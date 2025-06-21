using Avalanche.Core.Domain.Entities;
using System.Linq.Expressions;

namespace Avalanche.Core.Application.Interfaces.Repositories
{
    public interface IAffiliateRepository : IGenericRepository<Affiliate>
    {
        Task<Affiliate> GetByDocumentNumberAsync(Expression<Func<Affiliate, bool>> predicate, List<Expression<Func<Affiliate, object>>> properties);
    }
}
