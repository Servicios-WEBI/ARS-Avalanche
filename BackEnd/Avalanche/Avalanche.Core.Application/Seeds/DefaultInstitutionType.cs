using Avalanche.Core.Application.Interfaces.Reposirories;
using Avalanche.Core.Domain.Entities;

namespace Avalanche.Core.Application.Seeds
{
    public static class DefaultInstitutionType
    {
        public static async Task SeedAsync(IInstitutionTypeRepository institutionTypeRepository)
        {
            List<InstitutionType> institutionTypeList = new();
            try
            {
                var anyInstitutionType = await institutionTypeRepository.GetAllAsync();
                if (anyInstitutionType == null || anyInstitutionType.Count == 0)
                {
                    var institutionTypes = new List<string>
                    {
                        "Hospital General",
                        "Clinica Especializada",
                        "Centro de Diagnostico",
                        "Laboratorio Clinico",
                        "Centro de Rehabilitacion",
                        "Urgencias",
                        "Centro de Atencion Primaria",
                        "Hospital Pediatrico",
                        "Hospital Psiquiatrico",
                        "Centro Odontologico",
                        "Centro de Terapias Alternativas",
                        "Institucion de Cuidados Paliativos",
                        "Centro de Imagenologia",
                        "Hospital Militar",
                        "Centro de Atencion Domiciliaria"
                    };

                    foreach (var item in institutionTypes)
                    {
                        InstitutionType institutionType = new()
                        {
                            Name = item
                        };

                        institutionTypeList.Add(institutionType);
                    }

                    await institutionTypeRepository.AddManyAsync(institutionTypeList);
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
