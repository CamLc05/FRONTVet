using System.Net.Http.Json;
using Veterinaria.Models;

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
            var response = await _httpClient.PostAsJsonAsync(BaseUrl, factura);
            return response.IsSuccessStatusCode;
        }

        // READ
        public async Task<List<Factura>> ObtenerFacturasAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Factura>>(BaseUrl);
        }

        // UPDATE
        public async Task<bool> ActualizarFacturaAsync(int id, Factura factura)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{id}", factura);
            return response.IsSuccessStatusCode;
        }

        // DELETE
        public async Task<bool> EliminarFacturaAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
