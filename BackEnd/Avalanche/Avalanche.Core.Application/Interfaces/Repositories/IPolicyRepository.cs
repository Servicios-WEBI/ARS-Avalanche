using Avalanche.Core.Domain.Entities;
using System.Linq.Expressions;

namespace Avalanche.Core.Application.Interfaces.Repositories
{
    public interface IPolicyRepository : IGenericRepository<Policy>
    {
        Task<Policy> GetByNumberAsync(Expression<Func<Policy, bool>> predicate, List<Expression<Func<Policy, object>>> properties);
    }
}
