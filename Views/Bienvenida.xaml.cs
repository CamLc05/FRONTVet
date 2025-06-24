using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Extensions;
using Veterinaria.Controls;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Maui.Core;

namespace Veterinaria.Views;

public partial class Bienvenida : ContentPage
{
    public Bienvenida()
    {
        InitializeComponent();
    }

    private async void logout(object sender, EventArgs e)
    {
        var popup = new CustomPopup();

        await this.ShowPopupAsync(popup);

        if (popup.Result is bool confirmed && confirmed)
        {
            ((App)Application.Current).GoToLogin();
        }
        else
        {
            ((App)Application.Current).GoToLogin();
        }
    }
}