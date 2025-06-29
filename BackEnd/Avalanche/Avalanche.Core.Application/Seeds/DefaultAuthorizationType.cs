using Avalanche.Core.Application.Interfaces.Repositories;
using Avalanche.Core.Domain.Entities;

namespace Avalanche.Core.Application.Seeds
{
    public static class DefaultAuthorizationType
    {
        public static async Task SeedAsync(IAuthorizationTypeRepository authorizationTypeRepository)
        {
            List<AuthorizationType> authorizationTypeList = new();
            try 
            {
                var anyAuthorizationType = await authorizationTypeRepository.GetAllAsync();
                if (anyAuthorizationType == null || anyAuthorizationType.Count == 0)
                {
                    var authorizationTypes = new List<string>
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

                    foreach (var item in authorizationTypes)
                    {
                        AuthorizationType authorizationType = new()
                        {
                            Name = item
                        };

                        authorizationTypeList.Add(authorizationType);
                    }

                    await authorizationTypeRepository.AddManyAsync(authorizationTypeList);
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
