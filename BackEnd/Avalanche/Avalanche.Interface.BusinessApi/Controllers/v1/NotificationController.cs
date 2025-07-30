using Avalanche.Core.Application.Constants;
using Avalanche.Core.Application.Dtos.Common;
using Avalanche.Core.Application.Features.Notification.Queries.GetAll;
using Avalanche.Core.Application.Helpers;
using Avalanche.Interface.BusinessAPI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Avalanche.Interface.BusinessApi.Controllers.v1
{
    [Route("api/v1/notification")]
    [SwaggerTag("Manejo de notificaciones")]
    public class NotificationController : BaseApiController
    {
        [Authorize(Roles = "Administrator, Analyst")]
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAllNotificationQueryResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDTO))]
        [SwaggerOperation(
           Summary = "Obtener todas las notificaciones",
           Description = "Nos permite obtener todas las notificaciones disponibles en el sistema"
        )]
        public async Task<IActionResult> GetNotifications([FromHeader(Name = "analyst")] string? assignedAnalyst)
        {
            try
            {
                var result = await Mediator.Send(new GetAllNotificationQuery() { AssignedAnalyst = assignedAnalyst });

                if (result.Notifications.Count == 0)
                {
                    if (!string.IsNullOrEmpty(assignedAnalyst))
                        return NotFound(ErrorMapperHelper.Error(ErrorMessages.NotFound, "El analista no ha tenido notificaciones"));

                    return NotFound(ErrorMapperHelper.Error(ErrorMessages.NotFound, "No hay notificaciones en el sistema"));
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ErrorMapperHelper.Error(ErrorMessages.InternalServer, e.Message));
            }
        }        
    }
}
