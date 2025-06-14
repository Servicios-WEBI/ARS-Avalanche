using Avalanche.Core.Application.Interfaces.Repositories;
using Avalanche.Core.Domain.Entities;

namespace Avalanche.Core.Application.Interfaces.Reposirories
{
    public interface IInstitutionTypeRepository : IGenericRepository<InstitutionType>
    {
        Task<InstitutionType> GetByNameAsync(string name);
    }
}
