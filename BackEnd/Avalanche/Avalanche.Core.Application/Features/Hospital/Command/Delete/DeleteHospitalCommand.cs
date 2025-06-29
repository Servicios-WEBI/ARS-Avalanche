using AutoMapper;
using Avalanche.Core.Application.Constants;
using Avalanche.Core.Application.Dtos.Common;
using Avalanche.Core.Application.Dtos.Hospital;
using Avalanche.Core.Application.Interfaces.Repositories;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Avalanche.Core.Application.Features.Hospital.Command.Delete
{
    public class DeleteHospitalCommand : IRequest<HospitalDTO>
    {
        [SwaggerParameter(Description = "Id")]
        [Required(ErrorMessage = "Debe de ingresar el identificador único")]
        public string Id { get; set; }
    }

    public class DeleteHospitalCommandHandler : IRequestHandler<DeleteHospitalCommand, HospitalDTO>
    {
        private readonly IHospitalRepository _hospitalRepository;
        private readonly IMapper _mapper;

        public DeleteHospitalCommandHandler(IHospitalRepository hospitalRepository, IMapper mapper)
        {
            _hospitalRepository = hospitalRepository;
            _mapper = mapper;
        }

        public async Task<HospitalDTO> Handle(DeleteHospitalCommand command, CancellationToken cancellationToken)
        {
            try
            {
                HospitalDTO response = new();
                var valueToDelete = await _hospitalRepository.GetByIdAsync(command.Id);

                if (valueToDelete == null)
                    throw new Exception(ErrorMessages.NotFound);

                await _hospitalRepository.DeleteAsync(valueToDelete);

                response = _mapper.Map<HospitalDTO>(valueToDelete);
                response.Status = "Exitoso";
                response.Details = [new ErrorDetailsDTO { Code = "000", Message = "Se eliminó correctamente el hospital" }];
                return response;
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
