using Avalanche.Core.Application.Interfaces.Repositories;
using Avalanche.Core.Domain.Entities;

namespace Avalanche.Core.Application.Interfaces.Reposirories
{
    public interface ICoverageRepository : IGenericRepository<Coverage>
    {
        Task<Coverage> GetByNameAsync(string name);
    }
}
