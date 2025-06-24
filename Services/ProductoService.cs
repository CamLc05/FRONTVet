using System.Net.Http.Json;
using System.Text.Json;
using Veterinaria.Models;

namespace Veterinaria.Services
{
    public class ProductoService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://veterinenis-d7cyg.ondigitalocean.app/api/productos";
        private JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public ProductoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // CREATE
        public async Task<bool> CrearProductoAsync(Producto producto)
        {
            var response = await _httpClient.PostAsJsonAsync(BaseUrl, producto);
            return response.IsSuccessStatusCode;
        }

        // READ
        public async Task<List<Producto>> ObtenerProductosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Producto>>(BaseUrl);
        }
        public async Task<Producto> ObtenerProductoPorIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Producto>(options);
            }
            return null;
        }
        // UPDATE
        public async Task<bool> ActualizarProductoAsync(int id, Producto producto)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{id}", producto);
            return response.IsSuccessStatusCode;
        }

        // DELETE
        public async Task<bool> EliminarProductoAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
