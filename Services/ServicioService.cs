using System.Net.Http.Json;
using System.Text.Json;
using Veterinaria.Models;

namespace Veterinaria.Services
{
    public class ServicioService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://veterinenis-d7cyg.ondigitalocean.app/api/servicios";
        private JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public ServicioService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // CREATE
        public async Task<bool> CrearServicioAsync(Servicio servicio)
        {
            var response = await _httpClient.PostAsJsonAsync(BaseUrl, servicio);
            return response.IsSuccessStatusCode;
        }

        // READ
        public async Task<List<Servicio>> ObtenerServiciosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Servicio>>(BaseUrl);
        }

        public async Task<Servicio> ObtenerServicioPorIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Servicio>(options);
            }
            return null;
        }

        // UPDATE
        public async Task<bool> ActualizarServicioAsync(int id, Servicio servicio)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{id}", servicio);
            return response.IsSuccessStatusCode;
        }

        // DELETE
        public async Task<bool> EliminarServicioAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
