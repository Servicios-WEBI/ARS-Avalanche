using Avalanche.Core.Application.Interfaces.Repositories;
using Avalanche.Core.Domain.Entities;
using Avalanche.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Avalanche.Infrastructure.Persistence.Repositories
{
    public class PolicyRepository : GenericRepository<Policy>, IPolicyRepository
    {
        private readonly IDbContextFactory<ApplicationContext> _dbContext;

        public PolicyRepository(IDbContextFactory<ApplicationContext> dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Policy> GetByNumberAsync(Expression<Func<Policy, bool>> predicate, List<Expression<Func<Policy, object>>> properties)
        {
            using var dbContext = _dbContext.CreateDbContext();
            var query = dbContext.Set<Policy>().AsQueryable();

            foreach (var property in properties)
            {
                query = query.Include(property);
            }

            var entityType = typeof(Policy);
            var idProperty = entityType.GetProperty("Number");

            return await query.FirstOrDefaultAsync(predicate);
        }
    }
}
