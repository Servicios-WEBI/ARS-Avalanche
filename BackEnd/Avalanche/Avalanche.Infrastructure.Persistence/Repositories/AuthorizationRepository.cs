using Avalanche.Core.Application.Constants;
using Avalanche.Core.Application.Dtos.Reports;
using Avalanche.Core.Application.Features.Reports.Queries.GetAnalystPerformance;
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

            //Obtenemos las solicitudes dentro del rango de fechas
            var current = db.Authorization.Include(a => a.Status).Where(a => a.ApplicationDate >= start && a.ApplicationDate <= end);
            var previous = db.Authorization.Include(a => a.Status).Where(a => a.ApplicationDate >= prevStart && a.ApplicationDate <= prevEnd);

            //Filtramos solo por los estados requeridos
            var currentFiltered = current.Where(a => a.Status.Name == Statuses.Approved || a.Status.Name == Statuses.Rejected);
            var previousFiltered = previous.Where(a => a.Status.Name == Statuses.Approved || a.Status.Name == Statuses.Rejected);

            var totalCount = await currentFiltered.CountAsync();
            var previousCount = await previousFiltered.CountAsync();
            var approvedCount = await currentFiltered.Where(a => a.Status.Name == Statuses.Approved).CountAsync();
            var rejectedCount = await currentFiltered.Where(a => a.Status.Name == Statuses.Rejected).CountAsync();

            var result = new GetAuthorizationSummaryQueryResponse()
            {
                Period = new BaseReportDTO { Start = start, End = end },
                TotalCount = totalCount,
                ApprovalRate = totalCount == 0 ? 0 : approvedCount * 100.0 / totalCount,
                RejectionRate = totalCount == 0 ? 0 : rejectedCount * 100.0 / totalCount,
                ApprovedAmount = (double)await current.Where(a => a.Status.Name == Statuses.Approved).SumAsync(a => a.ApprovedAmount),
                RejectedAmount = (double)await current.Where(a => a.Status.Name == Statuses.Rejected).SumAsync(a => a.ApplicationAmount),
                PreviousPeriodCount = previousCount,
                PreviousApprovedAmount = (double)await previous.Where(a => a.Status.Name == Statuses.Approved).SumAsync(a => a.ApprovedAmount),
                PreviousRejectedAmount = (double)await previous.Where(a => a.Status.Name == Statuses.Rejected).SumAsync(a => a.ApplicationAmount),
            };

            return result;
        }
        
        public async Task<GetAnalystPerformanceQueryResponse> GetAnalystPerformanceAsync(DateOnly start, DateOnly end, string? analystId, int top = 10)
        {
            using var db = _dbContext.CreateDbContext();

            GetAnalystPerformanceQueryResponse result = new()
            {
                Period = new BaseReportDTO { Start = start, End = end },
                Analysts = new()
            };

            if (string.IsNullOrEmpty(analystId))
            {
                //Obtenemos las solicitudes dentro del rango de fechas y con los estados deseados
                var authorizationsInPeriod = db.Authorization
                    .Include(a => a.Status)
                    .Include(a => a.Analyst)
                    .Where(a => a.ApplicationDate >= start && a.ApplicationDate <= end
                && (a.Status.Name == Statuses.Approved || a.Status.Name == Statuses.Rejected));

                //Obtener los datos por analista
                var grouped = await authorizationsInPeriod
                .GroupBy(a => new { a.Analyst.Id, a.Analyst.FullName })
                .Select(g => new AnalystPerformanceDTO()
                {
                    AnalystId = g.Key.Id,
                    AnalystName = g.Key.FullName,
                    TotalProcessed = g.Count(),
                    ApprovedAmount = g
                    .Where(a => a.Status.Name == Statuses.Approved)
                    .Sum(a => a.ApprovedAmount ?? 0),
                    RejectedAmount = g
                    .Where(a => a.Status.Name == Statuses.Rejected)
                    .Sum(a => a.ApprovedAmount ?? 0)
                })
                .OrderByDescending(x => x.TotalProcessed) 
                .Take(top == 0 ? 10 : top)
                .ToListAsync();

                result.Analysts = grouped;
            }
            else
            {
                var analyst = await db.Analysts.FindAsync(analystId);

                if (analyst == null)
                    throw new Exception("Ese analista no existe en el sistema");

                //Obtener los datos por el analista deseado
                var authorizationsInPeriod = db.Authorization
                    .Include(a => a.Status)
                    .Include(a => a.Analyst)
                    .Where(a => a.ApplicationDate >= start && a.ApplicationDate <= end
                    && (a.Status.Name == Statuses.Approved || a.Status.Name == Statuses.Rejected) 
                    && a.AssignedAnalyst == analystId);

                var performance = new AnalystPerformanceDTO()
                {
                    AnalystId = analyst.Id,
                    AnalystName = analyst.FullName,
                    TotalProcessed = await authorizationsInPeriod.CountAsync(),
                    ApprovedAmount = await authorizationsInPeriod
                    .Where(a => a.Status.Name == Statuses.Approved)
                    .SumAsync(a => a.ApprovedAmount ?? 0),
                    RejectedAmount = await authorizationsInPeriod
                    .Where(a => a.Status.Name == Statuses.Rejected)
                    .SumAsync(a => a.ApprovedAmount ?? 0)
                };

                result.Analysts.Add(performance);
            }

            return result;
        }
    }
}
