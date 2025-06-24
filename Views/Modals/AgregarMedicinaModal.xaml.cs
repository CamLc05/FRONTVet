using Microsoft.Maui.Controls;

namespace Veterinaria.Views.Modals;

public partial class AgregarMedicinaModal : ContentPage
{
    public AgregarMedicinaModal()
    {
        InitializeComponent();
    }

    private async void OnAgregarClicked(object sender, EventArgs e)
    {
        // Aquí puedes obtener los valores y devolverlos a la página anterior si lo deseas
        string nombre = NombreMedicinaEntry.Text;
        string cantidad = CantidadEntry.Text;
        // Puedes usar MessagingCenter, eventos, o un ViewModel compartido para pasar los datos
        await DisplayAlert("Medicina agregada", $"Nombre: {nombre}\nCantidad: {cantidad}", "OK");
        await Navigation.PopModalAsync();
    }

    private async void OnCancelarClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}