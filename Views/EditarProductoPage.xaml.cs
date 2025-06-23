using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinaria.Models; 


namespace Veterinaria
;

public partial class EditarProductoPage : ContentPage
{
  
    public Producto Producto { get; set; }

    public List<TipoProducto> TiposProducto { get; set; } = Enum.GetValues(typeof(TipoProducto)).Cast<TipoProducto>().ToList();

    public EditarProductoPage(Producto producto)
    {
        InitializeComponent();
        Producto = producto;
        BindingContext = this;
    }

    private async void Guardar_Clicked(object sender, EventArgs e)
    {
        // Aquí podrías actualizar la base fake si hicieras una modificación real
        await DisplayAlert("Guardado", "El producto ha sido actualizado.", "OK");
        await Application.Current.MainPage.Navigation.PushAsync(new MainPage());
    }

    private async void Cancelar_Clicked(object sender, EventArgs e)
    {
        await Application.Current.MainPage.Navigation.PushAsync(new MainPage());
    }
    
  
    
}