namespace Veterinaria.Models;

public class Usuario
{
    public int Id { get; set; }
    public string Nombre_usuario { get; set; }
    public string Contrasena { get; set; }
    public Rol Rol { get; set; }
    public DateTime Fecha_creacion { get; set; }

}