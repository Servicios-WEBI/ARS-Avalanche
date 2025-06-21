using Avalanche.Core.Application.Interfaces.Repositories;
using Avalanche.Core.Domain.Entities;
using Avalanche.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Avalanche.Infrastructure.Persistence.Repositories
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        private readonly IDbContextFactory<ApplicationContext> _dbContext;

        public ClientRepository(IDbContextFactory<ApplicationContext> dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Client> GetByDocumentNumberAsync(Expression<Func<Client, bool>> predicate, List<Expression<Func<Client, object>>> properties)
        {
            using var dbContext = _dbContext.CreateDbContext();
            var query = dbContext.Set<Client>().AsQueryable();

            foreach (var property in properties)
            {
                query = query.Include(property);
            }

            var entityType = typeof(Client);
            var idProperty = entityType.GetProperty("DocumentNumber");

            return await query.FirstOrDefaultAsync(predicate);
        }
    }
}
