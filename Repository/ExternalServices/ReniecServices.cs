using AplicationLayer;
using EnterpriseLayer;
using Mappers.Dtos.Response;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Repository.ExternalServices
{
    public class ReniecServices : IReniecService
    {
        private readonly HttpClient _httpClient;
        private readonly string _token;

        private const string RENIEC_URL = "https://api.apis.net.pe/v2/reniec/dni?numero=";

        public ReniecServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _token = Environment.GetEnvironmentVariable("RENIEC_API_TOKEN")
                     ?? throw new InvalidOperationException("El token de la API de RENIEC no está configurado.");
        }

        public async Task<People> GetPersonDataByDNIAsync(string dni)
        {
            var url = $"{RENIEC_URL}{dni}";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error en la solicitud: {response.StatusCode}");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var reniecResponse = JsonConvert.DeserializeObject<ReniecResponseDTO>(jsonResponse);

            return new People(
                reniecResponse?.NumeroDocumento ?? "",
                reniecResponse?.Nombres ?? "",
                (reniecResponse?.ApellidoPaterno) + " " + (reniecResponse?.ApellidoMaterno),
                ""
            );
        }
    }
}
