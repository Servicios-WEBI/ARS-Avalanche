using Avalanche.Core.Application.Constants;
using Avalanche.Core.Application.Dtos.Common;
using Avalanche.Core.Application.Dtos.Policy;
using Avalanche.Core.Application.Features.Policy.Command.Add;
using Avalanche.Core.Application.Features.Policy.Command.Delete;
using Avalanche.Core.Application.Features.Policy.Command.Update;
using Avalanche.Core.Application.Features.Policy.Queries.GetAll;
using Avalanche.Core.Application.Helpers;
using Avalanche.Interface.BusinessAPI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Avalanche.Core.Application.Features.Policy.Queries.GetById;

namespace Avalanche.Interface.BusinessApi.Controllers.v1
{
    [Route("api/v1/policy")]
    [SwaggerTag("Manejo de polizas")]
    public class PolicyController : BaseApiController
    {
        [Authorize(Roles = "Administrator, Analyst")]
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAllPolicyQueryResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDTO))]
        [SwaggerOperation(
           Summary = "Obtener todas las polizas",
           Description = "Nos permite obtener todas las polizas disponibles en el sistema"
        )]
        public async Task<IActionResult> GetPolicies()
        {
            try
            {
                var result = await Mediator.Send(new GetAllPolicyQuery());

                if (result.Policies.Count == 0)
                {
                    return NotFound(ErrorMapperHelper.Error(ErrorMessages.NotFound, "No existen polizas en el sistema"));
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ErrorMapperHelper.Error(ErrorMessages.InternalServer, e.Message));
            }

        }

        [Authorize(Roles = "Administrator, Analyst")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetByIdPolicyQueryResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDTO))]
        [SwaggerOperation(
            Summary = "Obtener detalles de poliza",
            Description = "Nos permite obtener todos los detalles de la poliza"
         )]
        public async Task<IActionResult> GetPolicy([FromRoute] string id)
        {
            try
            {
                var result = await Mediator.Send(new GetByIdPolicyQuery() { Id = id });

                return Ok(result);
            }
            catch (Exception e)
            {
                if (e.Message == ErrorMessages.NotFound)
                    return NotFound(ErrorMapperHelper.Error(ErrorMessages.NotFound, "No existe una poliza con ese identificador único"));

                return StatusCode(StatusCodes.Status500InternalServerError, ErrorMapperHelper.Error(ErrorMessages.InternalServer, e.Message));
            }

        }
        
        [Authorize(Roles = "Administrator, Analyst")]
        [HttpPost()]
        [SwaggerOperation(
           Summary = "Crear una poliza",
           Description = "Nos permite crear una poliza"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PolicyDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDTO))]
        public async Task<IActionResult> PostPolicy([FromBody] AddPolicyCommand command)
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

        [Authorize(Roles = "Administrator, Analyst")]
        [HttpPut()]
        [SwaggerOperation(
           Summary = "Editar una poliza",
           Description = "Nos permite editar una poliza"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PolicyDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDTO))]
        public async Task<IActionResult> PutPolicy([FromBody] UpdatePolicyCommand command)
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
                    return NotFound(ErrorMapperHelper.Error(ErrorMessages.NotFound, "No existe una poliza con ese identificador único"));

                return StatusCode(StatusCodes.Status500InternalServerError, ErrorMapperHelper.Error(ErrorMessages.InternalServer, e.Message));
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        [SwaggerOperation(
           Summary = "Eliminar una poliza",
           Description = "Nos permite eliminar una poliza"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PolicyDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDTO))]
        public async Task<IActionResult> DeletePolicy([FromRoute] string id)
        {
            try
            {
                DeletePolicyCommand command = new() { Id = id };

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
                    return NotFound(ErrorMapperHelper.Error(ErrorMessages.NotFound, "No existe una poliza con ese identificador único"));

                return StatusCode(StatusCodes.Status500InternalServerError, ErrorMapperHelper.Error(ErrorMessages.InternalServer, e.Message));
            }
        }
    }
}
