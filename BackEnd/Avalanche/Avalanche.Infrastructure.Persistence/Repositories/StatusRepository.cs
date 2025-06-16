using Avalanche.Core.Application.Interfaces.Repositories;
using Avalanche.Core.Domain.Entities;
using Avalanche.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Avalanche.Infrastructure.Persistence.Repositories
{
    public class StatusRepository : GenericRepository<Status>, IStatusRepository
    {
        private readonly IDbContextFactory<ApplicationContext> _dbContext;

        public StatusRepository(IDbContextFactory<ApplicationContext> dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Status> GetByNameAsync(string name)
        {
            using var dbContext = _dbContext.CreateDbContext();
            return await dbContext.Set<Status>()
                .Where(x => x.Name == name).FirstOrDefaultAsync();
        }
    }
}
