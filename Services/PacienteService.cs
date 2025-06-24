using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Veterinaria.Models;
using Newtonsoft.Json;

public class PacienteService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "https://veterinenis-d7cyg.ondigitalocean.app/api/pacientes";
    private readonly JsonSerializerOptions options = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = null,
        Converters =
        {
            new JsonStringEnumConverter(),
            new FotoPerfilBufferConverter()
        }
    };

    public PacienteService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // CREATE
    public async Task<bool> CrearPacienteAsync(Paciente paciente)
    {
        var dto = MapPacienteToDto(paciente);
        var json = System.Text.Json.JsonSerializer.Serialize(dto);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(BaseUrl, content);
        return response.IsSuccessStatusCode;
    }

    // READ
    public async Task<List<Paciente>> ObtenerPacientesAsync()
    {
        var response = await _httpClient.GetAsync(BaseUrl);
        Console.WriteLine(response);
        if (!response.IsSuccessStatusCode) return new List<Paciente>();
        var json = await response.Content.ReadAsStringAsync();
        Console.WriteLine("JSON recibido: " + json);
        try
        {
            var pacientes = JsonConvert.DeserializeObject<List<Paciente>>(json);
            Console.WriteLine(pacientes);
            return pacientes;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error de deserialización JSON: {ex.Message}");
            return new List<Paciente>();
        }
    }

    public async Task<List<Paciente>> ObtenerPacientesPorNombreAsync(string nombre)
    {
        var response = await _httpClient.GetAsync($"{BaseUrl}/filtrar/{Uri.EscapeDataString(nombre)}");
        if (!response.IsSuccessStatusCode) return new List<Paciente>();
        var json = await response.Content.ReadAsStringAsync();
        try
        {
            var data = JsonConvert.DeserializeObject<List<Paciente>>(json);
            Console.WriteLine(data);
            return data;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error de deserialización JSON: {ex.Message}");
            return new List<Paciente>();
        }

    }

    public async Task<Paciente> ObtenerPacientePorIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"{BaseUrl}/{id}");
        if (!response.IsSuccessStatusCode) return null;
        var json = await response.Content.ReadAsStringAsync();
        try
        {
            // Usa el mismo deserializador que en el resto del servicio
            var paciente = JsonConvert.DeserializeObject<Paciente>(json);
            return paciente;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error de deserialización JSON: {ex.Message}");
            return null;
        }
    }

    public async Task<List<Cita>> ObtenerCitasPorPacienteAsync(int pacienteId)
    {
        var response = await _httpClient.GetAsync($"{BaseUrl}/citas/{pacienteId}");
        if (!response.IsSuccessStatusCode) return new List<Cita>();
        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<Cita>>(json) ?? new List<Cita>();
    }

    public async Task<List<Vacuna>> ObtenerVacunasPorPaciente(int pacienteId)
    {
        var response = await _httpClient.GetAsync($"{BaseUrl}/vacunas/{pacienteId}");
        if (!response.IsSuccessStatusCode) return new List<Vacuna>();
        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<Vacuna>>(json) ?? new List<Vacuna>();
    }

    // UPDATE
    public async Task<bool> ActualizarPacienteAsync(int id, Paciente paciente)
    {
        var dto = MapPacienteToDto(paciente);
        var json = System.Text.Json.JsonSerializer.Serialize(dto);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync($"{BaseUrl}/{id}", content);
        return response.IsSuccessStatusCode;
    }

    // DELETE
    public async Task<bool> EliminarPacienteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
        return response.IsSuccessStatusCode;
    }

    private PacienteRequestDto MapPacienteToDto(Paciente paciente)
    {
        return new PacienteRequestDto
        {
            Nombre = paciente.Nombre,
            Especie = paciente.Especie.ToString(), // Enum a string
            Sexo = paciente.Sexo.ToString(),       // Enum a string
            FechaNacimiento = paciente.Fecha_nacimiento.ToString("yyyy-MM-dd"),
            Raza = paciente.Raza,
            Padecimientos = paciente.Padecimiento,
            Intervenciones = paciente.Intervenciones,
            FotoPerfil = null, // O mapea si tienes imagen
            IdPropietario = paciente.Propietario?.Id ?? 0
        };
    }
}

public class PacienteRequestDto
{
    [JsonPropertyName("nombre")]
    public string Nombre { get; set; }

    [JsonPropertyName("especie")]
    public string Especie { get; set; }

    [JsonPropertyName("sexo")]
    public string Sexo { get; set; }

    [JsonPropertyName("fecha_nacimiento")]
    public string FechaNacimiento { get; set; } // Formato "yyyy-MM-dd"

    [JsonPropertyName("raza")]
    public string Raza { get; set; }

    [JsonPropertyName("padecimientos")]
    public string Padecimientos { get; set; }

    [JsonPropertyName("intervenciones")]
    public string Intervenciones { get; set; }

    [JsonPropertyName("foto_perfil")]
    public object FotoPerfil { get; set; }

    [JsonPropertyName("id_propietario")]
    public int IdPropietario { get; set; }
}
