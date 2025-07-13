using AutoMapper;
using Avalanche.Core.Application.Constants;
using Avalanche.Core.Application.Dtos.Affiliate;
using Avalanche.Core.Application.Dtos.Common;
using Avalanche.Core.Application.Interfaces.Repositories;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Avalanche.Core.Application.Features.Affiliate.Command.Add
{
    public class AddAffiliateCommand : IRequest<AffiliateDTO>
    {
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

        [SwaggerParameter(Description = "Cliente")]
        [Required(ErrorMessage = "Debe ingresar el cliente")]
        public string ClientId { get; set; }

    }

    public class AddAffiliateCommandHandler : IRequestHandler<AddAffiliateCommand, AffiliateDTO>
    {
        private readonly IAffiliateRepository _affilliateRepository;
        private readonly IDocumentTypeRepository _documentTypeRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly IMapper _mapper;

        public AddAffiliateCommandHandler(IAffiliateRepository affilliateRepository, IDocumentTypeRepository documentTypeRepository,
            IStatusRepository statusRepository, IMapper mapper)
        {
            _affilliateRepository = affilliateRepository;
            _documentTypeRepository = documentTypeRepository;
            _statusRepository = statusRepository;
            _mapper = mapper;
        }

        public async Task<AffiliateDTO> Handle(AddAffiliateCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var documentType = await _documentTypeRepository.GetByPropertyAsync(dt => dt.Name == command.DocumentType.ToUpper(), Properties.Name);
                if (documentType == null)
                {
                    throw new Exception($"No se encontró el tipo de documento: {command.DocumentType.ToUpper()}");
                }

                var affiliate = await _affilliateRepository.GetByPropertyAsync(a => a.DocumentNumber == command.DocumentNumber, Properties.DocumentNumber);
                if (affiliate != null)
                {
                    throw new Exception("Ya existe un afiliado con ese número de documento");
                }

                var status = await _statusRepository.GetByPropertyAsync(s => s.Name == Statuses.Active, Properties.Name);

                AffiliateDTO response = new();
                var valueToAdd = _mapper.Map<Domain.Entities.Affiliate>(command);
                valueToAdd.DocumentTypeId = documentType.Id;
                valueToAdd.StatusId = status.Id;
                valueToAdd.AffiliateDate = DateOnly.FromDateTime(DateTime.UtcNow);

                var entity = await _affilliateRepository.AddAsync(valueToAdd);

                response = _mapper.Map<AffiliateDTO>(entity);
                response.DocumentType = command.DocumentType;
                response.AffiliateStatus = "Activo";
                response.Status = "Exitoso";
                response.Details = [new ErrorDetailsDTO { Code = "000", Message = "Se insertó correctamente el afiliado" }];
                return response;
                
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
