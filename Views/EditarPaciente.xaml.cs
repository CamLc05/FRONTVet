using Veterinaria.ViewModels;
using Veterinaria.Models;

namespace Veterinaria.Views;

[QueryProperty(nameof(Paciente), "Paciente")]
public partial class EditarPaciente : ContentPage
{
    private EditarPacienteViewModel _viewModel;

    public Paciente Paciente
    {
        get => _viewModel?.Paciente;
        set
        {
            if (_viewModel != null)
                _viewModel.Paciente = value;
        }
    }

    public EditarPaciente()
    {
        InitializeComponent();
        _viewModel = new EditarPacienteViewModel();
        BindingContext = _viewModel;
    }

    private async void Guardar_Clicked(object sender, EventArgs e)
    {
        await _viewModel.GuardarAsync();
    }

    private async void Cancelar_Clicked(object sender, EventArgs e)
    {
        await _viewModel.CancelarAsync();
    }
}
