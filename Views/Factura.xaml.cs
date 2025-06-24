using Veterinaria.Models;

namespace Veterinaria.Views;

public partial class factura : ContentPage
{
    public factura(Models.Factura factura)
    {
        InitializeComponent();
        BindingContext = new { Factura = factura };
    }

    private async void OnVolverAVentasClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync(); // O usa PopAsync() si solo quieres regresar una página
    }
}