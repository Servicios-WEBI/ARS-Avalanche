using Avalanche.Core.Application.Interfaces.Repositories;
using Avalanche.Core.Domain.Entities;
using Avalanche.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Avalanche.Infrastructure.Persistence.Repositories
{
    public class HospitalRepository : GenericRepository<Hospital>, IHospitalRepository
    {
        private readonly IDbContextFactory<ApplicationContext> _dbContext;

        public HospitalRepository(IDbContextFactory<ApplicationContext> dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Hospital> GetByNameAsync(string name)
        {
            using var dbContext = _dbContext.CreateDbContext();
            return await dbContext.Set<Hospital>()
                .Where(x => x.Name == name).FirstOrDefaultAsync();
        }
    }
}
