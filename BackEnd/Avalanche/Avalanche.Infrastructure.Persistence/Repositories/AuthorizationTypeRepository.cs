using Avalanche.Core.Domain.Entities;
using Avalanche.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Avalanche.Infrastructure.Persistence.Repositories
{
    public class AuthorizationTypeRepository : GenericRepository<AuthorizationType>
    {
        private readonly IDbContextFactory<ApplicationContext> _dbContext;

        public AuthorizationTypeRepository(IDbContextFactory<ApplicationContext> dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
