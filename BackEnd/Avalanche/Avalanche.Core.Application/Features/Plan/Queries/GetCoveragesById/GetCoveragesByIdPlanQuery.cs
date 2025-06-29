using Avalanche.Core.Application.Constants;
using Avalanche.Core.Application.Dtos.PlanCoverage;
using Avalanche.Core.Application.Interfaces.Repositories;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Avalanche.Core.Application.Features.Plan.Queries.GetCoveragesById
{
    public class GetCoveragesByIdPlanQuery : IRequest<GetCoveragesByIdPlanQueryResponse>
    {
        [SwaggerParameter(Description = "Identificador único")]
        [Required(ErrorMessage = "Debe de ingresar el identificador único")]
        public string Id { get; set; }
    }

    public class GetCoveragesByIdPlanQueryHandler : IRequestHandler<GetCoveragesByIdPlanQuery, GetCoveragesByIdPlanQueryResponse>
    {
        private readonly IPlanRepository _planRepository;
        private readonly ICoverageRepository _coverageRepository;

        public GetCoveragesByIdPlanQueryHandler(IPlanRepository planRepository, ICoverageRepository coverageRepository)
        {
            _planRepository = planRepository;
            _coverageRepository = coverageRepository;
        }

        public async Task<GetCoveragesByIdPlanQueryResponse> Handle(GetCoveragesByIdPlanQuery query, CancellationToken cancellationToken)
        {
            try
            {
                GetCoveragesByIdPlanQueryResponse result = new();

                var entity = await _planRepository.GetByIdWithIncludeAsync(t => t.Id == query.Id, new List<Expression<Func<Domain.Entities.Plan, object>>>
                {
                    m => m.PlanCoverages
                });

                if (entity == null)
                {
                    throw new Exception(ErrorMessages.NotFound);
                }

                List<PlanCoverageResponseDTO> coverages = new();

                foreach (var item in entity.PlanCoverages)
                {
                    var coverage = await _coverageRepository.GetByIdAsync(item.CoverageId);

                    PlanCoverageResponseDTO response = new()
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
                result.Name = entity.Name;
                result.Description = entity.Description;
                result.MonthlyCost = entity.MonthlyCost;
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
