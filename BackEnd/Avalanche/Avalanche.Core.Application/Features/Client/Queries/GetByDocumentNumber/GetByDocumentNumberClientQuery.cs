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

        public GetByDocumentNumberClientQueryHandler(IClientRepository clientRepository, IPolicyRepository policyRepository, IStatusRepository statusRepository)
        {
            _clientRepository = clientRepository;
            _policyRepository = policyRepository;
            _statusRepository = statusRepository;
        }

        public async Task<GetByDocumentNumberClientQueryResponse> Handle(GetByDocumentNumberClientQuery query, CancellationToken cancellationToken)
        {
            try
            {
                GetByDocumentNumberClientQueryResponse result = new();

                var entity = await _clientRepository.GetByDocumentNumberAsync(t => t.DocumentNumber == query.DocumentNumber, new List<Expression<Func<Domain.Entities.Client, object>>>
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

                var activo = await _statusRepository.GetByNameAsync("Activo");

                Domain.Entities.Policy policy = new();
                if(entity.Policies.Count != 0)
                {
                    policy = await _policyRepository.GetByIdWithIncludeAsync(t => t.Id == entity.Policies[0].Id, new List<Expression<Func<Domain.Entities.Policy, object>>>
                    {
                        m => m.Plan,
                        m => m.Status
                    });
                }
                
                var affiliates = entity.Affiliates.Where(x => (x.DocumentNumber != entity.DocumentNumber) && (x.StatusId == activo.Id)).Select(a => new AffiliatesResponseDTO
                {
                    AffiliateId = a.Id,
                    AffiliateName = a.FirstName + " " + a.LastName,
                    DocumentType = a.DocumentType.Name,
                    DocumentNumber = a.DocumentNumber
                }).ToList();

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
