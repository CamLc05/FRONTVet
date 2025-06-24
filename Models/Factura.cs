using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Veterinaria.Models;

public class Factura
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TipoPago
    {
        tarjeta,
        efectivo,
        transferencia
    }

    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("costo_total")]
    public float CostoTotal { get; set; }

    [JsonProperty("fecha_creacion")]
    public DateTime FechaCreacion { get; set; }

    [JsonProperty("metodo_pago")]
    [JsonConverter(typeof(StringEnumConverter))]
    public TipoPago MetodoPago { get; set; }

    [JsonProperty("id_cita")]
    public int IdCita { get; set; }

    [JsonProperty("id_paciente")]
    public int IdPaciente { get; set; }

    [JsonIgnore] // Para que no interfiera con la serialización/deserialización
    public Cita? Cita { get; set; }

    [JsonIgnore] // Para que no interfiera con la serialización/deserialización
    public Paciente? Paciente { get; set; }

}

// Clase contenedora para la respuesta de la API
public class FacturaResponse
{
    [JsonProperty("factura")]
    public Factura Factura { get; set; }
}
