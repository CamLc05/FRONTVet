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
        
        private async void OnEliminarSwipe(object sender, EventArgs e)
        {
            if (sender is SwipeItem item && item.CommandParameter is Factura venta)
            {
                bool confirm = await Application.Current.MainPage.DisplayAlert("¿Eliminar?", $"¿Eliminar {venta.Cita.Motivo}?", "Sí", "Cancelar");
                if (confirm)
                {
                    // Aquí no puedes removerlo de la lista local. Deberías emitir un evento.
                    Console.WriteLine("venta eliminada: " + venta.Cita.Motivo);
                }
            }
        }

        private async void OnDetallesVenta(object sender, EventArgs e)
        {
            if (sender is ImageButton button && button.CommandParameter is Factura venta)
            {
                Console.WriteLine("Detalles de: " + venta.Cita.Motivo);
                await Application.Current.MainPage.Navigation.PushAsync(new DetallesVenta());
            }
        }



        private async void OnEditarSwipe(object sender, EventArgs e)
        {
            if (sender is SwipeItem item && item.CommandParameter is Factura venta)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new Ventas());
                Console.WriteLine("Editar: " + venta.Cita.Motivo);
            }
        }

        
    }