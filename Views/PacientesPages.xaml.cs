using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinaria.ViewModels;

namespace Veterinaria.Views;

public partial class PacientesPages : ContentPage
{
    public PacientesPages()
    {
        InitializeComponent();
        BindingContext = new PacienteViewModel();
    }
    private async void OnNuevoPacienteClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PacienteNuevo());
    }
    
    private async void Cancelar_Clicked(object sender, EventArgs e)
    {
        await Application.Current.MainPage.Navigation.PushAsync(new Inventario());
    }
    
    
    
        
    private async void OnNUevoPaciente(object sender, EventArgs e)
    {
        await Application.Current.MainPage.Navigation.PushAsync(new PacienteNuevo());
    }
}