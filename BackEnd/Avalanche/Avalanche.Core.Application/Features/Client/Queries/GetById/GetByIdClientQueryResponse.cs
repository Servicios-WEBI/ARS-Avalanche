using Avalanche.Core.Application.Dtos.Client;
using Swashbuckle.AspNetCore.Annotations;

namespace Avalanche.Core.Application.Features.Client.Queries.GetById
{
    public class GetByIdClientQueryResponse
    {
        [SwaggerSchema(Description = "Objeto de cliente")]
        public ClientResponseDTO Client { get; set; }
    }
}
