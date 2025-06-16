using Avalanche.Core.Domain.Entities;

namespace Avalanche.Core.Application.Interfaces.Repositories
{
    public interface IPlanRepository : IGenericRepository<Plan>
    {
        Task<Plan> GetByNameAsync(string name);
    }
}
