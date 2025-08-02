using Avalanche.Core.Application.Dtos.HttpClient;
using Avalanche.Core.Application.Interfaces.Clients;
using Avalanche.Core.Application.Interfaces.Services;
using Avalanche.Core.Domain.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Avalanche.Core.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly IHealthStateApiClient _healthStateApi;
        private readonly HealthStateApiSettings _apiSettings;
        private string _token;
        private DateTime _expiresAt;

        public TokenService(IHealthStateApiClient healthStateApi, IOptions<HealthStateApiSettings> options)
        {
            _healthStateApi = healthStateApi;
            _apiSettings = options.Value;
        }

        public async Task<string> GetTokenAsync(CancellationToken cancellationToken = default)
        {
            if (!string.IsNullOrEmpty(_token) && DateTime.UtcNow < _expiresAt)
                return _token;

            LoginRequestDTO dto = new()
            {
                Usuario = _apiSettings.Username,
                Clave = _apiSettings.Password
            };

            // Aquí pones tus credenciales reales o las obtienes de la configuración
            var result = await _healthStateApi.LoginAsync(dto, cancellationToken);

            _token = result.Token;
            _expiresAt = GetExpirationFromJwt(_token);

            return _token;
        }

        private DateTime GetExpirationFromJwt(string jwt)
        {
            var parts = jwt.Split('.');
            if (parts.Length < 2) throw new Exception("Token JWT inválido");
            var payload = parts[1];
            var json = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(PadBase64(payload)));
            var claims = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            var exp = Convert.ToInt64(claims["exp"]);
            // exp es en segundos desde epoch
            return DateTimeOffset.FromUnixTimeSeconds(exp).UtcDateTime.AddSeconds(-60); // margen de 1 minuto
        }

        private string PadBase64(string base64)
        {
            // pad base64 string if necessary
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return base64.Replace('-', '+').Replace('_', '/');
        }
    }
}
