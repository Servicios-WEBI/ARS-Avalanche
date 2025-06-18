using Avalanche.Core.Application.Dtos.Affiliate;
using Swashbuckle.AspNetCore.Annotations;

namespace Avalanche.Core.Application.Features.Affiliate.Queries.GetByDocumentNumber
{
    public class GetByDocumentNumberAffiliateQueryResponse
    {
        [SwaggerSchema(Description = "Objeto de afiliado")]
        public AffiliateResponseDTO Affiliate { get; set; }
    }
}
