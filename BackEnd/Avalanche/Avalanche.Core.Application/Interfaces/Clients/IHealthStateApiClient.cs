using Avalanche.Core.Application.Dtos.Common;
using Avalanche.Core.Application.Dtos.HttpClient;

namespace Avalanche.Core.Application.Interfaces.Clients
{
    public interface IHealthStateApiClient
    {
        Task<LoginResponseDTO> LoginAsync(LoginRequestDTO request, CancellationToken cancellationToken = default);
        Task<ErrorDetailsDTO> UpdateAuthorizationAsync(int id, UpdateAuthorizationRequestDTO request, string token, CancellationToken cancellationToken = default);
    }
}
