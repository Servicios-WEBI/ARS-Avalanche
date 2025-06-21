using Avalanche.Core.Application.Dtos.Client;
using Swashbuckle.AspNetCore.Annotations;

namespace Avalanche.Core.Application.Features.Client.Queries.GetByDocumentNumber
{
    public class GetByDocumentNumberClientQueryResponse
    {
        [SwaggerSchema(Description = "Objeto de cliente")]
        public ClientResponseDTO Client { get; set; }
    }
}
