using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Veterinaria.Models;

public class Producto
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("nombre")]
    public string Nombre { get; set; }

    [JsonProperty("gramaje")]
    public string Gramaje { get; set; }

    [JsonProperty("precio")]
    public float Precio { get; set; }

    [JsonProperty("tipo")]
    [JsonConverter(typeof(StringEnumConverter))]
    public TipoProducto Tipo { get; set; }

    [JsonProperty("fecha_vencimiento")]
    public DateTime? Fecha_vencimiento { get; set; }

    [JsonProperty("disponibilidad")]
    public int Disponibilidad { get; set; }

    // En Producto.cs
    [JsonIgnore]
    public int? CantidadOperacion { get; set; } = 1;

}

// Clase contenedora para la respuesta de la API
public class ProductosResponse
{
    [JsonProperty("productos")]
    public List<Producto> Productos { get; set; }
}
