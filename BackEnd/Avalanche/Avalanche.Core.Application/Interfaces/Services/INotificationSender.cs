using Avalanche.Core.Application.Dtos.Authorization;

namespace Avalanche.Core.Application.Interfaces.Services
{
    public interface INotificationSender
    {
        Task SendAuthorizationAssignedAsync(string analystSub, AuthorizationNotificationDTO notification);
    }
}
