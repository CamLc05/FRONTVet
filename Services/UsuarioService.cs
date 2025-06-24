using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Veterinaria.Models;
using Newtonsoft.Json;


namespace Veterinaria.Services
{
    public class UsuarioService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://veterinenis-d7cyg.ondigitalocean.app/api/usuarios";
        private readonly JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = null,
            Converters =
            {
                new JsonStringEnumConverter()
            }
        };

        public UsuarioService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // CREATE
        public async Task<bool> CrearUsuarioAsync(Usuario usuario)
        {
            var json = JsonConvert.SerializeObject(usuario);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(BaseUrl, content);
            return response.IsSuccessStatusCode;
        }

        public class LoginResponse
        {
            [JsonPropertyName("success")]
            public bool Success { get; set; }

            [JsonPropertyName("usuario")]
            public Usuario Usuario { get; set; }

            [JsonPropertyName("token")]
            public string Token { get; set; }
        }

        public async Task<List<Usuario>> ObtenerVeterinariosAsync()
        {
            var response = await _httpClient.GetAsync(BaseUrl);
            if (!response.IsSuccessStatusCode) return new List<Usuario>();
            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine("JSON: " + json);
            var usuarios = JsonConvert.DeserializeObject<List<Usuario>>(json) ?? new List<Usuario>();
            Console.WriteLine("USUARIOS: " + usuarios);
            var vet = usuarios.Where(u => u.Rol == Rol.veterinario).ToList();
            Console.WriteLine(vet);
            // Filtra solo los que tienen el rol 'veterinario'
            return vet;
        }

        public async Task<Usuario> LoginAsync(string email, string password)
        {
            var loginData = new { nombre_usuario = email, contrasena = password };
            var json = JsonConvert.SerializeObject(loginData);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{BaseUrl}/login", content);

            if (response.IsSuccessStatusCode)
            {
                var responseJson = await response.Content.ReadAsStringAsync();
                var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseJson);
                return loginResponse?.Usuario;
            }
            return null;
        }

        // UPDATE
        public async Task<bool> ActualizarUsuarioAsync(int id, Usuario usuario)
        {
            var json = JsonConvert.SerializeObject(usuario);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{BaseUrl}/{id}", content);
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
