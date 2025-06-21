using Avalanche.Core.Domain.Entities;
using System.Linq.Expressions;

namespace Avalanche.Core.Application.Interfaces.Repositories
{
    public interface IClientRepository : IGenericRepository<Client>
    {
        Task<Client> GetByDocumentNumberAsync(Expression<Func<Client, bool>> predicate, List<Expression<Func<Client, object>>> properties);
    }
}
