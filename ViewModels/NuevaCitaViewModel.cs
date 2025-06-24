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
    public class NuevaCitaViewModel : INotifyPropertyChanged
    {
        private readonly CitaService _citaService;
        private readonly PacienteService _pacienteService;
        private readonly UsuarioService _usuarioService;

        public ObservableCollection<Paciente> Pacientes { get; set; } = new();
        public ObservableCollection<Usuario> Veterinarios { get; set; } = new();

        private Cita _nuevaCita = new Cita
        {
            FechaCita = DateTimeOffset.Now
        };
        public Cita NuevaCita
        {
            get => _nuevaCita;
            set
            {
                _nuevaCita = value;
                OnPropertyChanged();
            }
        }

        private Paciente _pacienteSeleccionado;
        public Paciente PacienteSeleccionado
        {
            get => _pacienteSeleccionado;
            set
            {
                _pacienteSeleccionado = value;
                NuevaCita.Paciente = value;
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
                NuevaCita.Veterinario = value;
                OnPropertyChanged();
            }
        }

        public ICommand GuardarCitaCommand { get; }
        public ICommand CancelarCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public NuevaCitaViewModel()
        {
            _citaService = new CitaService(new HttpClient());
            _pacienteService = new PacienteService(new HttpClient());
            _usuarioService = new UsuarioService(new HttpClient());
            GuardarCitaCommand = new Command(async () => await GuardarCitaAsync());
            CancelarCommand = new Command(async () => await CancelarAsync());

            _ = CargarPacientesAsync();
            _ = CargarVeterinariosAsync();
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
            Console.WriteLine("Respuesta de service: " + veterinarios);
            Veterinarios.Clear();
            foreach (var v in veterinarios)
            {
                Veterinarios.Add(v);
                Console.WriteLine(v.Nombre_pila);
            }
                
        }

        private async Task GuardarCitaAsync()
        {
            if (NuevaCita.Paciente == null || NuevaCita.Veterinario == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Selecciona paciente y veterinario.", "OK");
                return;
            }

            var dto = MapCitaToDto(NuevaCita);

            var exito = await _citaService.CrearCitaAsync(dto);
            if (exito)
            {
                await Application.Current.MainPage.DisplayAlert("Éxito", "Cita creada correctamente.", "OK");
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No se pudo crear la cita.", "OK");
            }
        }

        private async Task CancelarAsync()
        {
            await Shell.Current.GoToAsync("..");
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



        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public class CitaRequestDto
    {
        [Newtonsoft.Json.JsonProperty("fecha_cita")]
        public string FechaCita { get; set; } // Formato: "yyyy-MM-dd HH:mm:ss"

        [Newtonsoft.Json.JsonProperty("motivo")]
        public string Motivo { get; set; }

        [Newtonsoft.Json.JsonProperty("detalles")]
        public string Detalles { get; set; }

        [Newtonsoft.Json.JsonProperty("peso")]
        public float? Peso { get; set; }

        [Newtonsoft.Json.JsonProperty("id_paciente")]
        public int IdPaciente { get; set; }

        [Newtonsoft.Json.JsonProperty("id_veterinario")]
        public int IdVeterinario { get; set; }
    }


}
