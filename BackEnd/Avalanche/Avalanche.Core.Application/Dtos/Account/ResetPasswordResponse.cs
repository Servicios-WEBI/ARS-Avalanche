using System.Text.Json.Serialization;

namespace Avalanche.Core.Application.Dtos.Account
{
    public class ResetPasswordResponse
	{
        [JsonIgnore]
        public bool HasError { get; set; }
        [JsonIgnore]
		public string Error { get; set; }
        public bool IsSuccess { get; set; }
    }
}
