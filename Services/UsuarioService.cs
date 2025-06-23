using System.Net.Http.Json;
using System.Text.Json;
using Veterinaria.Models;

namespace Veterinaria.Services
{
    public class UsuarioService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://veterinenis-d7cyg.ondigitalocean.app/api/usuarios";
        private JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public UsuarioService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // CREATE
        public async Task<bool> CrearUsuarioAsync(Usuario usuario)
        {
            var response = await _httpClient.PostAsJsonAsync(BaseUrl, usuario);
            return response.IsSuccessStatusCode;
        }

        // READ
        public async Task<List<Usuario>> ObtenerUsuariosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Usuario>>(BaseUrl);
        }
        public async Task<Usuario> ObtenerUsuarioPorIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Usuario>(options);
            }
            return null;
        }

        public async Task<Usuario> LoginAsync(string email, string password)
        {
            var loginData = new { Email = email, Password = password };
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/login", loginData);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Usuario>(options);
            }
            return null; // O manejar el error de otra manera
        }
        // UPDATE
        public async Task<bool> ActualizarUsuarioAsync(int id, Usuario usuario)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{id}", usuario);
            return response.IsSuccessStatusCode;
        }

        // DELETE
        public async Task<bool> EliminarUsuarioAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
