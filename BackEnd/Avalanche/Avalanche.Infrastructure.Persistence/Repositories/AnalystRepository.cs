using Avalanche.Core.Application.Constants;
using Avalanche.Core.Application.Interfaces.Repositories;
using Avalanche.Core.Domain.Entities;
using Avalanche.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Avalanche.Infrastructure.Persistence.Repositories
{
    public class AnalystRepository : GenericRepository<Analyst>, IAnalystRepository
    {
        private readonly IDbContextFactory<ApplicationContext> _dbContext;

        public AnalystRepository(IDbContextFactory<ApplicationContext> dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Analyst?> GetAnalystWithLeastWorkloadAsync(string pending)
        {
            using var db = _dbContext.CreateDbContext();

            return await db.Analysts
                .Where(a => a.IsActive)                        
                .Select(a => new
                {
                    Analyst = a,
                    PendingCount = a.Authorizations
                                   .Count(auth => auth.StatusId == pending)
                })
                .OrderBy(x => x.PendingCount)                 // menor primero
                .ThenBy(x => x.Analyst.Created)               // desempate opcional
                .Select(x => x.Analyst)
                .FirstOrDefaultAsync();
        }
    }
}
