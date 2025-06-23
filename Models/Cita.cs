namespace Veterinaria.Models;

public class Cita
{
    public int Id { get; set; }
    public Paciente Paciente { get; set; }
    public Usuario Veterinario { get; set; }
    public DateTime Fecha_cita { get; set; }
    public string Motivo { get; set; }
    public EstadoCita Status { get; set; }
    public string Detalles { get; set; }
    public float Peso { get; set; }
    public Dictionary<Producto, int> Medicamento { get; set; }
    public List<Servicio> Servicio { get; set; }
    public DateTime Fecha_creacion { get; set; }

}

