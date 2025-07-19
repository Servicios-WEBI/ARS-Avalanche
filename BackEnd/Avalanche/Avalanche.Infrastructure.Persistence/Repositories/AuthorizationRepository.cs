using Avalanche.Core.Application.Dtos.Reports;
using Avalanche.Core.Application.Features.Reports.Queries.GetAuthorizationSummary;
using Avalanche.Core.Application.Interfaces.Repositories;
using Avalanche.Core.Domain.Entities;
using Avalanche.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Avalanche.Infrastructure.Persistence.Repositories
{
    public class AuthorizationRepository : GenericRepository<Authorization>, IAuthorizationRepository
    {
        private readonly IDbContextFactory<ApplicationContext> _dbContext;

        public AuthorizationRepository(IDbContextFactory<ApplicationContext> dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetAuthorizationSummaryQueryResponse> GetAuthorizationSummaryAsync(DateOnly start, DateOnly end, DateOnly prevStart, DateOnly prevEnd)
        {
            using var db = _dbContext.CreateDbContext();

            //Estados a tomar en cuenta
            var validStatuses = new[] { "Aprobado", "Rechazado" };

            //Obtenemos las solicitudes dentro del rango de fechas
            var current = db.Authorization.Include<Authorization>("Status").Where(a => a.ApplicationDate >= start && a.ApplicationDate <= end);
            var previous = db.Authorization.Include<Authorization>("Status").Where(a => a.ApplicationDate >= prevStart && a.ApplicationDate <= prevEnd);

            //Filtramos solo por los estados requeridos
            var currentFiltered = current.Where(a => validStatuses.Contains(a.Status.Name));
            var previousFiltered = previous.Where(a => validStatuses.Contains(a.Status.Name));

            var totalCount = await currentFiltered.CountAsync();
            var previousCount = await previousFiltered.CountAsync();
            var approvedCount = await currentFiltered.Where(a => a.Status.Name == "Aprobado").CountAsync();
            var rejectedCount = await currentFiltered.Where(a => a.Status.Name == "Rechazado").CountAsync();

            var result = new GetAuthorizationSummaryQueryResponse()
            {
                Period = new BaseReportDTO { Start = start, End = end },
                TotalCount = totalCount,
                ApprovalRate = totalCount == 0 ? 0 : approvedCount * 100.0 / totalCount,
                RejectionRate = totalCount == 0 ? 0 : rejectedCount * 100.0 / totalCount,
                ApprovedAmount = (double)await current.Where(a => a.Status.Name == "Aprobado").SumAsync(a => a.ApprovedAmount),
                RejectedAmount = (double)await current.Where(a => a.Status.Name == "Rechazado").SumAsync(a => a.ApplicationAmount),
                PreviousPeriodCount = previousCount,
                PreviousApprovedAmount = (double)await previous.Where(a => a.Status.Name == "Aprobado").SumAsync(a => a.ApprovedAmount),
                PreviousRejectedAmount = (double)await previous.Where(a => a.Status.Name == "Rechazado").SumAsync(a => a.ApplicationAmount),
            };

            return result;
        }
    }
}
