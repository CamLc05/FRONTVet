using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinaria.Models;

namespace Veterinaria.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://veterinenis-d7cyg.ondigitalocean.app/api";

        public AuthService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<bool> Login(string email, string password)
        {
            try
            {
                var requestData = new { email, password };
                var json = JsonConvert.SerializeObject(requestData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{BaseUrl}/usuarios/login", content);
                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Full Response Content: {responseContent}");
                var result = JsonConvert.DeserializeObject<AuthResponse>(responseContent);
                if (result?.Success == true)
                {
                    Preferences.Set("user", result.Usuario.Nombre);
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
