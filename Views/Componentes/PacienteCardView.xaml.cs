using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Veterinaria.Models;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Extensions;
using Veterinaria.Controls;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Maui.Core;

namespace Veterinaria.Views.Componentes;

public partial class PacienteCardView : ContentView
{
    public PacienteCardView()
    {
        InitializeComponent();
    }
    public static readonly BindableProperty PacienteProperty =
        BindableProperty.Create(nameof(Paciente), typeof(Paciente), typeof(PacienteCardView), propertyChanged: OnPacienteChanged);

    public Paciente Paciente
    {
        get => (Paciente)GetValue(PacienteProperty);
        set => SetValue(PacienteProperty, value);
    }

    private static void OnPacienteChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (PacienteCardView)bindable;
        control.BindingContext = control;
    }


    private async void OnEliminarSwipe(object sender, EventArgs e)
    {
        if (sender is SwipeItem item && item.CommandParameter is Paciente paciente)
        {
            await MostrarPopup();
        }
    }

    private async void OnEditarSwipe(object sender, EventArgs e)
    {
        if (sender is SwipeItem item && item.CommandParameter is Paciente paciente)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new PacienteDetalle(paciente));
            Console.WriteLine("Editar: " + paciente.Nombre);
        }
    }

    private async void OnVerPaciente(object sender, EventArgs e)
    {
        var recognizer = sender as TapGestureRecognizer;
        var paciente = recognizer?.BindingContext as Paciente;

        if (paciente != null)
        {
            // Navegar a la página detalle, pasando el paciente
            await Application.Current.MainPage.Navigation.PushAsync(new PacienteDetalle(paciente));
        }
    }
    private async void OnDetallesPaciente(object sender, EventArgs e)
    {
        if (sender is ImageButton button && button.CommandParameter is Producto paciente)
        {
            Console.WriteLine("entro");
            await Application.Current.MainPage.Navigation.PushAsync(new PacienteDetalle(Paciente));
        }
    }

    private async Task MostrarPopup()
    {
        var popup = new CustomPopupDelete(); // Reemplaza con tu clase popup

        var page = this.FindParentPage();

        if (page != null)
        {
            await page.ShowPopupAsync(popup);
        }
        else
        {
            // Opcional: mostrar mensaje de error si no encontró la página
            await Application.Current.MainPage.DisplayAlert("Error", "No se encontró la página contenedora", "OK");
        }
    }
}


