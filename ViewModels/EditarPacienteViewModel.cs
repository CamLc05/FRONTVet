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
    public class EditarPacienteViewModel : INotifyPropertyChanged
    {
        private readonly PacienteService _pacienteService;
        private readonly PropietarioService _propietarioService;

        // Propiedades para los pickers
        public ObservableCollection<Propietario> Propietarios { get; set; } = new();
        public Array Especies { get; } = Enum.GetValues(typeof(TipoEspecie));
        public Array Sexos { get; } = Enum.GetValues(typeof(TipoSexo));

        private Paciente _paciente;
        public Paciente Paciente
        {
            get => _paciente;
            set
            {
                _paciente = value;
                OnPropertyChanged();
            }
        }

        public ICommand GuardarCommand { get; }
        public ICommand CancelarCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public EditarPacienteViewModel()
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

            // Si el paciente ya tiene propietario, selecciona el correspondiente de la lista
            if (Paciente != null && Paciente.Propietario != null)
            {
                var match = Propietarios.FirstOrDefault(x => x.Id == Paciente.Propietario.Id);
                if (match != null)
                    Paciente.Propietario = match;
            }
        }

        public async Task GuardarAsync()
        {
            if (Paciente == null)
                return;

            var exito = await _pacienteService.ActualizarPacienteAsync(Paciente.Id, Paciente);
            if (exito)
            {
                await Application.Current.MainPage.DisplayAlert("Éxito", "Paciente actualizado correctamente.", "OK");
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                Console.WriteLine(exito);
                await Application.Current.MainPage.DisplayAlert("Error", "No se pudo actualizar el paciente.", "OK");
            }
        }

        public async Task CancelarAsync()
        {
            await Shell.Current.GoToAsync("..");
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
