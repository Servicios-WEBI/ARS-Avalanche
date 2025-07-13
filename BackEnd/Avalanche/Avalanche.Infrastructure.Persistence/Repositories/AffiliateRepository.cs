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
    }
}
