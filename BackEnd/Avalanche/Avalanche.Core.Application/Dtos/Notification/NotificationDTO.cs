namespace Avalanche.Core.Application.Dtos.Notification
{
    public class NotificationDTO
    {
        public string AssignedAnalyst { get; set; }
        public string AuthorizationId { get; set; }
        public string Message { get; set; }
        public DateTime NotificationDate { get; set; }
    }
}
