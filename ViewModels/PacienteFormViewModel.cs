using System.Collections.ObjectModel;
using System.Windows.Input;
using Veterinaria.Models;
using Veterinaria.Services;
using Microsoft.Maui.Controls;

namespace Veterinaria.ViewModels;

public class PacienteFormViewModel : BindableObject
{
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

    public string Titulo => Paciente?.Nombre ?? "Nuevo paciente";

    public ObservableCollection<string> Especies { get; set; } =
        new ObservableCollection<string> { "Perro", "Gato", "Hamster", "Ave" };

    public ObservableCollection<Propietario> Propietarios { get; set; }

    public ICommand GuardarCommand { get; }

    public PacienteFormViewModel()
    {
        Propietarios = new ObservableCollection<Propietario>(FakeDatabase.ObtenerPropietarios());

        GuardarCommand = new Command(() =>
        {
            Application.Current.MainPage.DisplayAlert("Guardado", "Paciente actualizado", "OK");
        });
    }
}