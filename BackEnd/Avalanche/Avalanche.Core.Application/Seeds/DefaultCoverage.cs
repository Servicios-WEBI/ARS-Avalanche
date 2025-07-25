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
                        "CONSULTA GENERAL",
                        "CONSULTA ESPECIALIZADA",
                        "MEDICAMENTOS",
                        "EXAMENES DE LABORATORIO",
                        "ESTUDIOS DE IMAGENES",
                        "HOSPITALIZACION",
                        "PROCEDIMIENTOS QUIRURGICOS",
                        "TERAPIAS DE REHABILITACION",
                        "ATENCION DE URGENCIAS",
                        "MATERNIDAD",
                        "ATENCION ODONTOLOGICA",
                        "ATENCION PREVENTIVA",
                        "VACUNACION",
                        "ENFERMERIA A DOMICILIO",
                        "TRANSPORTE MEDICO",
                        "CUIDADOS PALIATIVOS",
                        "REHABILITACION POSTOPERATORIA",
                        "ATENCION PSICOLOGICA",
                        "TERAPIAS ALTERNATIVAS"
                    };

                    foreach (var item in coverages)
                    {
                        Coverage coverage = new()
                        {
                            Name = item
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
