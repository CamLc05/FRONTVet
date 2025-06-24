using Veterinaria.ViewModels;

namespace Veterinaria.Views;

public partial class PacienteNuevo : ContentPage
{
    public PacienteNuevo()
    {
        InitializeComponent();
        BindingContext = new PacienteNuevoViewModel();
    }
}
