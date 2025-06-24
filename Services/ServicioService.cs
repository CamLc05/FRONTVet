using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Veterinaria.Models;
using Newtonsoft.Json;


namespace Veterinaria.Services
{
    public class ServicioService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://veterinenis-d7cyg.ondigitalocean.app/api/servicios";
        

        public ServicioService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // CREATE
        public async Task<bool> CrearServicioAsync(Servicio servicio)
        {
            var json = JsonConvert.SerializeObject(servicio);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(BaseUrl, content);
            return response.IsSuccessStatusCode;
        }

        // READ
        public async Task<List<Servicio>> ObtenerServiciosAsync()
        {
            var response = await _httpClient.GetAsync(BaseUrl);
            if (!response.IsSuccessStatusCode) return new List<Servicio>();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Servicio>>(json) ?? new List<Servicio>();
        }

        public async Task<Servicio> ObtenerServicioPorIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/{id}");
            if (!response.IsSuccessStatusCode) return null;
            var json = await response.Content.ReadAsStringAsync();
            var servicioResponse = JsonConvert.DeserializeObject<ServicioResponse>(json);
            return servicioResponse?.Servicio;
        }

        // UPDATE
        public async Task<bool> ActualizarServicioAsync(int id, Servicio servicio)
        {
            var json = JsonConvert.SerializeObject(servicio);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{BaseUrl}/{id}", content);
            return response.IsSuccessStatusCode;
        }

        // DELETE
        public async Task<bool> EliminarServicioAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }

    // Clase contenedora para la respuesta de la API
    public class ServicioResponse
    {
        [JsonPropertyName("servicio")]
        public Servicio Servicio { get; set; }
    }
}
