using System.Runtime.Serialization;

public enum TipoProducto
{
    [EnumMember(Value = "medicamentos")]
    medicamento,

    [EnumMember(Value = "estetica")]
    estetica,

    [EnumMember(Value = "quirurgico")]
    quirurgico,

    [EnumMember(Value = "extra")]
    extra
}
