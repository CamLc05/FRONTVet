using CommunityToolkit.Maui.Views;

namespace Veterinaria.Controls;

public partial class CustomPopupDelete : Popup
{
    // Propiedad para guardar el resultado que quieres enviar al cerrar
    public object Result { get; private set; }

    public CustomPopupDelete()
    {
        InitializeComponent();
    }

    private async void OnConfirmClicked(object sender, EventArgs e)
    {
        Result = true;  // Indica que el usuario confirmó (continuar)
        await CloseAsync();        // Cierra el popup (sin argumentos)
    }
}

