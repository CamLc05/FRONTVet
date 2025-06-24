using System.Runtime.Serialization;

namespace Veterinaria.Models;

public enum TipoEspecie
{
    [EnumMember(Value = "Perro")]
    Perro,
    [EnumMember(Value = "Gato")]
    Gato
}