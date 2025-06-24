using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinaria.Models;
using Veterinaria.Services; 


namespace Veterinaria;

public partial class DetallesProducto : ContentPage
{
    public DetallesProducto(Producto producto)
    {
        InitializeComponent();
        BindingContext = producto;
        
    }
    
    private async void OnVolverClicked(object sender, EventArgs e)
    {
        
        await Application.Current.MainPage.Navigation.PushAsync(new MainPage());
    }

    private async void OnEditarClicked(object sender, EventArgs e)
    {
        // Navegar a la página de edición, pásale el paciente
        //  await Navigation.PushAsync(new EditarPacientePage(_paciente));
    }
    
    private async void OnNuevoProducto(object sender, EventArgs e)
    {
        // Navegar a la página de neuvo producto
        await Application.Current.MainPage.Navigation.PushAsync(new NuevoProducto());
    }
    
}
