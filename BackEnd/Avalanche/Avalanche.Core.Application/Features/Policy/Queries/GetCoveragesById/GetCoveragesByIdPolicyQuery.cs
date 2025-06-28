using Avalanche.Core.Application.Constants;
using Avalanche.Core.Application.Dtos.Common;
using Avalanche.Core.Application.Dtos.Policy;
using Avalanche.Core.Application.Interfaces.Repositories;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Avalanche.Core.Application.Features.Policy.Queries.GetCoveragesById
{
    public class GetCoveragesByIdPolicyQuery : IRequest<GetCoveragesByIdPolicyQueryResponse>
    {
        [SwaggerParameter(Description = "Identificador único")]
        [Required(ErrorMessage = "Debe de ingresar el identificador único")]
        public string Id { get; set; }
    }

    public class GetCoveragesByIdPolicyQueryHandler : IRequestHandler<GetCoveragesByIdPolicyQuery, GetCoveragesByIdPolicyQueryResponse>
    {
        private readonly IPolicyRepository _policyRepository;
        private readonly IPlanRepository _planRepository;
        private readonly ICoverageRepository _coverageRepository;

        public GetCoveragesByIdPolicyQueryHandler(IPolicyRepository policyRepository, ICoverageRepository coverageRepository,
            IPlanRepository planRepository)
        {
            _policyRepository = policyRepository;
            _planRepository = planRepository;
            _coverageRepository = coverageRepository;
        }

        public async Task<GetCoveragesByIdPolicyQueryResponse> Handle(GetCoveragesByIdPolicyQuery query, CancellationToken cancellationToken)
        {
            try
            {
                GetCoveragesByIdPolicyQueryResponse result = new();

                var entity = await _policyRepository.GetByIdWithIncludeAsync(t => t.Id == query.Id, new List<Expression<Func<Domain.Entities.Policy, object>>>
                {
                    m => m.Plan
                });

                if (entity == null)
                {
                    throw new Exception(ErrorMessages.NotFound);
                }

                var plan = await _planRepository.GetByIdWithIncludeAsync(t => t.Id == entity.PlanId, new List<Expression<Func<Domain.Entities.Plan, object>>>
                {
                    m => m.PlanCoverages
                });

                List<GetCoveragesByIdPolicyQueryResponseChild> coverages = new();

                foreach (var item in plan.PlanCoverages)
                {
                    var coverage = await _coverageRepository.GetByIdAsync(item.CoverageId);

                    GetCoveragesByIdPolicyQueryResponseChild response = new()
                    {
                        Name = coverage.Name,
                        Description = coverage.Description,
                        AmountLimit = item.AmountLimit,
                        YearFrequencyLimit = item.YearFrequencyLimit,
                        CoveragePercentage = item.CoveragePercentage
                    };

                    coverages.Add(response);
                }

                result.Id = entity.Id;
                result.Number = entity.Number;
                result.Plan = entity.Plan.Name;
                result.Coverages = coverages;

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
