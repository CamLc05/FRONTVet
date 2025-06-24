using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Veterinaria.Models;
using Newtonsoft.Json;
using Veterinaria.ViewModels;


namespace Veterinaria.Services
{
    public class CitaService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://veterinenis-d7cyg.ondigitalocean.app/api/citas";
        

        public CitaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // CREATE
        public async Task<bool> CrearCitaAsync(CitaRequestDto cita)
        {
            var json = JsonConvert.SerializeObject(cita);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(BaseUrl, content);
            return response.IsSuccessStatusCode;
        }

        // READ
        public async Task<List<Cita>> ObtenerCitasAsync()
        {
            var response = await _httpClient.GetAsync(BaseUrl);
            if (!response.IsSuccessStatusCode) return new List<Cita>();
            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine("JSON recibido: " + json);
            return JsonConvert.DeserializeObject<List<Cita>>(json);
        }

        public async Task<Cita> ObtenerCitaPorIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/{id}");
            if (!response.IsSuccessStatusCode) return null;
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Cita>(json);
        }


        // UPDATE
        public async Task<bool> ActualizarCitaAsync(int id, CitaRequestDto cita)
        {
            var json = JsonConvert.SerializeObject(cita);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{BaseUrl}/{id}", content);
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
