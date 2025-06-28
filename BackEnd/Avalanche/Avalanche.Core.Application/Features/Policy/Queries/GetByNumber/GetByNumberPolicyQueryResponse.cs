using Avalanche.Core.Application.Dtos.Policy;
using Swashbuckle.AspNetCore.Annotations;

namespace Avalanche.Core.Application.Features.Policy.Queries.GetByNumber
{
    public class GetByNumberPolicyQueryResponse
    {
        [SwaggerSchema(Description = "Objeto de poliza")]
        public PolicyResponseDTO Policy { get; set; }
    }
}
