using System.Windows.Input;
using Veterinaria.Models;
using Microsoft.Maui.Controls;
using Veterinaria.Views;
public class PacienteDetalleViewModel
{
    public Paciente Paciente { get; }
    public ICommand VolverCommand { get; }
    public ICommand EditarCommand { get; }
    public ICommand CancelarCommand { get; }

    public PacienteDetalleViewModel(Paciente paciente)
    {
        Paciente = paciente;
        VolverCommand = new Command(async () => await Volver());
        EditarCommand = new Command(async () => await Editar());
        CancelarCommand = new Command(async () => await Cancelar());
    }

    private async Task Volver() => await Application.Current.MainPage.Navigation.PopAsync();

    private async Task Editar()
    {
        // Navega a la página de edición, pasando el paciente
        // await Application.Current.MainPage.Navigation.PushAsync(new EditarPacientePage(Paciente));
    }

    private async Task Cancelar() => await Application.Current.MainPage.Navigation.PushAsync(new PacientesPages());
}
