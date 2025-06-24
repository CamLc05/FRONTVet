using System.Collections.ObjectModel;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Veterinaria.Models;
using Veterinaria.Services;
using Microsoft.Maui.Controls;

namespace Veterinaria.ViewModels;

public class ProductoViewModel : INotifyPropertyChanged
{
    private readonly ProductoService _productoService;

    public ObservableCollection<Producto> Productos { get; set; } = new();

    public ICommand SeleccionarCommand { get; }
    public ICommand RefrescarCommand { get; }

    public ProductoViewModel()
    {
        _productoService = new ProductoService(new HttpClient());

        SeleccionarCommand = new Command<Producto>(async (producto) =>
        {
            if (producto != null)
            {
                await Shell.Current.GoToAsync("productoForm", true, new Dictionary<string, object>
                {
                    { "Producto", producto }
                });
            }
        });

        RefrescarCommand = new Command(async () => await CargarProductosAsync());

        _ = CargarProductosAsync();
    }

    private async Task CargarProductosAsync()
    {
        var productos = await _productoService.ObtenerProductosAsync();
        Productos.Clear();
        if (productos != null)
        {
            foreach (var p in productos)
            {
                Productos.Add(p);
                Console.WriteLine(p);
            }
               
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propiedad = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propiedad));
    }
}
