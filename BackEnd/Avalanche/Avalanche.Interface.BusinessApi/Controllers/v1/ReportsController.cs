using Avalanche.Core.Application.Constants;
using Avalanche.Core.Application.Dtos.Common;
using Avalanche.Core.Application.Features.Reports.Queries.GetAnalystPerformance;
using Avalanche.Core.Application.Features.Reports.Queries.GetAuthorizationDistribution;
using Avalanche.Core.Application.Features.Reports.Queries.GetAuthorizationSummary;
using Avalanche.Core.Application.Helpers;
using Avalanche.Interface.BusinessAPI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Avalanche.Interface.BusinessApi.Controllers.v1
{
    [Route("api/v1/reports/")]
    [SwaggerTag("Consulta de reportes")]
    public class ReportsController : BaseApiController
    {
        [Authorize(Roles = "Administrator")]
        [HttpGet("authorizations/summary")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAuthorizationSummaryQueryResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDTO))]
        [SwaggerOperation(
           Summary = "Reporte de autorizaciones",
           Description = "Permite obtener información de reportería sobre las autorizaciones por mes"
        )]
        public async Task<IActionResult> GetAuthorizationSummary([FromQuery] GetAuthorizationSummaryQuery query)
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

                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ErrorMapperHelper.Error(ErrorMessages.InternalServer, e.Message));
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("authorizations/analyst-performance")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAnalystPerformanceQueryResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDTO))]
        [SwaggerOperation(
           Summary = "Reporte de rendimiento de los analistas",
           Description = "Permite obtener información de reportería sobre el rendimiento de los analistas por mes"
        )]
        public async Task<IActionResult> GetAnalystPerformance([FromQuery] GetAnalystPerformanceQuery query)
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

                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ErrorMapperHelper.Error(ErrorMessages.InternalServer, e.Message));
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("authorizations/distribution")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAuthorizationDistributionQueryResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDTO))]
        [SwaggerOperation(
           Summary = "Reporte de solicitudes por agrupación",
           Description = "Permite obtener el conteo de las solicitudes por tipo, plan del afiliado y hospital"
        )]
        public async Task<IActionResult> GetAnalystPerformance([FromQuery] GetAuthorizationDistributionQuery query)
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

                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ErrorMapperHelper.Error(ErrorMessages.InternalServer, e.Message));
            }
        }
    }
}
