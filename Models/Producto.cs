namespace Veterinaria.Models;

public class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Gramaje { get; set; }
    public float Precio { get; set; }
    public TipoProducto Tipo { get; set; }
    public DateTime Fecha_vencimiento { get; set; }
    public int Disponibilidad { get; set; }

}