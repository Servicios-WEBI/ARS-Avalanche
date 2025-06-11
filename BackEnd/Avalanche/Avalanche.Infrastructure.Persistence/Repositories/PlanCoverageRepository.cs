using Avalanche.Core.Domain.Entities;
using Avalanche.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Avalanche.Infrastructure.Persistence.Repositories
{
    public class PlanCoverageRepository : GenericRepository<PlanCoverage>
    {
        private readonly IDbContextFactory<ApplicationContext> _dbContext;

        public PlanCoverageRepository(IDbContextFactory<ApplicationContext> dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
