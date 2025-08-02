using Avalanche.Core.Application.Dtos.Common;
using Avalanche.Core.Application.Dtos.HttpClient;

namespace Avalanche.Core.Application.Interfaces.Services
{
    public interface IHealthStateService
    {
        Task<ErrorDetailsDTO> UpdateAuthorizationAsync(int id, UpdateAuthorizationRequestDTO request, CancellationToken cancellationToken = default);
    }
}
