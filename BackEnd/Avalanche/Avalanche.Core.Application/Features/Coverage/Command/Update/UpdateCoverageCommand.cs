using AutoMapper;
using Avalanche.Core.Application.Constants;
using Avalanche.Core.Application.Dtos.Common;
using Avalanche.Core.Application.Dtos.Coverage;
using Avalanche.Core.Application.Interfaces.Repositories;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Avalanche.Core.Application.Features.Coverage.Command.Update
{
    public class UpdateCoverageCommand : IRequest<CoverageDTO>
    {
        [SwaggerParameter(Description = "Id")]
        [Required(ErrorMessage = "Debe de ingresar el identificador único")]
        public string Id { get; set; }

        [SwaggerParameter(Description = "Nombre")]
        [Required(ErrorMessage = "Debe de ingresar el nombre de la cobertura")]
        public string Name { get; set; }

        [SwaggerParameter(Description = "Descipción")]
        public string? Description { get; set; }
    }

    public class UpdateCoverageCommandHandler : IRequestHandler<UpdateCoverageCommand, CoverageDTO>
    {
        private readonly ICoverageRepository _coverageRepository;
        private readonly IMapper _mapper;

        public UpdateCoverageCommandHandler(ICoverageRepository coverageRepository, IMapper mapper)
        {
            _coverageRepository = coverageRepository;
            _mapper = mapper;
        }

        public async Task<CoverageDTO> Handle(UpdateCoverageCommand command, CancellationToken cancellationToken)
        {
            try
            {
                CoverageDTO response = new();

                var valueToUpdate = await _coverageRepository.GetByIdAsync(command.Id);

                if (valueToUpdate == null)
                    throw new Exception(ErrorMessages.NotFound);

                valueToUpdate.Name = command.Name;
                valueToUpdate.Description = command.Description;

                await _coverageRepository.UpdateAsync(valueToUpdate, valueToUpdate.Id);

                response = _mapper.Map<CoverageDTO>(valueToUpdate);
                response.Status = "Exitoso";
                response.Details = [new ErrorDetailsDTO { Code = "000", Message = "Se modificó correctamente la cobertura" }];
                return response;
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
