using AutoMapper;
using Avalanche.Core.Application.Dtos.Affiliate;
using Avalanche.Core.Application.Interfaces.Repositories;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Avalanche.Core.Application.Features.Affiliate.Queries.GetById
{
    public class GetByIdAffiliateQuery : IRequest<GetByIdAffiliateQueryResponse>
    {
        [SwaggerParameter(Description = "Identificador único")]
        [Required(ErrorMessage = "Debe de ingresar el identificador único")]
        public string Id { get; set; }
    }

    public class GetByIdAffiliateQueryHandler : IRequestHandler<GetByIdAffiliateQuery, GetByIdAffiliateQueryResponse>
    {
        private readonly IAffiliateRepository _affilliateRepository;
        private readonly IPolicyRepository _policyRepository;
        private readonly IMapper _mapper;

        public GetByIdAffiliateQueryHandler(IAffiliateRepository affilliateRepository, IPolicyRepository policyRepository, IMapper mapper)
        {
            _affilliateRepository = affilliateRepository;
            _policyRepository = policyRepository;
            _mapper = mapper;
        }

        public async Task<GetByIdAffiliateQueryResponse> Handle(GetByIdAffiliateQuery query, CancellationToken cancellationToken)
        {
            try
            {
                GetByIdAffiliateQueryResponse result = new();

                var entity = await _affilliateRepository.GetByIdWithIncludeAsync(t => t.Id == query.Id, new List<Expression<Func<Domain.Entities.Affiliate, object>>>
                {
                    m => m.DocumentType,
                    m => m.Status,
                    m => m.Client,
                    m => m.AffiliatePolicies
                });

                var policy = await _policyRepository.GetByIdWithIncludeAsync(t => t.Id == entity.AffiliatePolicies[0].PolicyId, new List<Expression<Func<Domain.Entities.Policy, object>>>
                {
                    m => m.Plan,
                    m => m.Status
                });

                AffiliatePolicyResponseDTO affiliatePolicy = new()
                {
                    PolicyNumber = policy.Number,
                    PolicyStatus = policy.Status.Name,
                    PlanName = policy.Plan.Name
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
