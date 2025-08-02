using Avalanche.Core.Application.Clients;
using Avalanche.Core.Application.Helpers;
using Avalanche.Core.Application.Interfaces.Clients;
using Avalanche.Core.Application.Interfaces.Helpers;
using Avalanche.Core.Application.Interfaces.Services;
using Avalanche.Core.Application.Services;
using Avalanche.Core.Domain.Entities;
using Avalanche.Core.Domain.Settings;
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

            #region Configurations
            // Access to replace placeholders
            var healthStateApiSection = configuration.GetSection("HealthStateApi");
            var healthStateApiSettings = healthStateApiSection.Get<HealthStateApiSettings>();

            // Replace placeholders in settings
            healthStateApiSettings.Username = configuration["HEALTHSTATEAPI_USERNAME"];
            healthStateApiSettings.Password = configuration["HEALTHSTATEAPI_PASSWORD"];
            healthStateApiSettings.BaseUrl = configuration["HEALTHSTATEAPI_BASEURL"];

            // Configuring RefreshJWT settings
            services.Configure<HealthStateApiSettings>(options =>
            {
                options.Username = healthStateApiSettings.Username;
                options.Password = healthStateApiSettings.Password;
                options.BaseUrl = healthStateApiSettings.BaseUrl;
            });
            #endregion

            #region Services
            services.AddScoped<IPlanCoverageComparisonService, PlanCoverageComparisonService>();
            services.AddScoped<IAffiliateValidationService, AffiliateValidationService>();
            services.AddScoped<IEmailHelper, EmailHelper>();
            services.AddHttpClient<IHealthStateApiClient, HealthStateApiClient>(c =>
            {
                c.BaseAddress = new Uri(healthStateApiSettings.BaseUrl);
                c.Timeout = TimeSpan.FromSeconds(30);
            });
            services.AddSingleton<ITokenService, TokenService>();
            services.AddScoped<IHealthStateService, HealthStateService>();
            #endregion
        }
    }
}
