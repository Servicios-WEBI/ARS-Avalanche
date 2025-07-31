namespace Avalanche.Core.Application.Interfaces.Services
{
    public interface ITokenService
	{
        Task<string> GetTokenAsync(CancellationToken cancellationToken = default);
    }
}
