using AutoMapper;
using Avalanche.Core.Application.Dtos.Common;
using Avalanche.Core.Application.Dtos.Hospital;
using Avalanche.Core.Application.Interfaces.Repositories;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Avalanche.Core.Application.Features.Hospital.Command.Add
{
    public class AddHospitalCommand : IRequest<HospitalDTO>
    {
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

    public class AddHospitalCommandHandler : IRequestHandler<AddHospitalCommand, HospitalDTO>
    {
        private readonly IHospitalRepository _hospitalRepository;
        private readonly IMapper _mapper;

        public AddHospitalCommandHandler(IHospitalRepository hospitalRepository, IMapper mapper)
        {
            _hospitalRepository = hospitalRepository;
            _mapper = mapper;
        }

        public async Task<HospitalDTO> Handle(AddHospitalCommand command, CancellationToken cancellationToken)
        {
            try
            {
                HospitalDTO response = new();
                var valueToAdd = _mapper.Map<Domain.Entities.Hospital>(command);
                var entity = await _hospitalRepository.AddAsync(valueToAdd);

                response = _mapper.Map<HospitalDTO>(entity);
                response.Status = "Exitoso";
                response.Details = [new ErrorDetailsDTO { Code = "000", Message = "Se insertó correctamente el hospital" }];
                return response;
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
