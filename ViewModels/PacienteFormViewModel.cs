using System.Collections.ObjectModel;
using System.Windows.Input;
using Veterinaria.Models;
using Veterinaria.Services;
using Microsoft.Maui.Controls;
using System.Threading.Tasks;

namespace Veterinaria.ViewModels;

public class PacienteFormViewModel : BindableObject
{
    private readonly PacienteService _pacienteService;
    private readonly PropietarioService _propietarioService;

    private Paciente _paciente;
    public Paciente Paciente
    {
        get => _paciente;
        set
        {
            _paciente = value;
            OnPropertyChanged(nameof(Paciente));
            OnPropertyChanged(nameof(Titulo));
        }
    }

    public string Titulo => Paciente?.Id > 0 ? $"Editar: {Paciente.Nombre}" : "Nuevo paciente";

    public ObservableCollection<string> Especies { get; set; } =
        new ObservableCollection<string> { "perro", "gato" };

    public ObservableCollection<Propietario> Propietarios { get; set; } = new();

    public ICommand GuardarCommand { get; }

    public PacienteFormViewModel()
    {
        _pacienteService = new PacienteService(new HttpClient());
        _propietarioService = new PropietarioService(new HttpClient());

        GuardarCommand = new Command(async () => await GuardarAsync());

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
        if (Paciente == null)
        {
            await Application.Current.MainPage.DisplayAlert("Error", "No hay datos de paciente.", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(Paciente.Nombre) || Paciente.Propietario == null)
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Nombre y propietario son obligatorios.", "OK");
            return;
        }

        bool exito;
        if (Paciente.Id > 0)
        {
            exito = await _pacienteService.ActualizarPacienteAsync(Paciente.Id, Paciente);
        }
        else
        {
            exito = await _pacienteService.CrearPacienteAsync(Paciente);
        }

        if (exito)
        {
            await Application.Current.MainPage.DisplayAlert("Guardado", "Paciente guardado correctamente.", "OK");
            await Shell.Current.GoToAsync(".."); // Regresa a la página anterior
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Error", "No se pudo guardar el paciente.", "OK");
        }
    }
}
