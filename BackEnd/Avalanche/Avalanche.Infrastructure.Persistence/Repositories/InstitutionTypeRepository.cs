using Avalanche.Core.Application.Interfaces.Repositories;
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
    }
}
