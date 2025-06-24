using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Veterinaria.Models;
using Newtonsoft.Json;


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
            var json = JsonConvert.SerializeObject(producto);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(BaseUrl, content);
            return response.IsSuccessStatusCode;
        }

        // READ
        public async Task<List<Producto>> ObtenerProductosAsync()
        {
            var response = await _httpClient.GetAsync(BaseUrl);
            Console.WriteLine(response);
            if (!response.IsSuccessStatusCode) return new List<Producto>();
            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine("JSON recibido: " + json);
            try
            {
                var productosResponse = JsonConvert.DeserializeObject<ProductosResponse>(json);
                Console.WriteLine(productosResponse);
                return productosResponse?.Productos ?? new List<Producto>();
               
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error de deserialización JSON: {ex.Message}");
                return new List<Producto>();
            }
        }

        public async Task<Producto> ObtenerProductoPorIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/{id}");
            if (!response.IsSuccessStatusCode) return null;
            var json = await response.Content.ReadAsStringAsync();
            var productoResponse = JsonConvert.DeserializeObject<ProductoResponse>(json);
            return productoResponse?.Producto;
        }

        // UPDATE
        public async Task<bool> ActualizarProductoAsync(int id, Producto producto)
        {
            var json = JsonConvert.SerializeObject(producto);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{BaseUrl}/{id}", content);
            return response.IsSuccessStatusCode;
        }

        // DELETE
        public async Task<bool> EliminarProductoAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }

    // Clase contenedora para la respuesta de la API
    public class ProductoResponse
    {
        [JsonPropertyName("producto")]
        public Producto Producto { get; set; }
    }
}
