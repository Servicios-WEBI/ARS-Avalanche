using Avalanche.Core.Application.Interfaces.Reposirories;
using Avalanche.Core.Domain.Entities;
using Avalanche.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Avalanche.Infrastructure.Persistence.Repositories
{
    public class InstitutionTypeRepository : GenericRepository<InstitutionType>, IInstitutionTypeRepository
    {
        private readonly IDbContextFactory<ApplicationContext> _dbContext;

        public InstitutionTypeRepository(IDbContextFactory<ApplicationContext> dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<InstitutionType> GetByNameAsync(string name)
        {
            using var dbContext = _dbContext.CreateDbContext();
            return await dbContext.Set<InstitutionType>()
                .Where(x => x.Name == name).FirstOrDefaultAsync();
        }
    }
}
