using Avalanche.Core.Application.Interfaces.Reposirories;
using Avalanche.Core.Domain.Entities;

namespace Avalanche.Core.Application.Seeds
{
    public static class DefaultDocumentType
    {
        public static async Task SeedAsync(IDocumentTypeRepository documentTypeRepository)
        {
            List<DocumentType> documentTypeList = new();
            try
            {
                var anyDocumentType = await documentTypeRepository.GetAllAsync();
                if (anyDocumentType == null || anyDocumentType.Count == 0)
                {
                    var documentTypes = new List<string>
                    {
                        "Cedula",
                        "Pasaporte",
                        "RNC"
                    };

                    foreach (var item in documentTypes)
                    {
                        DocumentType documentType = new()
                        {
                            Name = item
                        };

                        documentTypeList.Add(documentType);
                    }

                    await documentTypeRepository.AddManyAsync(documentTypeList);
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
