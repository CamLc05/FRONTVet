using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Veterinaria.Models;
using Newtonsoft.Json;


namespace Veterinaria.Services
{
    public class FacturaService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://veterinenis-d7cyg.ondigitalocean.app/api/facturas";

        public FacturaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // CREATE
        public async Task<bool> CrearFacturaAsync(Factura factura)
        {
            var json = JsonConvert.SerializeObject(factura);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(BaseUrl, content);
            return response.IsSuccessStatusCode;
        }

        // READ
        public async Task<List<Factura>> ObtenerFacturasAsync()
        {
            var response = await _httpClient.GetAsync(BaseUrl);
            if (!response.IsSuccessStatusCode) return new List<Factura>();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Factura>>(json) ?? new List<Factura>();
        }

        public async Task<Factura> ObtenerFacturaPorIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/{id}");
            if (!response.IsSuccessStatusCode) return null;
            var json = await response.Content.ReadAsStringAsync();
            var facturaResponse = JsonConvert.DeserializeObject<FacturaResponse>(json);
            return facturaResponse?.Factura;
        }

        // UPDATE
        public async Task<bool> ActualizarFacturaAsync(int id, Factura factura)
        {
            var json = JsonConvert.SerializeObject(factura);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{BaseUrl}/{id}", content);
            return response.IsSuccessStatusCode;
        }

        // DELETE
        public async Task<bool> EliminarFacturaAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }

    // Clase contenedora para la respuesta de la API
    public class FacturaResponse
    {
        [JsonPropertyName("factura")]
        public Factura Factura { get; set; }
    }
}
