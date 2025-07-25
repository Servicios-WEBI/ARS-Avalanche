namespace Avalanche.Core.Application.Dtos.Authorization
{
    public class AuthorizationNotificationDTO
    {
        public string AuthorizationId { get; set; }
        public string Message { get; set; }
        public DateTime NotificationDate { get; set; }
    }
}
