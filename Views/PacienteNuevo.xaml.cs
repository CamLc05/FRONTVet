using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veterinaria.Views;

public partial class PacienteNuevo : ContentPage
{
    public PacienteNuevo()
    {
        InitializeComponent();
    }
    private async void OnGuardarClicked(object sender, EventArgs e)
    {
        // Aquí podrías guardar en base de datos o actualizar la lista original
        await DisplayAlert("Éxito", "Los cambios fueron guardados correctamente.", "OK");
        await Navigation.PopAsync(); // Regresa a la pantalla anterior
    }
}