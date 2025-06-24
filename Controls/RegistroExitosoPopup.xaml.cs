using CommunityToolkit.Maui.Views;

using CommunityToolkit.Maui.Views;
namespace Veterinaria.Controls;

public partial class RegistroExitosoPopup : Popup
{
    public object Result { get; private set; }
    public RegistroExitosoPopup()
    {
        InitializeComponent();
    }
    private async void OnConfirmClicked(object sender, EventArgs e)
    {
        Result = true;  // Indica que el usuario confirmó (continuar)
        await CloseAsync();        // Cierra el popup (sin argumentos)
    }
}