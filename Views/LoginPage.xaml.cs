using Veterinaria.Views;

namespace Veterinaria;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

	private async void onClick(object sender, EventArgs e)
	{
        ((App)Application.Current).GoToMainApp();
    }
    private async void onClickRegistro(object sender, EventArgs e)
    {
        await Application.Current.MainPage.Navigation.PushAsync(new Registro());
    }
}