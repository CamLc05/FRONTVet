using System;
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
    public ObservableCollection<Producto> Productos { get; set; }
    public ICommand SeleccionarCommand { get; }
    
    
    public ProductoViewModel()
    {
        var lista = FakeDatabase.ObtenerProductos();
        Productos = new ObservableCollection<Producto>(lista);
       
        SeleccionarCommand = new Command<Producto>(async (producto) =>
        {
            await Shell.Current.GoToAsync("productoForm", true, new Dictionary<string, object>
            {
                { "Producto", producto }
            });
        });
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propiedad = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propiedad));
    }
}