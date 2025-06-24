using System.Globalization;
using Veterinaria.Models;
using Veterinaria.ViewModels;

namespace Veterinaria.Views;

public partial class EditarCita : ContentPage
{
    public EditarCita()
    {
        InitializeComponent();
        BindingContext = new EditarCitaViewModel();
    }

 
}
