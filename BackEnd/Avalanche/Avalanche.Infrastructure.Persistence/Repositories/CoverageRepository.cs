using Avalanche.Core.Application.Interfaces.Reposirories;
using Avalanche.Core.Domain.Entities;
using Avalanche.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Avalanche.Infrastructure.Persistence.Repositories
{
    public class CoverageRepository : GenericRepository<Coverage>, ICoverageRepository
    {
        private readonly IDbContextFactory<ApplicationContext> _dbContext;

        public CoverageRepository(IDbContextFactory<ApplicationContext> dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Coverage> GetByNameAsync(string name)
        {
            using var dbContext = _dbContext.CreateDbContext();
            return await dbContext.Set<Coverage>()
                .Where(x => x.Name == name).FirstOrDefaultAsync();
        }
    }
}
