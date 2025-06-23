namespace Veterinaria.Models;

public class Factura
{
    public enum TipoPago
    {
        Tarjeta,
        Efectivo,
        Transferencia
    }
    public int Id { get; set; }
    public Cita Cita { get; set; }
    public Paciente Paciente { get; set; }
    public float costo_total { get; set; }
    public DateTime Fecha_creacion { get; set; }
    public TipoPago Metodo_pago { get; set; }

}