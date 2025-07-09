using Avalanche.Core.Application.Interfaces.Services;
using Avalanche.Core.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Avalanche.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            #region Services
            services.AddScoped<IPlanCoverageComparisonService, PlanCoverageComparisonService>();
            #endregion
        }
    }
}
