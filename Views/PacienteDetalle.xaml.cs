using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinaria.Models;


namespace Veterinaria.Views;

public partial class PacienteDetalle : ContentPage
{
    public PacienteDetalle(Paciente paciente)
    {
        InitializeComponent();
        BindingContext = paciente;
    }
    
    private async void OnVolverClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void OnEditarClicked(object sender, EventArgs e)
    {
        // Navegar a la página de edición, pásale el paciente
        //  await Navigation.PushAsync(new EditarPacientePage(_paciente));
    }
}