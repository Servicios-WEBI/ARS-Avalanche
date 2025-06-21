using Avalanche.Core.Application.Dtos.Policy;
using Swashbuckle.AspNetCore.Annotations;

namespace Avalanche.Core.Application.Features.Policy.Queries.GetById
{
    public class GetByIdPolicyQueryResponse
    {
        [SwaggerSchema(Description = "Objeto de poliza")]
        public PolicyResponseDTO Policy { get; set; }
    }
}
