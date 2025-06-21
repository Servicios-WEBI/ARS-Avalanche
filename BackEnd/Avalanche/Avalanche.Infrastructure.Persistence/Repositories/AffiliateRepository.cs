using Avalanche.Core.Application.Interfaces.Repositories;
using Avalanche.Core.Domain.Entities;
using Avalanche.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq.Expressions;

namespace Avalanche.Infrastructure.Persistence.Repositories
{
    public class AffiliateRepository : GenericRepository<Affiliate>, IAffiliateRepository
    {
        private readonly IDbContextFactory<ApplicationContext> _dbContext;

        public AffiliateRepository(IDbContextFactory<ApplicationContext> dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Affiliate> GetByDocumentNumberAsync(Expression<Func<Affiliate, bool>> predicate, List<Expression<Func<Affiliate, object>>> properties)
        {
            using var dbContext = _dbContext.CreateDbContext();
            var query = dbContext.Set<Affiliate>().AsQueryable();

            foreach (var property in properties)
            {
                query = query.Include(property);
            }

            var entityType = typeof(Affiliate);
            var idProperty = entityType.GetProperty("DocumentNumber");

            return await query.FirstOrDefaultAsync(predicate);
        }
    }
}
