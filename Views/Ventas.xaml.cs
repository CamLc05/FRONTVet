using System.Collections.ObjectModel;
using Veterinaria.Models;
using Veterinaria.Services;
using Veterinaria.ViewModels;

namespace Veterinaria.Views;

public partial class Ventas : ContentPage
{

        public Ventas()
        {
            InitializeComponent();
            BindingContext = new VentasViewModel();
        }
    }