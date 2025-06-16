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
                        "Consulta Medica",
                        "Procedimiento Quirurgico",
                        "Examen de Laboratorio",
                        "Estudio de Imagenes",
                        "Medicamento",
                        "Hospitalizacion",
                        "Terapia Fisica",
                        "Atencion de Urgencia",
                        "Seguimiento Postoperatorio"
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
