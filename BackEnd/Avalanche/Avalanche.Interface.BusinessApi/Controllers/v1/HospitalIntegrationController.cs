using Avalanche.Core.Application.Constants;
using Avalanche.Core.Application.Dtos.Common;
using Avalanche.Core.Application.Dtos.HospitalIntegration;
using Avalanche.Core.Application.Features.HospitalIntegration.Command.MakeAuthorization;
using Avalanche.Core.Application.Features.HospitalIntegration.Command.PayBills;
using Avalanche.Core.Application.Features.HospitalIntegration.Queries.CheckAuthorization;
using Avalanche.Core.Application.Features.HospitalIntegration.Queries.ValidateAffiliate;
using Avalanche.Core.Application.Helpers;
using Avalanche.Interface.BusinessAPI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Avalanche.Interface.BusinessApi.Controllers.v1
{
    [Route("api/v1/hospitales/integracion")]
    [SwaggerTag("Interoperabilidad para hospitales")]
    public class HospitalIntegrationController : BaseApiController
    {
        [Authorize(Roles = "Guest")]
        [HttpGet("validate-affiliate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ValidateAffiliateQueryResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ValidateAffiliateQueryResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDTO))]
        [SwaggerOperation(
           Summary = "Validar afiliado",
           Description = "Permite a los hospitales validar los clientes"
        )]
        public async Task<IActionResult> ValidateAffiliate([FromQuery] ValidateAffiliateQuery query)
        {
            try
            {
                if (query == null)
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

                var result = await Mediator.Send(query);

                if (!result.Exists)
                {
                    return NotFound(result);
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ErrorMapperHelper.Error(ErrorMessages.InternalServer, e.Message));
            }
        }

        [Authorize(Roles = "Administrator, Guest")]
        [HttpGet("check-authorization/{authorizationNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CheckAuthorizationQueryResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDTO))]
        [SwaggerOperation(
           Summary = "Consultar solicitudes",
           Description = "Permite a los hospitales consultar el estado de las solicitudes"
        )]
        public async Task<IActionResult> CheckAuthorization(int authorizationNumber)
        {
            try
            {
                var result = await Mediator.Send(new CheckAuthorizationQuery() { AuthorizationNumber = authorizationNumber });

                return Ok(result);
            }
            catch (Exception e)
            {
                if (e.Message == ErrorMessages.NotFound)
                    return NotFound(ErrorMapperHelper.Error(ErrorMessages.NotFound, "No existe una solicitud con ese identificador"));

                return StatusCode(StatusCodes.Status500InternalServerError, ErrorMapperHelper.Error(ErrorMessages.InternalServer, e.Message));
            }
        }

        [Authorize(Roles = "Guest")]
        [HttpPost("pay-bills")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PayBillResponseDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(PayBillResponseDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDTO))]
        [SwaggerOperation(
           Summary = "Pago de facturas",
           Description = "Permite a los hospitales recibir el pago por las solicitudes aprobadas"
        )]
        public async Task<IActionResult> PayBills([FromBody] PayBillsCommand command)
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

                if (result.Status == "Fallido")
                {
                    return NotFound(result);
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ErrorMapperHelper.Error(ErrorMessages.InternalServer, e.Message));
            }
        }

        [Authorize(Roles = "Guest")]
        [HttpPost("make-authorization")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthorizationResponseDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(AuthorizationResponseDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(AuthorizationResponseDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDTO))]
        [SwaggerOperation(
           Summary = "Registro de solicitud",
           Description = "Permite a los hospitales realizar solicitudes"
        )]
        public async Task<IActionResult> PayBills([FromBody] MakeAuthorizationCommand command)
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

                if (result.Status == "Fallido")
                {
                    return NotFound(result);
                }
                else if(result.Status != "Exitoso")
                {
                    return BadRequest(result);
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
