using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Veterinaria.Models;
using Veterinaria.Services;
using Microsoft.Maui.Controls;
using System.Threading.Tasks;

namespace Veterinaria.ViewModels
{
    public class VentasViewModel : INotifyPropertyChanged
    {
        private readonly FacturaService _facturaService;

        private ObservableCollection<Factura> _facturas;
        public ObservableCollection<Factura> Facturas
        {
            get => _facturas;
            set
            {
                if (_facturas != value)
                {
                    _facturas = value;
                    OnPropertyChanged();
                }
            }
        }

        private Factura _facturaSeleccionada;
        public Factura FacturaSeleccionada
        {
            get => _facturaSeleccionada;
            set
            {
                if (_facturaSeleccionada != value)
                {
                    _facturaSeleccionada = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand RefrescarCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly CitaService _citaService;

        private readonly PacienteService _pacienteService;

        public VentasViewModel()
        {
            _pacienteService = new PacienteService(new HttpClient());
            _facturaService = new FacturaService(new HttpClient());
            _citaService = new CitaService(new HttpClient());
            Facturas = new ObservableCollection<Factura>();
            RefrescarCommand = new Command(async () => await CargarFacturasAsync());

            _ = CargarFacturasAsync();
        }

        private async Task CargarFacturasAsync()
        {
            var facturas = await _facturaService.ObtenerFacturasAsync();
            Facturas.Clear();
            if (facturas != null)
            {
                foreach (var factura in facturas)
                {
                    // Obtén la cita asociada y asígnala a la propiedad Cita
                    factura.Cita = await _citaService.ObtenerCitaPorIdAsync(factura.IdCita);
                    
                    factura.Paciente = await _pacienteService.ObtenerPacientePorIdAsync(factura.IdPaciente);
                    Facturas.Add(factura);
                }
            }
        }


        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
