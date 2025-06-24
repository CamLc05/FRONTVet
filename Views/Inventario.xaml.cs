using Veterinaria.Models;
using Veterinaria.Services; 

namespace Veterinaria.Views;


public partial class Inventario : ContentPage
{
	public List<Producto> Productos { get; set; }

	public Inventario()
	{
		InitializeComponent();
		Productos = FakeDatabase.ObtenerProductos();
		BindingContext = this;
	}
	 
	private async void OnEliminarSwipe(object sender, EventArgs e)
	{
		if (sender is SwipeItem item && item.CommandParameter is Producto producto)
		{
			bool confirm = await DisplayAlert("¿Eliminar?", $"¿Eliminar {producto.Nombre}?", "Sí", "Cancelar");
			if (confirm)
			{
				Productos.Remove(producto);
				// Recargar lista
				BindingContext = null;
				BindingContext = this;
			}
		}
	}
        
	private async void OnEditarSwipe(object sender, EventArgs e)
	{
		if (sender is SwipeItem item && item.CommandParameter is Producto producto)
		{
			await Application.Current.MainPage.Navigation.PushAsync(new EditarProductoPage(producto));
		}
	}
        
	private async void OnDetallesProducto(object sender, EventArgs e)
	{
		if (sender is ImageButton button && button.CommandParameter is Producto producto)
		{
			Console.WriteLine("entro");
			await Application.Current.MainPage.Navigation.PushAsync(new DetallesProducto(producto));
		}
	}
        
	private async void OnNuevoProducto(object sender, EventArgs e)
	{
		// Navegar a la página de neuvo producto
		await Application.Current.MainPage.Navigation.PushAsync(new NuevoProducto());
	}


}