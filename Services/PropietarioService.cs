using System.Net.Http.Json;
using System.Text.Json;
using Veterinaria.Models;

namespace Veterinaria.Services
{
    public class PropietarioService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://veterinenis-d7cyg.ondigitalocean.app/api/propietarios";
        private JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public PropietarioService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // CREATE
        public async Task<bool> CrearPropietarioAsync(Propietario propietario)
        {
            var response = await _httpClient.PostAsJsonAsync(BaseUrl, propietario);
            return response.IsSuccessStatusCode;
        }

        // READ
        public async Task<List<Propietario>> ObtenerPropietariosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Propietario>>(BaseUrl);
        }

        public async Task<Propietario> ObtenerPropietarioPorIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Propietario>(options);
            }
            return null;
        }

        public async Task<List<Propietario>> ObtenerPropietariosPorNombreAsync(string nombre)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/filtrar/{Uri.EscapeDataString(nombre)}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Propietario>>(options);
            }
            return new List<Propietario>();
        }

        // UPDATE
        public async Task<bool> ActualizarPropietarioAsync(int id, Propietario propietario)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{id}", propietario);
            return response.IsSuccessStatusCode;
        }

        // DELETE
        public async Task<bool> EliminarPropietarioAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
