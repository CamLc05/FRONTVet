using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veterinaria.Views;

public partial class NuevoPropietario : ContentPage
{
    public NuevoPropietario()
    {
        InitializeComponent();
    }
    
    private async void Guardar_Clicked(object sender, EventArgs e)
    {
      
        await DisplayAlert("Guardado", "El producto ha sido creado.", "OK");
        await Application.Current.MainPage.Navigation.PushAsync(new NuevoPropietario());
    }

    private async void Cancelar_Clicked(object sender, EventArgs e)
    {
        await Application.Current.MainPage.Navigation.PushAsync(new NuevoPropietario());
    }
}