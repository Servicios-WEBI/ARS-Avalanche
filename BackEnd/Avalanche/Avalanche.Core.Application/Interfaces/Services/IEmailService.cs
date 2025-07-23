using Avalanche.Core.Application.Dtos.Email;
using Avalanche.Core.Domain.Settings;

namespace Avalanche.Core.Application.Interfaces.Services
{
    public interface IEmailService
	{
		Task SendAsync(EmailRequest request);
	}
}
