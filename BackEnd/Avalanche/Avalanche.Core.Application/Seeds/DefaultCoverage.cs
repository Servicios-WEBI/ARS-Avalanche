using Avalanche.Core.Application.Interfaces.Repositories;
using Avalanche.Core.Domain.Entities;

namespace Avalanche.Core.Application.Seeds
{
    public static class DefaultCoverage
    {
        public static async Task SeedAsync(ICoverageRepository coverageRepository)
        {
            List<Coverage> coverageList = new();
            try
            {
                var anyCoverage = await coverageRepository.GetAllAsync();
                if (anyCoverage == null || anyCoverage.Count == 0)
                {
                    var coverages = new List<string>
                    {
                        "Consulta General",
                        "Consulta Especializada",
                        "Medicamentos",
                        "Examenes de Laboratorio",
                        "Estudios de Imagenes",
                        "Hospitalizacion",
                        "Procedimientos Quirurgicos",
                        "Terapias de Rehabilitacion",
                        "Atencion de Urgencias",
                        "Maternidad",
                        "Atencion Odontologica",
                        "Atencion Preventiva",
                        "Vacunacion",
                        "Enfermeria a Domicilio",
                        "Transporte Medico",
                        "Cuidados Paliativos",
                        "Rehabilitacion Postoperatoria",
                        "Atencion Psicologica",
                        "Terapias Alternativas"
                    };

                    foreach (var item in coverages)
                    {
                        Coverage coverage = new()
                        {
                            Name = item,
                            Description = item,
                        };

                        coverageList.Add(coverage);
                    }

                    await coverageRepository.AddManyAsync(coverageList);
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
