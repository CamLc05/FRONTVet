using System;
using System.Text.Json.Serialization;

namespace Veterinaria.Models;

public class Propietario
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("nombre")]
    public string Nombre { get; set; }

    [JsonPropertyName("apellidos")]
    public string Apellidos { get; set; }

    [JsonPropertyName("num_telefono")]
    public string Num_telefono { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("direccion")]
    public string Direccion { get; set; }

    [JsonPropertyName("fecha_creacion")]
    public DateTime Fecha_creacion { get; set; }
}
