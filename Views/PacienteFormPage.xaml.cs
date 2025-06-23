using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinaria.Models;
using Veterinaria.ViewModels;
using Veterinaria.Views;


namespace Veterinaria.Views;

[QueryProperty(nameof(Paciente), "Paciente")]
public partial class PacienteFormPage : ContentPage
{
    public PacienteFormPage()
    {
        InitializeComponent();
        BindingContext = viewModel;
    }


    private readonly PacienteFormViewModel viewModel = new PacienteFormViewModel();

    private Paciente _paciente;

    public Paciente Paciente
    {
        get => _paciente;
        set
        {
            _paciente = value;
            viewModel.Paciente = value;
        }
    }
}