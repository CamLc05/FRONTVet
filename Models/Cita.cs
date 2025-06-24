using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Veterinaria.Models;
public class Cita
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("fecha_cita")]
    public DateTimeOffset FechaCita { get; set; }

    [JsonProperty("motivo")]
    public string Motivo { get; set; }

    [JsonProperty("detalles")]
    public string Detalles { get; set; }

    [JsonProperty("peso")]
    public float? Peso { get; set; }

    [JsonProperty("fecha_creacion")]
    public DateTimeOffset FechaCreacion { get; set; }

    [JsonProperty("status")]
    [JsonConverter(typeof(StringEnumConverter))]
    public EstadoCita Status { get; set; }

    [JsonProperty("paciente")]
    public Paciente Paciente { get; set; }

    [JsonProperty("veterinario")]
    public Usuario Veterinario { get; set; }

    [JsonProperty("medicamentos")]
    public List<Producto> Medicamentos { get; set; }

    [JsonProperty("servicios")]
    public List<Servicio> Servicios { get; set; }

    [JsonProperty("observaciones_relacion")]
    public string ObservacionesRelacion { get; set; }
}
