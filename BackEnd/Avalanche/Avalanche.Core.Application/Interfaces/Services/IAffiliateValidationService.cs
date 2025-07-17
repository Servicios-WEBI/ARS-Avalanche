using Avalanche.Core.Application.Dtos.Common;

namespace Avalanche.Core.Application.Interfaces.Services
{
    public interface IAffiliateValidationService
    {
        Task<AffiliateValidationResult> ValidateAsync(string? documentType, string documentNumber,
            string? policyNumber, CancellationToken ct = default);
    }
}
