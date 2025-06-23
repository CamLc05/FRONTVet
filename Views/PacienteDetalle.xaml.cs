using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinaria.Models;


namespace Veterinaria.Views;

public partial class PacienteDetalle : ContentPage
{
    public PacienteDetalle(Paciente paciente)
    {
        InitializeComponent();
        BindingContext = paciente;
    }
}