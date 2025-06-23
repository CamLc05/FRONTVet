using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Veterinaria.Models;
using Veterinaria.Services;
using Microsoft.Maui.Controls; 


namespace Veterinaria.ViewModels;

public class ProductoFormViewModel : BindableObject
{
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
    
    public string Titulo => Producto?.Nombre ?? "Nuevo producto";
    
    
    public ObservableCollection<string> TipoProducto { get; set; } =
        new ObservableCollection<string> { "Medicamento", "Estetica", "Quirurgico", "Extra" };

    
    public ICommand GuardarCommand { get; }

    public ProductoFormViewModel()
    {
        
        GuardarCommand = new Command(() =>
        {
            Application.Current.MainPage.DisplayAlert("Guardado", "Producto actualizado", "OK");
        });
    }
    
}