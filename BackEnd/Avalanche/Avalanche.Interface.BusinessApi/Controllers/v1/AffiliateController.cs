using Avalanche.Core.Application.Constants;
using Avalanche.Core.Application.Dtos.Common;
using Avalanche.Core.Application.Dtos.Affiliate;
using Avalanche.Core.Application.Features.Affiliate.Command.Add;
using Avalanche.Core.Application.Features.Affiliate.Command.Delete;
using Avalanche.Core.Application.Features.Affiliate.Command.Update;
using Avalanche.Core.Application.Features.Affiliate.Queries.GetAll;
using Avalanche.Core.Application.Helpers;
using Avalanche.Interface.BusinessAPI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Avalanche.Core.Application.Features.Affiliate.Queries.GetById;
using Avalanche.Core.Application.Features.Affiliate.Queries.GetByDocumentNumber;

namespace Avalanche.Interface.BusinessApi.Controllers.v1
{
    [Route("api/v1/affiliate")]
    [SwaggerTag("Manejo de afiliados")]
    public class AffiliateController : BaseApiController
    {
        [Authorize(Roles = "Administrator, Analyst")]
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAllAffiliateQueryResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDTO))]
        [SwaggerOperation(
           Summary = "Obtener todos los afiliados",
           Description = "Nos permite obtener todos los afiliados disponibles en el sistema"
        )]
        public async Task<IActionResult> GetAffiliates()
        {
            try
            {
                var result = await Mediator.Send(new GetAllAffiliateQuery());

                if (result.Affiliates.Count == 0)
                {
                    return NotFound(ErrorMapperHelper.Error(ErrorMessages.NotFound, "No existen afiliados en el sistema"));
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetByIdAffiliateQueryResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDTO))]
        [SwaggerOperation(
            Summary = "Obtener detalles de afiliado",
            Description = "Nos permite obtener todos los detalles del afiliado"
         )]
        public async Task<IActionResult> GetAffiliate([FromRoute] string id)
        {
            try
            {
                var result = await Mediator.Send(new GetByIdAffiliateQuery() { Id = id });

                return Ok(result);
            }
            catch (Exception e)
            {
                if (e.Message == ErrorMessages.NotFound)
                    return NotFound(ErrorMapperHelper.Error(ErrorMessages.NotFound, "No existe un afiliado con ese identificador único"));

                return StatusCode(StatusCodes.Status500InternalServerError, ErrorMapperHelper.Error(ErrorMessages.InternalServer, e.Message));
            }

        }

        [Authorize(Roles = "Administrator, Analyst")]
        [HttpGet("by-document/{documentNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetByDocumentNumberAffiliateQueryResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDTO))]
        [SwaggerOperation(
            Summary = "Obtener detalles de afiliado por numero de documento",
            Description = "Nos permite obtener todos los detalles del afiliado por numero de documento"
         )]
        public async Task<IActionResult> GetAffiliateByDocumentNumber([FromRoute] string documentNumber)
        {
            try
            {
                var result = await Mediator.Send(new GetByDocumentNumberAffiliateQuery() { DocumentNumber = documentNumber });

                return Ok(result);
            }
            catch (Exception e)
            {
                if (e.Message == ErrorMessages.NotFound)
                    return NotFound(ErrorMapperHelper.Error(ErrorMessages.NotFound, "No existe un afiliado con ese número de documento"));

                return StatusCode(StatusCodes.Status500InternalServerError, ErrorMapperHelper.Error(ErrorMessages.InternalServer, e.Message));
            }

        }

        [Authorize(Roles = "Administrator, Analyst")]
        [HttpPost()]
        [SwaggerOperation(
           Summary = "Crear un afiliado",
           Description = "Nos permite crear un afiliado"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AffiliateDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDTO))]
        public async Task<IActionResult> PostAffiliates([FromBody] AddAffiliateCommand command)
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
           Summary = "Editar un afiliado",
           Description = "Nos permite editar un afiliado"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AffiliateDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDTO))]
        public async Task<IActionResult> PutAffiliates([FromBody] UpdateAffiliateCommand command)
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
                    return NotFound(ErrorMapperHelper.Error(ErrorMessages.NotFound, "No existe un afiliado con ese identificador único"));

                return StatusCode(StatusCodes.Status500InternalServerError, ErrorMapperHelper.Error(ErrorMessages.InternalServer, e.Message));
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        [SwaggerOperation(
           Summary = "Eliminar un afiliado",
           Description = "Nos permite eliminar un afiliado"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AffiliateDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDTO))]
        public async Task<IActionResult> DeleteAffiliates([FromRoute] string id)
        {
            try
            {
                DeleteAffiliateCommand command = new() { Id = id };

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
                    return NotFound(ErrorMapperHelper.Error(ErrorMessages.NotFound, "No existe un afiliado con ese identificador único"));

                return StatusCode(StatusCodes.Status500InternalServerError, ErrorMapperHelper.Error(ErrorMessages.InternalServer, e.Message));
            }
        }
    }
}
