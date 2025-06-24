using CommunityToolkit.Maui.Views;
using CommunityToolkit.Maui.Extensions;
using Veterinaria.Controls;

namespace Veterinaria.Views;

public partial class Registro : ContentPage
{
	public Registro()
	{
		InitializeComponent();
	}

    private async void MostrarMenuRol(object sender, EventArgs e)
    {
        await  Application.Current.MainPage.Navigation.PushAsync(new RegistroRol());
    }



}