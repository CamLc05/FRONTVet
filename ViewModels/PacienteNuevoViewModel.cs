using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Veterinaria.Models;
using Veterinaria.Services;
using Microsoft.Maui.Controls;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace Veterinaria.ViewModels
{
    public class PacienteNuevoViewModel : INotifyPropertyChanged
    {
        private readonly PacienteService _pacienteService;
        private readonly PropietarioService _propietarioService;

        public ObservableCollection<Propietario> Propietarios { get; set; } = new();
        public Array Especies { get; } = Enum.GetValues(typeof(TipoEspecie));
        public Array Sexos { get; } = Enum.GetValues(typeof(TipoSexo));

        private Paciente _nuevoPaciente = new Paciente();
        public Paciente NuevoPaciente
        {
            get => _nuevoPaciente;
            set
            {
                _nuevoPaciente = value;
                OnPropertyChanged();
            }
        }

        private Propietario _selectedPropietario;
        public Propietario SelectedPropietario
        {
            get => _selectedPropietario;
            set
            {
                _selectedPropietario = value;
                NuevoPaciente.Propietario = value;
                OnPropertyChanged();
            }
        }

        public ICommand GuardarCommand { get; }
        public ICommand CancelarCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public PacienteNuevoViewModel()
        {
            _pacienteService = new PacienteService(new HttpClient());
            _propietarioService = new PropietarioService(new HttpClient());
            GuardarCommand = new Command(async () => await GuardarAsync());
            CancelarCommand = new Command(async () => await CancelarAsync());

            _ = CargarPropietariosAsync();
        }

        private async Task CargarPropietariosAsync()
        {
            var propietarios = await _propietarioService.ObtenerPropietariosAsync();
            Propietarios.Clear();
            foreach (var p in propietarios)
                Propietarios.Add(p);
        }

        private async Task GuardarAsync()
        {
            if (NuevoPaciente.Propietario == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debes seleccionar un propietario.", "OK");
                return;
            }

            var exito = await _pacienteService.CrearPacienteAsync(NuevoPaciente);
            if (exito)
            {
                await Application.Current.MainPage.DisplayAlert("Éxito", "Paciente registrado correctamente.", "OK");
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No se pudo registrar el paciente.", "OK");
            }
        }

        private async Task CancelarAsync()
        {
            await Shell.Current.GoToAsync("..");
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
