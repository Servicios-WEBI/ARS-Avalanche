using AutoMapper;
using Avalanche.Core.Application.Constants;
using Avalanche.Core.Application.Dtos.Common;
using Avalanche.Core.Application.Dtos.Coverage;
using Avalanche.Core.Application.Interfaces.Repositories;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Avalanche.Core.Application.Features.Coverage.Command.Delete
{
    public class DeleteCoverageCommand : IRequest<CoverageDTO>
    {
        [SwaggerParameter(Description = "Id")]
        [Required(ErrorMessage = "Debe de ingresar el identificador único")]
        public string Id { get; set; }
    }

    public class DeleteCoverageCommandHandler : IRequestHandler<DeleteCoverageCommand, CoverageDTO>
    {
        private readonly ICoverageRepository _coverageRepository;
        private readonly IMapper _mapper;

        public DeleteCoverageCommandHandler(ICoverageRepository coverageRepository, IMapper mapper)
        {
            _coverageRepository = coverageRepository;
            _mapper = mapper;
        }

        public async Task<CoverageDTO> Handle(DeleteCoverageCommand command, CancellationToken cancellationToken)
        {
            try
            {
                CoverageDTO response = new();
                var valueToDelete = await _coverageRepository.GetByIdAsync(command.Id);

                if (valueToDelete == null)
                    throw new Exception(ErrorMessages.NotFound);

                await _coverageRepository.DeleteAsync(valueToDelete);

                response = _mapper.Map<CoverageDTO>(valueToDelete);
                response.Status = "Exitoso";
                response.Details = [new ErrorDetailsDTO { Code = "000", Message = "Se eliminó correctamente la cobertura" }];
                return response;
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
