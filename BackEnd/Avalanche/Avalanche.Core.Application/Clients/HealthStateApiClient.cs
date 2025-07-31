using Avalanche.Core.Application.Dtos.HttpClient;
using Avalanche.Core.Application.Interfaces.Clients;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;

namespace Avalanche.Core.Application.Clients
{
    public class HealthStateApiClient : IHealthStateApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<HealthStateApiClient> _logger;

        public HealthStateApiClient(HttpClient httpClient, ILogger<HealthStateApiClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<string> LoginAsync(LoginRequestDTO request, CancellationToken cancellationToken = default)
        {
            string token = "";
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/Auth/Login", content, cancellationToken);
                response.EnsureSuccessStatusCode();
                _logger.LogInformation("Se obtuvo el token satisfactoriamente");

                token = await response.Content.ReadAsStringAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Hubo un error al intentar obtener token del API");
                throw;
            }

            return token;
        }
    }
}
