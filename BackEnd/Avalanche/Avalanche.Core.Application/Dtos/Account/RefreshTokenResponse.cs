using System.Text.Json.Serialization;

namespace Avalanche.Core.Application.Dtos.Account
{
    public class RefreshTokenResponse
	{
		[JsonIgnore]
		public bool HasError { get; set; }
		[JsonIgnore]
		public string Error { get; set; }
		public string JWToken { get; set; }
		
	}
}
