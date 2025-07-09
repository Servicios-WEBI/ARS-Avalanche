using Avalanche.Core.Application.Dtos.PlanCoverage;
using Avalanche.Core.Application.Interfaces.Services;
using Avalanche.Core.Domain.Entities;

namespace Avalanche.Core.Application.Services
{
    public class PlanCoverageComparisonService : IPlanCoverageComparisonService
    {
        public bool AreEqual(PlanCoverage entity, PlanCoverageDTO dto)
        {
            return entity.CoverageId == dto.CoverageId &&
                   entity.AmountLimit == dto.AmountLimit &&
                   entity.YearFrequencyLimit == dto.YearFrequencyLimit &&
                   entity.CoveragePercentage == dto.CoveragePercentage;
        }

        public bool AreListsEqual(List<PlanCoverage> entities, List<PlanCoverageDTO> dtos)
        {
            if (entities.Count() != dtos.Count())
                return false;

            foreach (var dto in dtos)
            {
                var match = entities.FirstOrDefault(e => e.CoverageId == dto.CoverageId);
                if (match == null || !AreEqual(match, dto))
                    return false;
            }

            return true;
        }
    }
}
