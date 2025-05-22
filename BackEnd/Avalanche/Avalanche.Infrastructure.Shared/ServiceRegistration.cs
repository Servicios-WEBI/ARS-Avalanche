using Avalanche.Core.Application.Interfaces.Services;
using Avalanche.Core.Domain.Settings;
using Avalanche.Infrastructure.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Avalanche.Infrastructure.Shared
{
    public static class ServiceRegistration
	{
		public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
            // Access to replace placeholders
            var mailSettingsSection = configuration.GetSection("MailSettings");
            var mailSettings = mailSettingsSection.Get<MailSettings>();

            // Replace placeholders in settings
            mailSettings.EmailFrom = configuration["SMTPEMAIL"];
            mailSettings.SmtpUser = configuration["SMTPEMAIL"];
            mailSettings.SmtpPass = configuration["SMTPPASS"];

            // Configuring Mail settings
            services.Configure<MailSettings>(options =>
            {
                options.EmailFrom = mailSettings.EmailFrom;
                options.SmtpHost = mailSettings.SmtpHost;
                options.SmtpPort = mailSettings.SmtpPort;
                options.SmtpUser = mailSettings.SmtpUser;
                options.SmtpPass = mailSettings.SmtpPass;
                options.DisplayName = mailSettings.DisplayName;
            });

            services.AddTransient<IEmailService, EmailService>();
		}
	}
}
