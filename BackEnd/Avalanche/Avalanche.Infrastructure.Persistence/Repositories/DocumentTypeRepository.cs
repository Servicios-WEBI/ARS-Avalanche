using Avalanche.Core.Application.Interfaces.Repositories;
using Avalanche.Core.Domain.Entities;
using Avalanche.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Avalanche.Infrastructure.Persistence.Repositories
{
    public class DocumentTypeRepository : GenericRepository<DocumentType>, IDocumentTypeRepository
    {
        private readonly IDbContextFactory<ApplicationContext> _dbContext;

        public DocumentTypeRepository(IDbContextFactory<ApplicationContext> dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DocumentType> GetByNameAsync(string name)
        {
            using var dbContext = _dbContext.CreateDbContext();
            return await dbContext.Set<DocumentType>()
                .Where(x => x.Name == name).FirstOrDefaultAsync();
        }
    }
}
