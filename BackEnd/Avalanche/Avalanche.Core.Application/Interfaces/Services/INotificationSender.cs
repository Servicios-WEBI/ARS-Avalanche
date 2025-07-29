using Avalanche.Core.Application.Dtos.Notification;

namespace Avalanche.Core.Application.Interfaces.Services
{
    public interface INotificationSender
    {
        Task SendAuthorizationAssignedAsync(string analystSub, NotificationDTO notification);
    }
}
