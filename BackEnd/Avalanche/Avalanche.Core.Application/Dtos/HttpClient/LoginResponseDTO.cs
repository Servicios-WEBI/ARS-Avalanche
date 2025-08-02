namespace Avalanche.Core.Application.Dtos.HttpClient
{
    public class LoginResponseDTO
    {
        public bool IsSuccess { get; set; }
        public string Token { get; set; }
        public int RolId { get; set; }
    }
}
