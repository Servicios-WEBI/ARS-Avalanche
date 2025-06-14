using Avalanche.Core.Application.Interfaces.Repositories;
using Avalanche.Core.Domain.Entities;

namespace Avalanche.Core.Application.Interfaces.Reposirories
{
    public interface IDocumentTypeRepository : IGenericRepository<DocumentType>
    {
        Task<DocumentType> GetByNameAsync(string name);
    }
}
