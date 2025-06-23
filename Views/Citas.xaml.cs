using System.Globalization;

namespace Veterinaria.Views;

public partial class Citas : ContentPage
{
    public string FechaFormateada { get; set; }
    public Citas()
	{
		InitializeComponent();

        var cultura = new CultureInfo("es-MX"); // o "es-ES" tambi�n sirve
        FechaFormateada = DateTime.Now.ToString("dddd d 'de' MMMM", cultura);

        // Aplica capitalizaci�n a la primera letra del d�a
        FechaFormateada = char.ToUpper(FechaFormateada[0]) + FechaFormateada.Substring(1);

        // Asignar al Label si lo haces directo
        lblFecha.Text = FechaFormateada;
    }
}