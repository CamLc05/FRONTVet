using System;
using Newtonsoft.Json;

namespace Veterinaria.Models;

public class Servicio
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("nombre")]
    public string Nombre { get; set; }

    [JsonProperty("descripcion")]
    public string Descripcion { get; set; }

    [JsonProperty("costo")]
    public float Costo { get; set; }
}

// Clase contenedora para la respuesta de la API
public class ServicioResponse
{
    [JsonProperty("servicio")]
    public Servicio Servicio { get; set; }
}
