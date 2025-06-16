using AutoMapper;
using Avalanche.Core.Application.Dtos.Common;
using Avalanche.Core.Application.Dtos.Coverage;
using Avalanche.Core.Application.Interfaces.Repositories;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Avalanche.Core.Application.Features.Coverage.Command.Add
{
    public class AddCoverageCommand : IRequest<CoverageDTO>
    {
        [SwaggerParameter(Description = "Nombre")]
        [Required(ErrorMessage = "Debe de ingresar el nombre de la cobertura")]
        public string Name { get; set; }

        [SwaggerParameter(Description = "Descipción")]
        public string? Description { get; set; }
    }

    public class AddCoverageCommandHandler : IRequestHandler<AddCoverageCommand, CoverageDTO>
    {
        private readonly ICoverageRepository _coverageRepository;
        private readonly IMapper _mapper;

        public AddCoverageCommandHandler(ICoverageRepository coverageRepository, IMapper mapper)
        {
            _coverageRepository = coverageRepository;
            _mapper = mapper;
        }

        public async Task<CoverageDTO> Handle(AddCoverageCommand command, CancellationToken cancellationToken)
        {
            try
            {
                CoverageDTO response = new();
                var valueToAdd = _mapper.Map<Domain.Entities.Coverage>(command);
                var entity = await _coverageRepository.AddAsync(valueToAdd);

                response = _mapper.Map<CoverageDTO>(entity);
                response.Status = "Exitoso";
                response.Details = [new ErrorDetailsDTO { Code = "000", Message = "Se insertó correctamente la cobertura" }];
                return response;
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
