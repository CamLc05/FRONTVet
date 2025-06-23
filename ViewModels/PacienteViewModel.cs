using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Veterinaria.Models;
using Veterinaria.Services;
using System.Windows.Input;
using Microsoft.Maui.Controls;



namespace Veterinaria.ViewModels;

public class PacienteViewModel : INotifyPropertyChanged
{
    public ObservableCollection<Paciente> Pacientes { get; set; }
    public ICommand SeleccionarCommand { get; }

    public PacienteViewModel()
    {
        var lista = FakeDatabase.ObtenerPacientes();
        Pacientes = new ObservableCollection<Paciente>(lista);
       
        SeleccionarCommand = new Command<Paciente>(async (paciente) =>
        {
            await Shell.Current.GoToAsync("patcienteForm", true, new Dictionary<string, object>
            {
                { "Paciente", paciente }
            });
        });
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propiedad = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propiedad));
    }
}