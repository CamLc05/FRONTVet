using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Veterinaria.Models;
using Veterinaria.Services;
using Microsoft.Maui.Controls;
using System.Threading.Tasks;
using Veterinaria.Views;

namespace Veterinaria.ViewModels;

public class PacienteViewModel : INotifyPropertyChanged
{
    private readonly PacienteService _pacienteService;

    public ObservableCollection<Paciente> Pacientes { get; set; } = new();
    private string _filtroNombre;
    public string FiltroNombre
    {
        get => _filtroNombre;
        set
        {
            if (_filtroNombre != value)
            {
                _filtroNombre = value;
                OnPropertyChanged();
                FiltrarPacientesCommand.Execute(null);
            }
        }
    }

    public ICommand FiltrarPacientesCommand { get; }
    public ICommand SeleccionarCommand { get; }
    public ICommand NuevoPacienteCommand { get; }
    public ICommand CancelarCommand { get; }
    public ICommand EliminarCommand { get; }
    public ICommand EditarCommand { get; }
    public ICommand DetallesCommand { get; }


    public event PropertyChangedEventHandler PropertyChanged;

    public PacienteViewModel()
    {
        _pacienteService = new PacienteService(new HttpClient());
        FiltrarPacientesCommand = new Command(async () => await FiltrarPacientesAsync());
        SeleccionarCommand = new Command<Paciente>(async (paciente) => await SeleccionarPacienteAsync(paciente));
        EliminarCommand = new Command<Paciente>(async (paciente) => await OnEliminar(paciente));
        EditarCommand = new Command<Paciente>(async (paciente) => await OnEditar(paciente));
        DetallesCommand = new Command<Paciente>(async (paciente) => await OnDetalles(paciente));
        NuevoPacienteCommand = new Command(async () => await OnNuevoPaciente());

        _ = CargarPacientesAsync();
    }

    private async Task CargarPacientesAsync()
    {
        Console.WriteLine("Cargando pacientes... AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
        var pacientes = await _pacienteService.ObtenerPacientesAsync();
        Pacientes.Clear();
        foreach (var paciente in pacientes)
        {
            Pacientes.Add(paciente);
            Console.WriteLine($"Paciente: {paciente.Nombre}");
        }
    }
    private async Task OnEliminar(Paciente paciente)
    {
        if (paciente == null)
            return;

        bool confirm = await Application.Current.MainPage.DisplayAlert(
            "¿Eliminar?", $"¿Eliminar a {paciente.Nombre}?", "Sí", "Cancelar");

        if (confirm)
        {
            var exito = await _pacienteService.EliminarPacienteAsync(paciente.Id);
            if (exito)
            {
                Pacientes.Remove(paciente);
                await Application.Current.MainPage.DisplayAlert("Eliminado", "Paciente eliminado correctamente.", "OK");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No se pudo eliminar el paciente.", "OK");
            }
        }
    }

    private async Task OnEditar(Paciente paciente)
    {
        if (paciente == null)
            return;

        // Navega a la página de edición, pasando el paciente
        await Shell.Current.GoToAsync(nameof(EditarPaciente), true, new Dictionary<string, object>
    {
        { "Paciente", paciente }
    });
    }

    private async Task OnDetalles(Paciente paciente)
    {
        if (paciente == null)
            return;

        // Navega a la página de detalles, pasando el paciente
        await Shell.Current.GoToAsync(nameof(PacienteDetalle), true, new Dictionary<string, object>
    {
        { "Paciente", paciente }
    });
    }

    private async Task OnCancelar()
    {
        await Shell.Current.GoToAsync(nameof(Inventario));
    }

    private async Task FiltrarPacientesAsync()
    {
        if (string.IsNullOrWhiteSpace(FiltroNombre))
        {
            await CargarPacientesAsync();
        }
        else
        {
            var pacientesFiltrados = await _pacienteService.ObtenerPacientesPorNombreAsync(FiltroNombre);
            Pacientes.Clear();
            foreach (var paciente in pacientesFiltrados)
                Pacientes.Add(paciente);
        }
    }

    private async Task SeleccionarPacienteAsync(Paciente paciente)
    {
        if (paciente == null)
            return;

        // Si usas Shell
        await Shell.Current.GoToAsync(nameof(PacienteDetalle), true, new Dictionary<string, object>
        {
            { "Paciente", paciente }
        });

        // Si usas NavigationPage, sería algo como:
        // await Application.Current.MainPage.Navigation.PushAsync(new PacienteFormPage(paciente));
    }

    private async Task OnNuevoPaciente()
    {
        await Shell.Current.GoToAsync(nameof(PacienteNuevo));
    }

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
