using Avalanche.Core.Application.Dtos.Email;
using Avalanche.Core.Domain.Settings;

namespace Avalanche.Core.Application.Interfaces.Services
{
    public interface IEmailService
	{
		public MailSettings _mailSettings { get; }
		Task SendAsync(EmailRequest request);
	}
}
