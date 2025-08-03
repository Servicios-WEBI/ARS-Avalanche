using Avalanche.Core.Application.Constants;
using Avalanche.Core.Application.Dtos.Common;
using Avalanche.Core.Application.Dtos.Plan;
using Avalanche.Core.Application.Features.Plan.Command.Add;
using Avalanche.Core.Application.Features.Plan.Command.Delete;
using Avalanche.Core.Application.Features.Plan.Command.Update;
using Avalanche.Core.Application.Features.Plan.Queries.GetAll;
using Avalanche.Core.Application.Features.Plan.Queries.GetCoveragesById;
using Avalanche.Core.Application.Helpers;
using Avalanche.Interface.BusinessAPI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Avalanche.Interface.BusinessApi.Controllers.v1
{
    [Route("api/v1/plan")]
    [SwaggerTag("Manejo de planes")]
    public class PlanController : BaseApiController
    {
        [Authorize(Roles = "Administrator, Analyst")]
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAllPlanQueryResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDTO))]
        [SwaggerOperation(
           Summary = "Obtener todos los planes",
           Description = "Nos permite obtener todos los planes disponibles en el sistema"
        )]
        public async Task<IActionResult> GetPlans()
        {
            try
            {
                var result = await Mediator.Send(new GetAllPlanQuery());

                if (result.Plans.Count == 0)
                {
                    return NotFound(ErrorMapperHelper.Error(ErrorMessages.NotFound, "No existen planes en el sistema"));
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ErrorMapperHelper.Error(ErrorMessages.InternalServer, e.Message));
            }

        }

        [Authorize(Roles = "Administrator, Analyst")]
        [HttpGet("{id}/coverages")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetCoveragesByIdPlanQueryResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDTO))]
        [SwaggerOperation(
           Summary = "Obtener las coberturas de un plan",
           Description = "Nos permite obtener las coberturas de un plan"
        )]
        public async Task<IActionResult> GetPlan(string id)
        {
            try
            {
                var result = await Mediator.Send(new GetCoveragesByIdPlanQuery() { Id = id});

                return Ok(result);
            }
            catch (Exception e)
            {
                if (e.Message == ErrorMessages.NotFound)
                    return NotFound(ErrorMapperHelper.Error(ErrorMessages.NotFound, "No existe un plan con ese identificador único"));

                return StatusCode(StatusCodes.Status500InternalServerError, ErrorMapperHelper.Error(ErrorMessages.InternalServer, e.Message));
            }

        }

        [Authorize(Roles = "Administrator")]
        [HttpPost()]
        [SwaggerOperation(
           Summary = "Crear un plan",
           Description = "Nos permite crear un plan"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PlanDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDTO))]
        public async Task<IActionResult> PostPlans([FromBody] AddPlanCommand command)
        {
            try
            {
                if (command == null)
                {
                    return BadRequest(ErrorMapperHelper.Error(ErrorMessages.BadRequest, "El cuerpo de la solicitud no puede estar vacío o tiene errores de formato."));
                }

                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList<string>();

                    return BadRequest(ErrorMapperHelper.ListError(errors));
                }

                var result = await Mediator.Send(command);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ErrorMapperHelper.Error(ErrorMessages.InternalServer, e.Message));
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut()]
        [SwaggerOperation(
           Summary = "Editar un plan",
           Description = "Nos permite editar un plan"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PlanDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDTO))]
        public async Task<IActionResult> PutPlans([FromBody] UpdatePlanCommand command)
        {
            try
            {
                if (command == null)
                {
                    return BadRequest(ErrorMapperHelper.Error(ErrorMessages.BadRequest, "El cuerpo de la solicitud no puede estar vacío o tiene errores de formato."));
                }

                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList<string>();

                    return BadRequest(ErrorMapperHelper.ListError(errors));
                }

                var result = await Mediator.Send(command);
                return Ok(result);
            }
            catch (Exception e)
            {
                if (e.Message == ErrorMessages.NotFound)
                    return NotFound(ErrorMapperHelper.Error(ErrorMessages.NotFound, "No existe un plan con ese identificador único"));

                return StatusCode(StatusCodes.Status500InternalServerError, ErrorMapperHelper.Error(ErrorMessages.InternalServer, e.Message));
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        [SwaggerOperation(
           Summary = "Eliminar un plan",
           Description = "Nos permite eliminar un plan"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PlanDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDTO))]
        public async Task<IActionResult> DeletePlans([FromRoute] string id)
        {
            try
            {
                DeletePlanCommand command = new() { Id = id };

                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList<string>();

                    return BadRequest(ErrorMapperHelper.ListError(errors));
                }

                var result = await Mediator.Send(command);
                return Ok(result);
            }
            catch (Exception e)
            {
                if (e.Message == ErrorMessages.NotFound)
                    return NotFound(ErrorMapperHelper.Error(ErrorMessages.NotFound, "No existe un plan con ese identificador único"));

                return StatusCode(StatusCodes.Status500InternalServerError, ErrorMapperHelper.Error(ErrorMessages.InternalServer, e.Message));
            }
        }
    }
}
