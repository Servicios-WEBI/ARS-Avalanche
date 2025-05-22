using System.Text.Json.Serialization;

namespace Avalanche.Core.Application.Dtos.Account
{
    public class RegisterResponse
	{
        [JsonIgnore]
        public bool HasError { get; set; }
        [JsonIgnore]
		public string Error { get; set; }
		public bool IsSuccess { get; set; }
    }
}
