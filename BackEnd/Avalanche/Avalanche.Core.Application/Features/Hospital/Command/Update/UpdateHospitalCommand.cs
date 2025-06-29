using AutoMapper;
using Avalanche.Core.Application.Constants;
using Avalanche.Core.Application.Dtos.Common;
using Avalanche.Core.Application.Dtos.Hospital;
using Avalanche.Core.Application.Interfaces.Repositories;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Avalanche.Core.Application.Features.Hospital.Command.Update
{
    public class UpdateHospitalCommand : IRequest<HospitalDTO>
    {
        [SwaggerParameter(Description = "Id")]
        [Required(ErrorMessage = "Debe de ingresar el identificador único")]
        public string Id { get; set; }

        [SwaggerSchema(Description = "Nombre del hospital")]
        [Required(ErrorMessage = "Debe ingresar el nombre del hospital")]
        public string Name { get; set; }

        [SwaggerSchema(Description = "Tipo de institución")]
        [Required(ErrorMessage = "Debe ingresar el tipo de institución")]
        public string InstitutionTypeId { get; set; }

        [SwaggerSchema(Description = "Estado de la institución")]
        [Required(ErrorMessage = "Debe ingresar el estado de la institución")]
        public string StatusId { get; set; }
    }

    public class UpdateHospitalCommandHandler : IRequestHandler<UpdateHospitalCommand, HospitalDTO>
    {
        private readonly IHospitalRepository _hospitalRepository;
        private readonly IMapper _mapper;

        public UpdateHospitalCommandHandler(IHospitalRepository hospitalRepository, IMapper mapper)
        {
            _hospitalRepository = hospitalRepository;
            _mapper = mapper;
        }

        public async Task<HospitalDTO> Handle(UpdateHospitalCommand command, CancellationToken cancellationToken)
        {
            try
            {
                HospitalDTO response = new();

                var valueToUpdate = await _hospitalRepository.GetByIdAsync(command.Id);

                if (valueToUpdate == null)
                    throw new Exception(ErrorMessages.NotFound);

                valueToUpdate.Name = command.Name;
                valueToUpdate.InstitutionTypeId = command.InstitutionTypeId;
                valueToUpdate.StatusId = command.StatusId;

                await _hospitalRepository.UpdateAsync(valueToUpdate, valueToUpdate.Id);

                response = _mapper.Map<HospitalDTO>(valueToUpdate);
                response.Status = "Exitoso";
                response.Details = [new ErrorDetailsDTO { Code = "000", Message = "Se modificó correctamente el hospital" }];
                return response;
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
