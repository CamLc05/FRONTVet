using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Veterinaria.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


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
            var dto = MapProductoToDto(producto);
            var json = JsonConvert.SerializeObject(dto);
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
            var dto = MapProductoToDto(producto);
            Console.WriteLine(dto);
            var json = JsonConvert.SerializeObject(dto);
            Console.WriteLine(json);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{BaseUrl}/{id}", content);
            Console.WriteLine(response);
            return response.IsSuccessStatusCode;

        }
        private ProductoRequestDto MapProductoToDto(Producto producto)
        {
            return new ProductoRequestDto
            {
                Nombre = producto.Nombre,
                Gramaje = producto.Gramaje,
                Precio = producto.Precio,
                Tipo = producto.Tipo,
                Fecha_vencimiento = producto.Fecha_vencimiento,
                Disponibilidad = producto.Disponibilidad
            };
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

    public class ProductoRequestDto
    {
        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("gramaje")]
        public string Gramaje { get; set; }

        [JsonProperty("precio")]
        public float Precio { get; set; }
        [JsonProperty("tipo")]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public TipoProducto Tipo { get; set; }

        [JsonProperty("fecha_vencimiento")]
        public DateTimeOffset? Fecha_vencimiento { get; set; }

        [JsonProperty("disponibilidad")]
        public int Disponibilidad { get; set; }
    }
}
