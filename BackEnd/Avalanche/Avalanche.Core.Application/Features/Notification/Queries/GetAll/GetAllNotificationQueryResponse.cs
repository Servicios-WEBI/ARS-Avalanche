using Swashbuckle.AspNetCore.Annotations;

namespace Avalanche.Core.Application.Features.Notification.Queries.GetAll
{
    public class GetAllNotificationQueryResponse
    {
        [SwaggerSchema(Description = "Listado de estados")]
        public List<GetAllNotificationQueryResponseChild> Notifications { get; set; }
    }

    public class GetAllNotificationQueryResponseChild
    {
        [SwaggerSchema(Description = "Identificador único")]
        public string Id { get; set; }

        [SwaggerSchema(Description = "Id de la autorización")]
        public string AuthorizationId { get; set; }

        [SwaggerSchema(Description = "Analista asignado")]
        public string AssignedAnalyst { get; set; }

        [SwaggerSchema(Description = "Id del analista asignado")]
        public string AssignedAnalystId { get; set; }

        [SwaggerSchema(Description = "Fecha y hora de la notificación")]
        public DateTime NotificationDate { get; set; }
    }
}
