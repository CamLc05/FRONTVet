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
    public class EditarCitaViewModel : INotifyPropertyChanged
    {
        private readonly CitaService _citaService;
        private readonly PacienteService _pacienteService;
        private readonly UsuarioService _usuarioService;
        private readonly ProductoService _productoService;
        private readonly ServicioService _servicioService;

        public ObservableCollection<Paciente> Pacientes { get; set; } = new();
        public ObservableCollection<Usuario> Veterinarios { get; set; } = new();
        public ObservableCollection<Producto> MedicamentosDisponibles { get; set; } = new();
        public ObservableCollection<Servicio> ServiciosDisponibles { get; set; } = new();

        private Cita _cita;
        public Cita Cita
        {
            get => _cita;
            set { _cita = value; OnPropertyChanged(); }
        }

        private Paciente _pacienteSeleccionado;
        public Paciente PacienteSeleccionado
        {
            get => _pacienteSeleccionado;
            set
            {
                _pacienteSeleccionado = value;
                if (Cita != null) Cita.Paciente = value;
                OnPropertyChanged();
            }
        }

        private Usuario _veterinarioSeleccionado;
        public Usuario VeterinarioSeleccionado
        {
            get => _veterinarioSeleccionado;
            set
            {
                _veterinarioSeleccionado = value;
                if (Cita != null) Cita.Veterinario = value;
                OnPropertyChanged();
            }
        }

        // Modal Medicamentos
        private bool _mostrarModalMedicamentos;
        public bool MostrarModalMedicamentos
        {
            get => _mostrarModalMedicamentos;
            set { _mostrarModalMedicamentos = value; OnPropertyChanged(); }
        }

        private Producto _medicamentoSeleccionado;
        public Producto MedicamentoSeleccionado
        {
            get => _medicamentoSeleccionado;
            set { _medicamentoSeleccionado = value; OnPropertyChanged(); }
        }

        // Modal Servicios
        private bool _mostrarModalServicios;
        public bool MostrarModalServicios
        {
            get => _mostrarModalServicios;
            set { _mostrarModalServicios = value; OnPropertyChanged(); }
        }

        private Servicio _servicioSeleccionado;
        public Servicio ServicioSeleccionado
        {
            get => _servicioSeleccionado;
            set { _servicioSeleccionado = value; OnPropertyChanged(); }
        }

        public ICommand GuardarCitaCommand { get; }
        public ICommand AbrirModalMedicamentosCommand { get; }
        public ICommand CerrarModalMedicamentosCommand { get; }
        public ICommand AgregarMedicamentoCommand { get; }
        public ICommand AbrirModalServiciosCommand { get; }
        public ICommand CerrarModalServiciosCommand { get; }
        public ICommand AgregarServicioCommand { get; }
        public ICommand CancelarCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public EditarCitaViewModel()
        {
            _citaService = new CitaService(new HttpClient());
            _pacienteService = new PacienteService(new HttpClient());
            _usuarioService = new UsuarioService(new HttpClient());
            _productoService = new ProductoService(new HttpClient());
            _servicioService = new ServicioService(new HttpClient());

            GuardarCitaCommand = new Command(async () => await GuardarCitaAsync());
            AbrirModalMedicamentosCommand = new Command(() => MostrarModalMedicamentos = true);
            CerrarModalMedicamentosCommand = new Command(() => MostrarModalMedicamentos = false);
            AgregarMedicamentoCommand = new Command(AgregarMedicamento);
            AbrirModalServiciosCommand = new Command(() => MostrarModalServicios = true);
            CerrarModalServiciosCommand = new Command(() => MostrarModalServicios = false);
            AgregarServicioCommand = new Command(AgregarServicio);
            CancelarCommand = new Command(async () => await CancelarAsync());

            _ = CargarPacientesAsync();
            _ = CargarVeterinariosAsync();
            _ = CargarMedicamentosDisponiblesAsync();
            _ = CargarServiciosDisponiblesAsync();
        }

        public async Task InicializarAsync(Cita cita)
        {
            Cita = cita;
            PacienteSeleccionado = cita.Paciente;
            VeterinarioSeleccionado = cita.Veterinario;
        }

        private async Task CargarPacientesAsync()
        {
            var pacientes = await _pacienteService.ObtenerPacientesAsync();
            Pacientes.Clear();
            foreach (var p in pacientes)
                Pacientes.Add(p);
        }

        private async Task CargarVeterinariosAsync()
        {
            var veterinarios = await _usuarioService.ObtenerVeterinariosAsync();
            Veterinarios.Clear();
            foreach (var v in veterinarios)
                Veterinarios.Add(v);
        }

        private async Task CargarMedicamentosDisponiblesAsync()
        {
            var medicamentos = await _productoService.ObtenerProductosAsync();
            MedicamentosDisponibles.Clear();
            foreach (var m in medicamentos)
                MedicamentosDisponibles.Add(m);
        }

        private async Task CargarServiciosDisponiblesAsync()
        {
            var servicios = await _servicioService.ObtenerServiciosAsync();
            ServiciosDisponibles.Clear();
            foreach (var s in servicios)
                ServiciosDisponibles.Add(s);
        }

        private async Task GuardarCitaAsync()
        {
            if (Cita == null || Cita.Paciente == null || Cita.Veterinario == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Selecciona paciente y veterinario.", "OK");
                return;
            }
           var data= MapCitaToDto(Cita);
            var exito = await _citaService.ActualizarCitaAsync(Cita.Id, data);
            if (exito)
            {
                await Application.Current.MainPage.DisplayAlert("Éxito", "Cita actualizada correctamente.", "OK");
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No se pudo actualizar la cita.", "OK");
            }
        }

        private void AgregarMedicamento()
        {
            if (MedicamentoSeleccionado != null && Cita != null)
            {
                if (Cita.Medicamentos == null)
                    Cita.Medicamentos = new List<Producto>();
                if (!Cita.Medicamentos.Any(m => m.Id == MedicamentoSeleccionado.Id))
                    Cita.Medicamentos.Add(MedicamentoSeleccionado);
                OnPropertyChanged(nameof(Cita));
                MostrarModalMedicamentos = false;
            }
        }

        private void AgregarServicio()
        {
            if (ServicioSeleccionado != null && Cita != null)
            {
                if (Cita.Servicios == null)
                    Cita.Servicios = new List<Servicio>();
                if (!Cita.Servicios.Any(s => s.Id == ServicioSeleccionado.Id))
                    Cita.Servicios.Add(ServicioSeleccionado);
                OnPropertyChanged(nameof(Cita));
                MostrarModalServicios = false;
            }
        }
        private CitaRequestDto MapCitaToDto(Cita cita)
        {
            return new CitaRequestDto
            {
                FechaCita = cita.FechaCita.ToString("yyyy-MM-dd HH:mm:ss"),
                Motivo = cita.Motivo,
                Detalles = cita.Detalles,
                Peso = cita.Peso,
                IdPaciente = cita.Paciente?.Id ?? 0,
                IdVeterinario = cita.Veterinario?.Id ?? 0
            };
        }

        private async Task CancelarAsync()
        {
            await Shell.Current.GoToAsync("..");
        }

        

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }


   
}
