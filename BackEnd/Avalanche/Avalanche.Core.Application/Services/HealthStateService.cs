using Avalanche.Core.Application.Dtos.Common;
using Avalanche.Core.Application.Dtos.HttpClient;
using Avalanche.Core.Application.Interfaces.Clients;
using Avalanche.Core.Application.Interfaces.Services;

namespace Avalanche.Core.Application.Services
{
    public class HealthStateService : IHealthStateService
    {
        private readonly ITokenService _tokenService;
        private readonly IHealthStateApiClient _apiClient;

        public HealthStateService(ITokenService tokenService, IHealthStateApiClient apiClient)
        {
            _tokenService = tokenService;
            _apiClient = apiClient;
        }

        public async Task<ErrorDetailsDTO> UpdateAuthorizationAsync(int id, UpdateAuthorizationRequestDTO request, CancellationToken cancellationToken = default)
        {
            var token = await _tokenService.GetTokenAsync(cancellationToken);
            var result = await _apiClient.UpdateAuthorizationAsync(id, request, token, cancellationToken);
            return result;
        }
    }
}
