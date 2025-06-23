

namespace Veterinaria.Models;

public class Vacuna
{
    public int Id { get; set; }
    public int id_paciente { get; set; }

    public string nombre_vacuna { get; set; }
    public string fecha_aplicacion { get; set; }

    public string proxima_dosis { get; set; }

}