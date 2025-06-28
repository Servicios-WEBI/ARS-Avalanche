using Avalanche.Core.Application.Constants;
using Avalanche.Core.Application.Dtos.Common;
using Avalanche.Core.Application.Dtos.Policy;
using Avalanche.Core.Application.Interfaces.Repositories;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Avalanche.Core.Application.Features.Policy.Queries.GetByNumber
{
    public class GetByNumberPolicyQuery : IRequest<GetByNumberPolicyQueryResponse>
    {
        [SwaggerParameter(Description = "Número de poliza")]
        [Required(ErrorMessage = "Debe de ingresar el número de poliza")]
        public string Number { get; set; }
    }

    public class GetByNumberPolicyQueryHandler : IRequestHandler<GetByNumberPolicyQuery, GetByNumberPolicyQueryResponse>
    {
        private readonly IPolicyRepository _policyRepository;
        private readonly IAffiliateRepository _affiliateRepository;
        private readonly IStatusRepository _statusRepository;

        public GetByNumberPolicyQueryHandler(IPolicyRepository policyRepository, IAffiliateRepository affiliateRepository, IStatusRepository statusRepository)
        {
            _policyRepository = policyRepository;
            _affiliateRepository = affiliateRepository;
            _statusRepository = statusRepository;
        }

        public async Task<GetByNumberPolicyQueryResponse> Handle(GetByNumberPolicyQuery query, CancellationToken cancellationToken)
        {
            try
            {
                GetByNumberPolicyQueryResponse result = new();

                var entity = await _policyRepository.GetByNumberAsync(t => t.Number == query.Number, new List<Expression<Func<Domain.Entities.Policy, object>>>
                {
                    m => m.Client,
                    m => m.Plan,
                    m => m.Status,
                    m => m.AffiliatePolicies
                });

                if (entity == null)
                {
                    throw new Exception(ErrorMessages.NotFound);
                }

                var activo = await _statusRepository.GetByNameAsync("Activo");

                List<AffiliatesResponseDTO> affiliates = new();
                foreach (var item in entity.AffiliatePolicies.Where(a => !a.IsPrincipal && a.StatusId == activo.Id).ToList())
                {
                    var affiliate = await _affiliateRepository.GetByIdWithIncludeAsync(a => a.Id == item.AffiliateId, new List<Expression<Func<Domain.Entities.Affiliate, object>>>
                    {
                        m => m.DocumentType
                    });

                    AffiliatesResponseDTO response = new()
                    {
                        AffiliateId = affiliate.Id,
                        AffiliateName = affiliate.FirstName + " " + affiliate.LastName,
                        DocumentNumber = affiliate.DocumentNumber,
                        DocumentType = affiliate.DocumentType.Name
                    };

                    affiliates.Add(response);
                }

                PolicyResponseDTO policy = new()
                {
                    Id = entity.Id,
                    Number = entity.Number,
                    EffectiveStartDate = entity.EffectiveStartDate,
                    EffectiveEndDate = entity.EffectiveEndDate,
                    Client = entity.Client.FirstName + " " + entity.Client.LastName,
                    ClientDocumentNumber = entity.Client.DocumentNumber,
                    Plan = entity.Plan.Name,
                    Status = entity.Status.Name,
                    Affiliates = affiliates
                };

                result.Policy = policy;

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
