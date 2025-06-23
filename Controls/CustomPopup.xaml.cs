using CommunityToolkit.Maui.Views;

namespace Veterinaria.Controls;

public partial class CustomPopup : Popup
{
    // Propiedad para guardar el resultado que quieres enviar al cerrar
    public object Result { get; private set; }

    public CustomPopup()
    {
        InitializeComponent();
    }

    private async void OnConfirmClicked(object sender, EventArgs e)
    {
        Result = true;  // Indica que el usuario confirmó (continuar)
        await CloseAsync();        // Cierra el popup (sin argumentos)
    }
}

