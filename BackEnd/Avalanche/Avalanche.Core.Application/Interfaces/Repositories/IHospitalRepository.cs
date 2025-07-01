using Avalanche.Core.Domain.Entities;

namespace Avalanche.Core.Application.Interfaces.Repositories
{
    public interface IHospitalRepository : IGenericRepository<Hospital>
    {
        Task<Hospital> GetByNameAsync(string name);
    }
}
