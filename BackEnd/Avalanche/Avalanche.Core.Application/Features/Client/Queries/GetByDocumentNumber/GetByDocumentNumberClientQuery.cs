using Avalanche.Core.Application.Constants;
using Avalanche.Core.Application.Dtos.Client;
using Avalanche.Core.Application.Dtos.Common;
using Avalanche.Core.Application.Interfaces.Repositories;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Avalanche.Core.Application.Features.Client.Queries.GetByDocumentNumber
{
    public class GetByDocumentNumberClientQuery : IRequest<GetByDocumentNumberClientQueryResponse>
    {
        [SwaggerParameter(Description = "Número de documento")]
        [Required(ErrorMessage = "Debe de ingresar el número de documento")]
        public string DocumentNumber { get; set; }
    }

    public class GetByDocumentNumberClientQueryHandler : IRequestHandler<GetByDocumentNumberClientQuery, GetByDocumentNumberClientQueryResponse>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IPolicyRepository _policyRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly IDocumentTypeRepository _documentRepository;

        public GetByDocumentNumberClientQueryHandler(IClientRepository clientRepository, IPolicyRepository policyRepository,
            IStatusRepository statusRepository, IDocumentTypeRepository documentRepository)
        {
            _clientRepository = clientRepository;
            _policyRepository = policyRepository;
            _statusRepository = statusRepository;
            _documentRepository = documentRepository;
        }

        public async Task<GetByDocumentNumberClientQueryResponse> Handle(GetByDocumentNumberClientQuery query, CancellationToken cancellationToken)
        {
            try
            {
                GetByDocumentNumberClientQueryResponse result = new();
                List<AffiliatesResponseDTO> affiliates = new();

                var entity = await _clientRepository.GetByPropertyWithIncludeAsync(t => t.DocumentNumber == query.DocumentNumber,
                    new List<Expression<Func<Domain.Entities.Client, object>>>
                {
                    m => m.DocumentType,
                    m => m.Policies,
                    m => m.Status,
                    m => m.Affiliates
                });

                if (entity == null)
                {
                    throw new Exception(ErrorMessages.NotFound);
                }

                var active = await _statusRepository.GetByPropertyAsync(s => s.Name == Statuses.Active);

                Domain.Entities.Policy policy = new();
                if (entity.Policies.Count != 0)
                {
                    policy = await _policyRepository.GetByIdWithIncludeAsync(t => t.Id == entity.Policies[0].Id, new List<Expression<Func<Domain.Entities.Policy, object>>>
                    {
                        m => m.Plan,
                        m => m.Status
                    });
                }

                foreach (var item in entity.Affiliates.Where(x => (x.DocumentNumber != entity.DocumentNumber) && (x.StatusId == active.Id)))
                {
                    if (item.DocumentType == null)
                    {
                        item.DocumentType = await _documentRepository.GetByIdAsync(item.DocumentTypeId);
                    }

                    AffiliatesResponseDTO dto = new()
                    {
                        AffiliateId = item.Id,
                        AffiliateName = item.FirstName + " " + item.LastName,
                        DocumentType = item.DocumentType.Name,
                        DocumentNumber = item.DocumentNumber
                    };

                    affiliates.Add(dto);
                }

                ClientResponseDTO client = new()
                {
                    Id = entity.Id,
                    FirstName = entity.FirstName,
                    MiddleName = entity.MiddleName,
                    LastName = entity.LastName,
                    DocumentType = entity.DocumentType.Name,
                    DocumentNumber = entity.DocumentNumber,
                    Phone = entity.Phone,
                    Email = entity.Email,
                    CustomerSince = entity.CustomerSince,
                    Status = entity.Status.Name,
                    Address = entity.Address,
                    PolicyNumber = policy.Number,
                    PolicyStatus = policy.Status?.Name,
                    PolicyPlan = policy.Plan?.Name,
                    Affiliates = affiliates
                };

                result.Client = client;

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
