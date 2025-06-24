using System;
using System.Text.Json.Serialization;

namespace Veterinaria.Models;

public class Usuario
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("nombre_usuario")]
    public string Nombre_usuario { get; set; }

    [JsonPropertyName("contrasena")]
    public string? Contrasena { get; set; }

    [JsonPropertyName("rol")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Rol Rol { get; set; }

    [JsonPropertyName("fecha_creacion")]
    public DateTime Fecha_creacion { get; set; }

    [JsonPropertyName("nombre_pila")]
    public string Nombre_pila { get; set; }
}

// Clase contenedora para la respuesta de la API
public class UsuarioResponse
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("usuario")]
    public Usuario Usuario { get; set; }
}
