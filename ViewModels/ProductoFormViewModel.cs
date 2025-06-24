using System.Collections.ObjectModel;
using System.Windows.Input;
using Veterinaria.Models;
using Veterinaria.Services;
using Microsoft.Maui.Controls;
using System.Threading.Tasks;

namespace Veterinaria.ViewModels;

public class ProductoFormViewModel : BindableObject
{
    private readonly ProductoService _productoService;

    private Producto _producto;
    public Producto Producto
    {
        get => _producto;
        set
        {
            _producto = value;
            OnPropertyChanged(nameof(Producto));
            OnPropertyChanged(nameof(Titulo));
        }
    }

    public string Titulo => Producto?.Id > 0 ? $"Editar: {Producto.Nombre}" : "Nuevo producto";

    public ObservableCollection<string> TiposProducto { get; set; } =
        new ObservableCollection<string> { "medicamento", "estetica", "quirurgico", "extra" };

    public ICommand GuardarCommand { get; }

    public ProductoFormViewModel()
    {
        _productoService = new ProductoService(new HttpClient());
        GuardarCommand = new Command(async () => await GuardarAsync());
    }

    private async Task GuardarAsync()
    {
        if (Producto == null)
        {
            await Application.Current.MainPage.DisplayAlert("Error", "No hay datos de producto.", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(Producto.Nombre) || Producto.Precio <= 0)
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Nombre y precio son obligatorios.", "OK");
            return;
        }

        bool exito;
        if (Producto.Id > 0)
        {
            exito = await _productoService.ActualizarProductoAsync(Producto.Id, Producto);
        }
        else
        {
            exito = await _productoService.CrearProductoAsync(Producto);
        }

        if (exito)
        {
            await Application.Current.MainPage.DisplayAlert("Guardado", "Producto guardado correctamente.", "OK");
            await Shell.Current.GoToAsync(".."); // Regresa a la página anterior
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Error", "No se pudo guardar el producto.", "OK");
        }
    }
}
