namespace Avalanche.Core.Application.Dtos.Common
{
    public class AffiliateValidationResult : ErrorDTO
    {
        public Domain.Entities.Affiliate? Affiliate { get; set; }
        public Domain.Entities.Policy? Policy { get; set; }
    }
}
