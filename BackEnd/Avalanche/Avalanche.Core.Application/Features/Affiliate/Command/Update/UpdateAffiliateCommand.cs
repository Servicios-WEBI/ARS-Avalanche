using AutoMapper;
using Avalanche.Core.Application.Constants;
using Avalanche.Core.Application.Dtos.Common;
using Avalanche.Core.Application.Dtos.Affiliate;
using Avalanche.Core.Application.Interfaces.Repositories;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Avalanche.Core.Application.Features.Affiliate.Command.Update
{
    public class UpdateAffiliateCommand : IRequest<AffiliateDTO>
    {
        [SwaggerParameter(Description = "Id")]
        [Required(ErrorMessage = "Debe de ingresar el identificador único")]
        public string Id { get; set; }

        [SwaggerParameter(Description = "Primer nombre")]
        [Required(ErrorMessage = "Debe ingresar el primer nombre")]
        public string FirstName { get; set; }

        [SwaggerParameter(Description = "Segundo nombre")]
        public string? MiddleName { get; set; }

        [SwaggerParameter(Description = "Apellido")]
        [Required(ErrorMessage = "Debe ingresar el apellido")]
        public string LastName { get; set; }

        [SwaggerParameter(Description = "Tipo de documento")]
        [Required(ErrorMessage = "Debe ingresar el tipo de documento")]
        public string DocumentType { get; set; }

        [SwaggerParameter(Description = "Número de documento")]
        [Required(ErrorMessage = "Debe ingresar el número de documento")]
        public string DocumentNumber { get; set; }

        [SwaggerParameter(Description = "Fecha de nacimiento")]
        [Required(ErrorMessage = "Debe ingresar la fecha de nacimiento")]
        public DateOnly BirthDate { get; set; }

        [SwaggerParameter(Description = "Género")]
        [Required(ErrorMessage = "Debe ingresar el género")]
        public string Gender { get; set; }

        [SwaggerParameter(Description = "Estado")]
        [Required(ErrorMessage = "Debe ingresar el nuevo estado")]
        public string Status { get; set; }

        [SwaggerParameter(Description = "Cliente")]
        [Required(ErrorMessage = "Debe ingresar el cliente")]
        public string ClientId { get; set; }
    }

    public class UpdateAffiliateCommandHandler : IRequestHandler<UpdateAffiliateCommand, AffiliateDTO>
    {
        private readonly IAffiliateRepository _affilliateRepository;
        private readonly IDocumentTypeRepository _documentTypeRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly IMapper _mapper;

        public UpdateAffiliateCommandHandler(IAffiliateRepository affilliateRepository, IDocumentTypeRepository documentTypeRepository,
            IStatusRepository statusRepository, IMapper mapper)
        {
            _affilliateRepository = affilliateRepository;
            _documentTypeRepository = documentTypeRepository;
            _statusRepository = statusRepository;
            _mapper = mapper;
        }

        public async Task<AffiliateDTO> Handle(UpdateAffiliateCommand command, CancellationToken cancellationToken)
        {
            try
            {
                AffiliateDTO response = new();

                var valueToUpdate = await _affilliateRepository.GetByIdAsync(command.Id);

                if (valueToUpdate == null)
                    throw new Exception(ErrorMessages.NotFound);

                var documentType = await _documentTypeRepository.GetByNameAsync(command.DocumentType.ToUpper());
                if (documentType == null)
                {
                    throw new Exception($"No se encontró el tipo de documento: {command.DocumentType.ToUpper()}");
                }
                var status = await _statusRepository.GetByNameAsync(command.Status);
                if (documentType == null)
                {
                    throw new Exception($"No se encontró el estado: {command.Status}");
                }

                valueToUpdate.FirstName = command.FirstName;
                valueToUpdate.MiddleName = command.MiddleName;
                valueToUpdate.LastName = command.LastName;
                valueToUpdate.DocumentTypeId = documentType.Id;
                valueToUpdate.DocumentNumber = command.DocumentNumber;
                valueToUpdate.BirthDate = command.BirthDate;
                valueToUpdate.Gender = command.Gender;
                valueToUpdate.StatusId = status.Id;
                valueToUpdate.ClientId = command.ClientId;

                await _affilliateRepository.UpdateAsync(valueToUpdate, valueToUpdate.Id);

                response = _mapper.Map<AffiliateDTO>(valueToUpdate);
                response.DocumentType = command.DocumentType;
                response.AffiliateStatus = command.Status;
                response.Status = "Exitoso";
                response.Details = [new ErrorDetailsDTO { Code = "000", Message = "Se modificó correctamente el afiliado" }];
                return response;
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
