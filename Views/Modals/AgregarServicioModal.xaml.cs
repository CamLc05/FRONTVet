using Microsoft.Maui.Controls;

namespace Veterinaria.Views.Modals;

public partial class AgregarServicioModal : ContentPage
{
    public AgregarServicioModal()
    {
        InitializeComponent();
    }

    private async void OnAgregarClicked(object sender, EventArgs e)
    {
        string nombre = NombreServicioEntry.Text;
        string costo = CostoEntry.Text;
        await DisplayAlert("Servicio agregado", $"Nombre: {nombre}\nCosto: {costo}", "OK");
        await Navigation.PopModalAsync();
    }

    private async void OnCancelarClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}