using System.Globalization;
using Veterinaria.ViewModels;

namespace Veterinaria.Views;

public partial class Citas : ContentPage
{
    public string FechaFormateada { get; set; }
    public Citas()
	{
		InitializeComponent();
        BindingContext = new CitasViewModel();
        
    }

    private async void OnPlusTapped(object sender, EventArgs e)
    {
        // Navegar a la página NuevaCita
        await Navigation.PushAsync(new NuevaCita());
    }
    private async void OnEditar(object sender, EventArgs e)
    {
        await Application.Current.MainPage.Navigation.PushAsync(new Calendario());
    }


}