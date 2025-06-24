using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinaria.Models; 

namespace Veterinaria.Views;

public partial class EditarPaciente : ContentPage
{
    public Paciente Paciente { get; set; }
    
    public EditarPaciente(Paciente paciente)
    {
        InitializeComponent();
        Paciente = paciente;
        BindingContext = this;
    }
    

    private async void Guardar_Clicked(object sender, EventArgs e)
    {
        // Aquí podrías actualizar la base fake si hicieras una modificación real
        await DisplayAlert("Guardado", "El producto ha sido actualizado.", "OK");
        await Application.Current.MainPage.Navigation.PushAsync(new PacientesPages());
    }

    private async void Cancelar_Clicked(object sender, EventArgs e)
    {
        await Application.Current.MainPage.Navigation.PushAsync(new PacientesPages());
    }



}