namespace Veterinaria.Models;

public class Paciente
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public TipoEspecie Especie { get; set; }
    public string Raza { get; set; }
    public string Foto_perfil { get; set; }
    public ImageSource FotoSource
    {
        get
        {
            return string.IsNullOrWhiteSpace(Foto_perfil)
                ? "fondoinicio.png"
                : ImageSource.FromFile(Foto_perfil);
        }
    }
    
    public DateTime Fecha_nacimiento { get; set; }
    public TipoSexo Sexo { get; set; }
    public Propietario Propietario { get; set; }
    public string Padecimiento { get; set; }
    public List<Cita> Citas { get; set; }
    public string Intervenciones { get; set; }
    public List<string> Vacunas { get; set; }

}