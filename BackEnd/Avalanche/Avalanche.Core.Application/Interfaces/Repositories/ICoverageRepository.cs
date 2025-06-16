using Avalanche.Core.Domain.Entities;

namespace Avalanche.Core.Application.Interfaces.Repositories
{
    public interface ICoverageRepository : IGenericRepository<Coverage>
    {
        Task<Coverage> GetByNameAsync(string name);
    }
}
