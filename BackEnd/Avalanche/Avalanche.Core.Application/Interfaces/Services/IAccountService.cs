using Avalanche.Core.Application.Dtos.Account;

namespace Avalanche.Core.Application.Interfaces.Services
{
    public interface IAccountService
	{
		Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
		Task<RegisterResponse> RegisterUserAsync(RegisterRequest request);
		Task<RegisterResponse> RegisterAnalystAsync(RegisterRequest request);
        Task<ConfirmEmailResponse> ConfirmEmailAsync(string userId, string token);
		Task<ResetPasswordResponse> ResetPasswordAsync(string email);
		ConfirmCodeResponse ConfirmCode(string code);
		Task<ResetPasswordResponse> ChangePasswordAsync(string password);
		Task<string> GenerateJWToken(string userId);
		string GenerateRefreshToken(string userId);
		string ValidateRefreshToken();
		Task<UserDTO> GetUsersById(string id);
        Task<RegisterResponse> EditProfile(UserDTO user);

    }
}
