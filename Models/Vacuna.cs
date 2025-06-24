using System;
using System.Text.Json.Serialization;

namespace Veterinaria.Models;

public class Vacuna
{
    [JsonPropertyName("nombre_vacuna")]
    public string NombreVacuna { get; set; }

    [JsonPropertyName("fecha_aplicacion")]
    public DateTime? FechaAplicacion { get; set; }

    [JsonPropertyName("proxima_dosis")]
    public DateTime? ProximaDosis { get; set; }
}
