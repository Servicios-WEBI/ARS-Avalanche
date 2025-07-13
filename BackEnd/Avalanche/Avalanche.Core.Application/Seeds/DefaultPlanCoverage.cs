using Avalanche.Core.Application.Dtos.PlanCoverage;
using Avalanche.Core.Application.Interfaces.Repositories;
using Avalanche.Core.Domain.Entities;
using CsvHelper;
using System.Globalization;

namespace Avalanche.Core.Application.Seeds
{
    public static class DefaultPlanCoverage
    {
        public static async Task SeedAsync(IPlanCoverageRepository planCoverageRepository, string csvFilePath, 
            IPlanRepository planRepository, ICoverageRepository coverageRepository)
        {
            List<PlanCoverage> planCoverageList = new();
            try
            {
                var anyPlanCoverage = await planCoverageRepository.GetAllAsync();
                if (anyPlanCoverage == null || anyPlanCoverage.Count == 0)
                {
                    using var reader = new StreamReader(csvFilePath);
                    using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

                    var records = csv.GetRecords<PlanCoverageSeedDTO>().ToList();

                    foreach (var item in records)
                    {
                        PlanCoverage planCoverage = new();
                        var plan = await planRepository.GetByPropertyAsync(p => p.Name == item.PlanName);
                        var coverage = await coverageRepository.GetByPropertyAsync(h => h.Name == item.CoverageName);

                        planCoverage.Id = Guid.NewGuid().ToString().Substring(0, 12);
                        planCoverage.AmountLimit = item.AmountLimit;
                        planCoverage.YearFrequencyLimit = item.YearFrequencyLimit;
                        planCoverage.CoveragePercentage = item.CoveragePercentage;
                        planCoverage.PlanId = plan.Id;
                        planCoverage.CoverageId = coverage.Id;
                        planCoverageList.Add(planCoverage);
                    }

                    await planCoverageRepository.AddManyAsync(planCoverageList);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
