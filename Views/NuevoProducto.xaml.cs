using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Veterinaria.Models;
using Veterinaria.Views;

namespace Veterinaria;

public partial class NuevoProducto : ContentPage
{
    public NuevoProducto()
    {
        InitializeComponent();
        
    }
    
    private async void Guardar_Clicked(object sender, EventArgs e)
    {
        // Aquí podrías actualizar la base fake si hicieras una modificación real
        await DisplayAlert("Guardado", "El producto ha sido creado.", "OK");
        await Application.Current.MainPage.Navigation.PushAsync(new Inventario());
    }

    private async void Cancelar_Clicked(object sender, EventArgs e)
    {
        await Application.Current.MainPage.Navigation.PushAsync(new Inventario());
    }
}
