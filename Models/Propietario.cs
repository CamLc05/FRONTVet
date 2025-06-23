namespace Veterinaria.Models;

public class Propietario
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public int Num_telefono { get; set; }
    public string Email { get; set; }
    public string Direccion { get; set; }
    public DateTime Fecha_creacion { get; set; }

}