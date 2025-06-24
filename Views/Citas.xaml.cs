using System.Globalization;
using Veterinaria.ViewModels;

namespace Veterinaria.Views;

public partial class Citas : ContentPage
{
    public string FechaFormateada { get; set; }
    public Citas()
	{
		InitializeComponent();
        BindingContext = new CitasViewModel();
        
    }


}