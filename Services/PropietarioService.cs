using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Veterinaria.Models;
using Newtonsoft.Json;


namespace Veterinaria.Services
{
    public class PropietarioService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://veterinenis-d7cyg.ondigitalocean.app/api/propietarios";
        private readonly JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = null
        };

        public PropietarioService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // CREATE
        public async Task<bool> CrearPropietarioAsync(Propietario propietario)
        {
            var json = JsonConvert.SerializeObject(propietario);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(BaseUrl, content);
            return response.IsSuccessStatusCode;
        }

        // READ
        public async Task<List<Propietario>> ObtenerPropietariosAsync()
        {
            var response = await _httpClient.GetAsync(BaseUrl);
            if (!response.IsSuccessStatusCode) return new List<Propietario>();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Propietario>>(json) ?? new List<Propietario>();
        }

        public async Task<Propietario> ObtenerPropietarioPorIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/{id}");
            if (!response.IsSuccessStatusCode) return null;
            var json = await response.Content.ReadAsStringAsync();
            var propietarioResponse = JsonConvert.DeserializeObject<PropietarioResponse>(json);
            return propietarioResponse?.Propietario;
        }

        public async Task<List<Propietario>> ObtenerPropietariosPorNombreAsync(string nombre)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/filtrar/{Uri.EscapeDataString(nombre)}");
            if (!response.IsSuccessStatusCode) return new List<Propietario>();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Propietario>>(json) ?? new List<Propietario>();
        }

        // UPDATE
        public async Task<bool> ActualizarPropietarioAsync(int id, Propietario propietario)
        {
            var json = JsonConvert.SerializeObject(propietario);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{BaseUrl}/{id}", content);
            return response.IsSuccessStatusCode;
        }

        // DELETE
        public async Task<bool> EliminarPropietarioAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }

    // Clase contenedora para la respuesta de la API
    public class PropietarioResponse
    {
        [JsonPropertyName("propietario")]
        public Propietario Propietario { get; set; }
    }
}
