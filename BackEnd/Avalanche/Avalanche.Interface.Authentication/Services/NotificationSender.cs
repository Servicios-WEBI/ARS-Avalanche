using Avalanche.Core.Application.Dtos.Authorization;
using Avalanche.Core.Application.Interfaces.Services;
using Avalanche.Interface.Authentication.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Avalanche.Interface.Authentication.Services
{
    public class NotificationSender : INotificationSender
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationSender(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendAuthorizationAssignedAsync(string analystSub, AuthorizationNotificationDTO notification)
        {
            await _hubContext.Clients.User(analystSub).SendAsync("AuthorizationAssigned", notification);
        }
    }
}
