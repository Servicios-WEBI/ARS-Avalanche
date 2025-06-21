using Avalanche.Core.Application.Constants;
using Avalanche.Core.Application.Dtos.Affiliate;
using Avalanche.Core.Application.Interfaces.Repositories;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Avalanche.Core.Application.Features.Affiliate.Queries.GetByDocumentNumber
{
    public class GetByDocumentNumberAffiliateQuery : IRequest<GetByDocumentNumberAffiliateQueryResponse>
    {
        [SwaggerParameter(Description = "Número de documento")]
        [Required(ErrorMessage = "Debe de ingresar el número de documento")]
        public string DocumentNumber { get; set; }
    }

    public class GetByDocumentNumberAffiliateQueryHandler : IRequestHandler<GetByDocumentNumberAffiliateQuery, GetByDocumentNumberAffiliateQueryResponse>
    {
        private readonly IAffiliateRepository _affilliateRepository;
        private readonly IPolicyRepository _policyRepository;

        public GetByDocumentNumberAffiliateQueryHandler(IAffiliateRepository affilliateRepository, IPolicyRepository policyRepository)
        {
            _affilliateRepository = affilliateRepository;
            _policyRepository = policyRepository;
        }

        public async Task<GetByDocumentNumberAffiliateQueryResponse> Handle(GetByDocumentNumberAffiliateQuery query, CancellationToken cancellationToken)
        {
            try
            {
                GetByDocumentNumberAffiliateQueryResponse result = new();

                var entity = await _affilliateRepository.GetByDocumentNumberAsync(t => t.DocumentNumber == query.DocumentNumber, new List<Expression<Func<Domain.Entities.Affiliate, object>>>
                {
                    m => m.DocumentType,
                    m => m.Status,
                    m => m.Client,
                    m => m.AffiliatePolicies
                });

                if (entity == null)
                {
                    throw new Exception(ErrorMessages.NotFound);
                }

                Domain.Entities.Policy policy = new();
                if (entity.AffiliatePolicies.Count != 0)
                {
                    policy = await _policyRepository.GetByIdWithIncludeAsync(t => t.Id == entity.AffiliatePolicies[0].PolicyId, new List<Expression<Func<Domain.Entities.Policy, object>>>
                    {
                        m => m.Plan,
                        m => m.Status
                    });
                }
                
                AffiliatePolicyResponseDTO affiliatePolicy = new()
                {
                    PolicyNumber = policy.Number,
                    PolicyStatus = policy.Status?.Name,
                    PlanName = policy.Plan?.Name 
                };

                AffiliateResponseDTO affilliate = new()
                {
                    Id = entity.Id,
                    FirstName = entity.FirstName,
                    MiddleName = entity.MiddleName,
                    LastName = entity.LastName,
                    DocumentType = entity.DocumentType.Name,
                    DocumentNumber = entity.DocumentNumber,
                    BirthDate = entity.BirthDate,
                    AffiliateDate = entity.AffiliateDate,
                    Gender = entity.Gender,
                    Status = entity.Status.Name,
                    IsPrincipal = entity.AffiliatePolicies.Count != 0 ? entity.AffiliatePolicies[0].IsPrincipal : null,
                    ClientId = entity.ClientId,
                    ClientName = entity.Client.FirstName + " " + entity.Client.LastName,
                    Policy = affiliatePolicy
                };

                result.Affiliate = affilliate;

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
