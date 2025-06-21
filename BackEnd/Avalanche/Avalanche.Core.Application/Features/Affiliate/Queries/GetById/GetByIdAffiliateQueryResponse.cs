using Avalanche.Core.Application.Dtos.Affiliate;
using Swashbuckle.AspNetCore.Annotations;

namespace Avalanche.Core.Application.Features.Affiliate.Queries.GetById
{
    public class GetByIdAffiliateQueryResponse
    {
        [SwaggerSchema(Description = "Objeto de afiliado")]
        public AffiliateResponseDTO Affiliate { get; set; }
    }
}
