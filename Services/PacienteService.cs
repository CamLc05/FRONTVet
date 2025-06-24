using System.Net.Http.Json;
using System.Text.Json;
using Veterinaria.Models;

public class PacienteService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "https://veterinenis-d7cyg.ondigitalocean.app/api/pacientes";
    private JsonSerializerOptions options = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    };

    public PacienteService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // CREATE
    public async Task<bool> CrearPacienteAsync(Paciente paciente)
    {
        var response = await _httpClient.PostAsJsonAsync(BaseUrl, paciente);
        return response.IsSuccessStatusCode;
    }

    public async Task<List<Paciente>> ObtenerPacientesPorNombreAsync(string nombre)
    {
        var response = await _httpClient.GetAsync($"{BaseUrl}/filtrar/{Uri.EscapeDataString(nombre)}");
        if (response.IsSuccessStatusCode)
        {
            
            return await response.Content.ReadFromJsonAsync<List<Paciente>>(options);
        }
        return new List<Paciente>();
    }

    public async Task<List<Cita>> ObtenerCitasPorPacienteAsync(int pacienteId)
    {
        var response = await _httpClient.GetAsync($"{BaseUrl}/citas/{pacienteId}");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<Cita>>(options);
        }
        return new List<Cita>();
    }

    public async Task<List<Vacuna>> ObtenerVacunasPorPaciente(int pacienteId)
    {
        var response = await _httpClient.GetAsync($"{BaseUrl}/vacunas/{pacienteId}");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<Vacuna>>(options);
        }
        return new List<Vacuna>();
    }
    // READ
    public async Task<List<Paciente>> ObtenerPacientesAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Paciente>>(BaseUrl);
    }

    // UPDATE
    public async Task<bool> ActualizarPacienteAsync(int id, Paciente paciente)
    {
        var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{id}", paciente);
        return response.IsSuccessStatusCode;
    }

    // DELETE
    public async Task<bool> EliminarPacienteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
        return response.IsSuccessStatusCode;
    }
}
