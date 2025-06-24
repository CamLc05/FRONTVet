using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinaria.Models;


namespace Veterinaria.Views;

[QueryProperty(nameof(Paciente), "Paciente")]
public partial class PacienteDetalle : ContentPage
{
    private Paciente _paciente;
    public Paciente Paciente
    {
        get => _paciente;
        set
        {
            _paciente = value;
            BindingContext = _paciente; // O usa un ViewModel si prefieres
        }
    }
    public PacienteDetalle()
    {
        InitializeComponent();
        BindingContext = new PacienteDetalleViewModel(Paciente);
    }
}
