using System.Net.Http.Json;
using System.Text.Json;
using Veterinaria.Models;

namespace Veterinaria.Services
{
    public class CitaService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://veterinenis-d7cyg.ondigitalocean.app/api/citas";
        private JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public CitaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // CREATE
        public async Task<bool> CrearCitaAsync(Cita cita)
        {
            var response = await _httpClient.PostAsJsonAsync(BaseUrl, cita);
            return response.IsSuccessStatusCode;
        }

        // READ
        public async Task<List<Cita>> ObtenerCitasAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Cita>>(BaseUrl);
        }

        public async Task<Cita> ObtenerCitaPorIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Cita>(options);
            }
            return null;
        }

        // UPDATE
        public async Task<bool> ActualizarCitaAsync(int id, Cita cita)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{id}", cita);
            return response.IsSuccessStatusCode;
        }

        // DELETE
        public async Task<bool> EliminarCitaAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
