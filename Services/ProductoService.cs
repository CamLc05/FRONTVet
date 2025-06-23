using System.Net.Http.Json;
using Veterinaria.Models;

namespace Veterinaria.Services
{
    public class ProductoService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://veterinenis-d7cyg.ondigitalocean.app/api/productos";

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
