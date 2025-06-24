using CommunityToolkit.Maui.Extensions;
using Veterinaria.Controls;

namespace Veterinaria.Views;

public partial class RegistroRol : ContentPage
{
	public RegistroRol()
	{
		InitializeComponent();
	}

    private async void rolAdmin(object sender, EventArgs e)
    {
        var popup = new RegistroExitosoPopup();

        await this.ShowPopupAsync(popup);

        if (popup.Result is bool confirmed && confirmed)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
        }
        else
        {
            await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
        }
    }
}