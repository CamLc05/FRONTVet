using System.Globalization;
using Veterinaria.Models;
using Veterinaria.ViewModels;

namespace Veterinaria.Views;

public partial class NuevaCita : ContentPage
    {

    // Constructor para nueva cita
    public NuevaCita()
    {
        InitializeComponent();
        BindingContext = new NuevaCitaViewModel();
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }

}
