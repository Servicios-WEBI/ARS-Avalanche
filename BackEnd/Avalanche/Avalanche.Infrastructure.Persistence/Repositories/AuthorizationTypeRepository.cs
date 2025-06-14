using Avalanche.Core.Application.Interfaces.Reposirories;
using Avalanche.Core.Domain.Entities;
using Avalanche.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Avalanche.Infrastructure.Persistence.Repositories
{
    public class AuthorizationTypeRepository : GenericRepository<AuthorizationType>, IAuthorizationTypeRepository
    {
        private readonly IDbContextFactory<ApplicationContext> _dbContext;

        public AuthorizationTypeRepository(IDbContextFactory<ApplicationContext> dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AuthorizationType> GetByNameAsync(string name)
        {
            using var dbContext = _dbContext.CreateDbContext();
            return await dbContext.Set<AuthorizationType>()
                .Where(x => x.Name == name).FirstOrDefaultAsync();
        }
    }
}
