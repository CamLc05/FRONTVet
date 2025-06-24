using Veterinaria.ViewModels;
using Veterinaria.Models;
namespace Veterinaria.Views;

public partial class PacientesPages : ContentPage
{
	public PacientesPages()
	{
		InitializeComponent();
		BindingContext = new PacienteViewModel();
	}

    private void OnEliminarPaciente(object sender, Paciente paciente)
    {
        if (BindingContext is PacienteViewModel vm)
            vm.EliminarCommand.Execute(paciente);
    }

    private void OnEditarPaciente(object sender, Paciente paciente)
    {
        if (BindingContext is PacienteViewModel vm)
            vm.EditarCommand.Execute(paciente);
    }

    private void OnDetallesPaciente(object sender, Paciente paciente)
    {
        if (BindingContext is PacienteViewModel vm)
            vm.DetallesCommand.Execute(paciente);
    }

    private void OnVerPaciente(object sender, Paciente paciente)
    {
        if (BindingContext is PacienteViewModel vm)
            vm.SeleccionarCommand.Execute(paciente);
    }
}