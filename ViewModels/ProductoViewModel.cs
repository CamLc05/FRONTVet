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
    public ICommand AumentarCantidadCommand { get; }
    public ICommand ReducirCantidadCommand { get; }


    public ICommand SeleccionarCategoriaCommand { get; }
    // En Producto.cs



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
        SeleccionarCategoriaCommand = new Command<TipoProducto>(async (tipo) => await CargarProductosCategoriaAsync(tipo));
        AumentarCantidadCommand = new Command<Producto>(async (producto) =>
        {
            if (producto != null)
                await ModificarDisponibilidadProductoAsync(producto, producto.CantidadOperacion ?? 1, true);
            else
            {
                Console.WriteLine("EL PRODUCTO ES NULL");
            }
        });

        ReducirCantidadCommand = new Command<Producto>(async (producto) =>
        {
            if (producto != null)
                await ModificarDisponibilidadProductoAsync(producto, producto.CantidadOperacion ?? 1, false);
        });



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
                p.CantidadOperacion = 1;
                Productos.Add(p);
                Console.WriteLine(p);
            }
               
        }
    }

    private async Task CargarProductosCategoriaAsync(TipoProducto tipoProducto)
    {
        var productos = await _productoService.ObtenerProductosAsync();
        Productos.Clear();
        if (productos != null)
        {
            foreach (var p in productos)
            {
                if(p.Tipo == tipoProducto)
                {
                    p.CantidadOperacion = 1;
                    Productos.Add(p);
                    Console.WriteLine(p);
                }
                
            }
        }
    }

    public async Task ModificarDisponibilidadProductoAsync(Producto producto, int cantidad, bool aumentar)
    {
        Console.WriteLine("Entro a modificar");
        if (producto == null || cantidad <= 0)
        {
            Console.WriteLine("No se recibe ningun producto");
            return;

        }
            

        // Calcula la nueva disponibilidad
        int nuevaDisponibilidad = aumentar
            ? producto.Disponibilidad + cantidad
            : producto.Disponibilidad - cantidad;

        if (nuevaDisponibilidad < 0)
            nuevaDisponibilidad = 0;

        // Crea una copia para actualizar solo la disponibilidad
        var productoActualizado = new Producto
        {
            Id = producto.Id,
            Nombre = producto.Nombre,
            Gramaje = producto.Gramaje,
            Precio = producto.Precio,
            Tipo = producto.Tipo,
            Fecha_vencimiento = producto.Fecha_vencimiento,
            Disponibilidad = nuevaDisponibilidad
        };

        var exito = await _productoService.ActualizarProductoAsync(producto.Id, productoActualizado);


        if (exito)
        {
            producto.Disponibilidad = nuevaDisponibilidad;
            OnPropertyChanged(nameof(Productos));
            await Application.Current.MainPage.DisplayAlert("Éxito", "Stock actualizado.", "OK");
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Error", "No se pudo actualizar el stock.", "OK");
        }
    }


    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propiedad = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propiedad));
    }
}
