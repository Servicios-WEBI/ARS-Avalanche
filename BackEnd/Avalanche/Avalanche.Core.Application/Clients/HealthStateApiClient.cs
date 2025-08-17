using Avalanche.Core.Application.Constants;
using Avalanche.Core.Application.Dtos.Common;
using Avalanche.Core.Application.Dtos.HttpClient;
using Avalanche.Core.Application.Interfaces.Clients;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
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

        public async Task<LoginResponseDTO> LoginAsync(LoginRequestDTO request, CancellationToken cancellationToken = default)
        {
            LoginResponseDTO token = new();
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/Auth/Login", content, cancellationToken);
                response.EnsureSuccessStatusCode();
                _logger.LogInformation("Se obtuvo el token satisfactoriamente");

                var result = await response.Content.ReadAsStringAsync();
                token = JsonConvert.DeserializeObject<LoginResponseDTO>(result);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Hubo un error al intentar obtener token del API");
                throw;
            }

            return token;
        }

        public async Task<ErrorDetailsDTO> UpdateAuthorizationAsync(int id, UpdateAuthorizationRequestDTO request, string token, CancellationToken cancellationToken = default)
        {
            ErrorDetailsDTO result = new();
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.PutAsync($"/api/Solicitud/{id}/estado", content, cancellationToken);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    result.Code = response.StatusCode.ToString();
                    result.Message = "Hubo un error al tratar de actualizar la solicitud en el hospital";
                    return result;
                }

                result.Code = response.StatusCode.ToString();
                result.Message = "Se actualizó la solicitud en el hospital";
            }
            catch (Exception ex)
            {
                result.Code = ErrorMessages.InternalServer;
                result.Message = "Hubo un error al tratar de actualizar la solicitud en el hospital";
            }

            return result;
        }
    }
}
