using Avalanche.Core.Application.Dtos.HttpClient;

namespace Avalanche.Core.Application.Interfaces.Clients
{
    public interface IHealthStateApiClient
    {
        Task<string> LoginAsync(LoginRequestDTO request, CancellationToken cancellationToken = default);
    }
}
