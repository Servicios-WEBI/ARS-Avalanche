using AutoMapper;
using Avalanche.Core.Application.Constants;
using Avalanche.Core.Application.Dtos.Affiliate;
using Avalanche.Core.Application.Dtos.Common;
using Avalanche.Core.Application.Interfaces.Repositories;
using MediatR;

namespace Avalanche.Core.Application.Features.Affiliate.Command.Add
{
    public class AddAffiliateCommand : AffiliateRequestDTO, IRequest<AffiliateDTO>
    {

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
                var documentType = await _documentTypeRepository.GetByPropertyAsync(dt => dt.Name == command.DocumentType.ToUpper());
                if (documentType == null)
                {
                    throw new Exception($"No se encontró el tipo de documento: {command.DocumentType.ToUpper()}");
                }

                var affiliate = await _affilliateRepository.GetByPropertyAsync(a => a.DocumentNumber == command.DocumentNumber);
                if (affiliate != null)
                {
                    throw new Exception("Ya existe un afiliado con ese número de documento");
                }

                var status = await _statusRepository.GetByPropertyAsync(s => s.Name == Statuses.Active);

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
