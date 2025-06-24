using System.Collections.ObjectModel;
using Veterinaria.Models;
using Veterinaria.Services;

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
        
        private async void OnEliminarSwipe(object sender, EventArgs e)
        {
            if (sender is SwipeItem item && item.CommandParameter is Venta venta)
            {
                bool confirm = await Application.Current.MainPage.DisplayAlert("¿Eliminar?", $"¿Eliminar {venta.Tipo}?", "Sí", "Cancelar");
                if (confirm)
                {
                    // Aquí no puedes removerlo de la lista local. Deberías emitir un evento.
                    Console.WriteLine("venta eliminada: " + venta.Tipo);
                }
            }
        }

        private async void OnDetallesVenta(object sender, EventArgs e)
        {
            if (sender is ImageButton button && button.CommandParameter is Venta venta)
            {
                Console.WriteLine("Detalles de: " + venta.Tipo);
                await Application.Current.MainPage.Navigation.PushAsync(new DetallesVenta());
            }
        }



        private async void OnEditarSwipe(object sender, EventArgs e)
        {
            if (sender is SwipeItem item && item.CommandParameter is Venta venta)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new Ventas());
                Console.WriteLine("Editar: " + venta.Tipo);
            }
        }

        
    }