using Veterinaria.ViewModels;

namespace Veterinaria.Views;

public partial class Inventario : ContentPage
{
    public Inventario()
    {
        InitializeComponent();
        BindingContext = new ProductoViewModel();
    }
}
