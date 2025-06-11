using Avalanche.Core.Domain.Entities;
using Avalanche.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Avalanche.Infrastructure.Persistence.Repositories
{
    public class AffiliatePolicyRepository : GenericRepository<AffiliatePolicy>
    {
        private readonly IDbContextFactory<ApplicationContext> _dbContext;

        public AffiliatePolicyRepository(IDbContextFactory<ApplicationContext> dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
