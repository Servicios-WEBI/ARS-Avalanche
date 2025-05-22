using Avalanche.Core.Application.Dtos.Email;
using Avalanche.Core.Application.Interfaces.Services;
using Avalanche.Core.Domain.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Avalanche.Infrastructure.Shared.Services
{
    public class EmailService : IEmailService
	{
		public MailSettings _mailSettings { get; }

		private readonly ILogger<EmailService> _logger;

		public EmailService(IOptions<MailSettings> mailSettings, ILogger<EmailService> logger)
		{
			_mailSettings = mailSettings.Value;
			_logger = logger;
		}

		public async Task SendAsync(EmailRequest request)
		{
			try
			{
				// create message
				var email = new MimeMessage();
				email.Sender = MailboxAddress.Parse(request.From ?? _mailSettings.EmailFrom);
				email.To.Add(MailboxAddress.Parse(request.To));
				email.Subject = request.Subject;
				var builder = new BodyBuilder();
				builder.HtmlBody = request.Body;
				email.Body = builder.ToMessageBody();
				using var smtp = new SmtpClient();
				smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
				smtp.Connect(_mailSettings.SmtpHost, _mailSettings.SmtpPort, SecureSocketOptions.StartTls);
				smtp.Authenticate(_mailSettings.SmtpUser, _mailSettings.SmtpPass);
				await smtp.SendAsync(email);
				smtp.Disconnect(true);

			}
			catch (Exception ex)
			{
				_logger.LogCritical(ex,"Algo ha ocurrido enviando el correo");
			}
		}

	}
}
