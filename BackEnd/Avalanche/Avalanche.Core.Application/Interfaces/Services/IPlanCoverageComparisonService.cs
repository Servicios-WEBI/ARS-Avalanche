using Avalanche.Core.Application.Dtos.PlanCoverage;
using Avalanche.Core.Domain.Entities;

namespace Avalanche.Core.Application.Interfaces.Services
{
    public interface IPlanCoverageComparisonService
    {
        bool AreEqual(PlanCoverage entity, PlanCoverageDTO dto);
        bool AreListsEqual(List<PlanCoverage> entities, List<PlanCoverageDTO> dtos);
    }
}
