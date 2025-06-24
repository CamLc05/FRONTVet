using System.Collections.ObjectModel;
using Veterinaria.Models;
using Veterinaria.Services;
using Veterinaria.Views;

namespace Veterinaria.Views;

public partial class Ventas : ContentPage
{
    public ObservableCollection<Venta> ventas { get; set; } = new();

    public class Venta
    {
        public string Tipo { get; set; }
        public string Fecha { get; set; }
        public string Total { get; set; }
    }

    public Ventas()
    {
        InitializeComponent();

        // Datos de ejemplo  
        ventas.Add(new Venta { Tipo = "Consulta general", Fecha = "Martes 24 junio 2025", Total = "$600.00 MXN" });
        ventas.Add(new Venta { Tipo = "Cirugía", Fecha = "Martes 25 junio 2025", Total = "$1750.00 MXN" });
        ventas.Add(new Venta { Tipo = "Baño", Fecha = "Martes 25 junio 2025", Total = "$200.00 MXN" });
        ventas.Add(new Venta { Tipo = "Consulta general", Fecha = "Martes 26 junio 2025", Total = "$400.00 MXN" });

            VentasList.ItemsSource = ventas;
        }
    }