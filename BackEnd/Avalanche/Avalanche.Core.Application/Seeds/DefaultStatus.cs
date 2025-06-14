using Avalanche.Core.Application.Interfaces.Reposirories;
using Avalanche.Core.Domain.Entities;

namespace Avalanche.Core.Application.Seeds
{
    public static class DefaultStatus
    {
        public static async Task SeedAsync(IStatusRepository statusRepository)
        {
            List<Status> statusList = new();
            try
            {
                var anyStatus = await statusRepository.GetAllAsync();
                if (anyStatus == null || anyStatus.Count == 0)
                {
                    var statuses = new List<string>
                    {
                        "Activo",
                        "Inactivo",
                        "Pendiente",
                        "Aprobado",
                        "Rechazado",
                        "Suspendido",
                        "Vencido",
                        "Anulado"
                    };

                    foreach (var item in statuses)
                    {
                        Status status = new()
                        {
                            Name = item
                        };

                        statusList.Add(status);
                    }

                    await statusRepository.AddManyAsync(statusList);
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
