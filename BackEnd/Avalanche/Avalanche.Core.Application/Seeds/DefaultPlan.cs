using AutoMapper;
using Avalanche.Core.Application.Dtos.Plan;
using Avalanche.Core.Application.Interfaces.Repositories;
using Avalanche.Core.Domain.Entities;
using CsvHelper;
using System.Globalization;

namespace Avalanche.Core.Application.Seeds
{
    public static class DefaultPlan
    {
        public static async Task SeedAsync(IPlanRepository planRepository, string csvFilePath, IMapper mapper)
        {
            List<Plan> planList = new();
            try
            {
                var anyPlan = await planRepository.GetAllAsync();
                if (anyPlan == null || anyPlan.Count == 0) 
                {
                    using var reader = new StreamReader(csvFilePath);
                    using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

                    var records = csv.GetRecords<PlanSeedDTO>().ToList();

                    planList = mapper.Map<List<Plan>>(records);

                    planList.ForEach(x => x.Id = Guid.NewGuid().ToString().Substring(0, 12));

                    await planRepository.AddManyAsync(planList);
                }
                else
                {
                    return;
                }
                
            }
            catch (Exception ex) 
            {
                throw;
            }
        }
    }
}
