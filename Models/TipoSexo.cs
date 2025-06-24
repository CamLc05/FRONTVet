using System.Runtime.Serialization;

namespace Veterinaria.Models;

public enum TipoSexo
{
    [EnumMember(Value = "Macho")]
    Macho,
    [EnumMember(Value = "Hembra")]
    Hembra
}