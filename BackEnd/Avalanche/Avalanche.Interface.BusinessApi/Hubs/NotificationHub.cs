using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Avalanche.Interface.BusinessApi.Hubs
{
    [Authorize]
    public class NotificationHub : Hub
    {
        // Sin métodos públicos aquí, solo se usa el HubContext para enviar notificaciones desde el backend.
    }
}
