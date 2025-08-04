using Avalanche.Core.Application.Constants;
using Avalanche.Core.Application.Dtos.Common;
using Avalanche.Core.Application.Features.Status.Queries.GetAll;
using Avalanche.Core.Application.Helpers;
using Avalanche.Interface.BusinessAPI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Avalanche.Interface.BusinessApi.Controllers.v1
{
    [Route("api/v1/status")]
    [SwaggerTag("Manejo de estados")]
    public class StatusController : BaseApiController
    {
        [Authorize(Roles = "Administrator, Analyst")]
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAllStatusQueryResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDTO))]
        [SwaggerOperation(
           Summary = "Obtener todos los estados",
           Description = "Nos permite obtener todos los estados disponibles en el sistema"
        )]
        public async Task<IActionResult> GetStatuss()
        {
            try
            {
                var result = await Mediator.Send(new GetAllStatusQuery());

                if (result.Statuses.Count == 0)
                {
                    return NotFound(ErrorMapperHelper.Error(ErrorMessages.NotFound, "No existen estados en el sistema"));
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
