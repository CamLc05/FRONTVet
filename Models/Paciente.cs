using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Maui.Controls;

namespace Veterinaria.Models;

public class Paciente
{

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("nombre")]
    public string Nombre { get; set; }

    [JsonPropertyName("especie")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TipoEspecie Especie { get; set; }

    [JsonPropertyName("raza")]
    public string Raza { get; set; }

    /*[JsonPropertyName("foto_perfil")]
    [JsonConverter(typeof(FotoPerfilBufferConverter))]
    public byte[] Foto_perfil { get; set; }

    public ImageSource FotoSource
    {
        get
        {
            if (Foto_perfil == null || Foto_perfil.Length == 0)
                return "fondoinicio.png";
            return ImageSource.FromStream(() => new MemoryStream(Foto_perfil));
        }
    }
    */

    [JsonPropertyName("fecha_nacimiento")]
    public DateTime Fecha_nacimiento { get; set; }

    [JsonPropertyName("sexo")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TipoSexo Sexo { get; set; }

    [JsonPropertyName("propietario")]
    public Propietario Propietario { get; set; }

    [JsonPropertyName("padecimientos")]
    public string Padecimiento { get; set; }

    public List<Cita> Citas { get; set; }

    [JsonPropertyName("intervenciones")]
    public string Intervenciones { get; set; }

    public List<string> Vacunas { get; set; }
}

// Convertidor para foto_perfil tipo buffer
public class FotoPerfilBufferConverter : JsonConverter<byte[]>
{
    public override byte[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (var doc = JsonDocument.ParseValue(ref reader))
        {
            if (doc.RootElement.TryGetProperty("data", out var dataElement) && dataElement.ValueKind == JsonValueKind.Array)
            {
                var bytes = new List<byte>();
                foreach (var item in dataElement.EnumerateArray())
                    bytes.Add(item.GetByte());
                return bytes.ToArray();
            }
        }
        return Array.Empty<byte>();
    }

    public override void Write(Utf8JsonWriter writer, byte[] value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}



