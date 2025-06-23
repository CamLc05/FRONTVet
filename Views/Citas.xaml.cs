using System.Globalization;

namespace Veterinaria.Views;

public partial class Citas : ContentPage
{
    public string FechaFormateada { get; set; }
    public Citas()
	{
		InitializeComponent();

        var cultura = new CultureInfo("es-MX"); // o "es-ES" también sirve
        FechaFormateada = DateTime.Now.ToString("dddd d 'de' MMMM", cultura);

        // Aplica capitalización a la primera letra del día
        FechaFormateada = char.ToUpper(FechaFormateada[0]) + FechaFormateada.Substring(1);

        // Asignar al Label si lo haces directo
        lblFecha.Text = FechaFormateada;
    }
}