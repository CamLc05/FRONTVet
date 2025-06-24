using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinaria.Models;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Storage;




namespace Veterinaria.Views;

public partial class PacienteNuevo : ContentPage
{
    public List<Propietario> Propietarios { get; set; } = new List<Propietario>();
    public Propietario SelectedPropietario { get; set; }
    public PacienteNuevo()
    {
        InitializeComponent();
        CargarPropietarios(); // Simula la carga de propietarios.
        BindingContext = this;
    }

    async void OnTakePhotoClicked(object sender, EventArgs e)
    {
        // 1) Chequear + pedir permiso de cámara
        var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
        if (status != PermissionStatus.Granted)
            status = await Permissions.RequestAsync<Permissions.Camera>();

        if (status != PermissionStatus.Granted)
        {
            await DisplayAlert("Permisos", "Sin permiso de cámara no podemos tomar la foto.", "OK");
            return;
        }

        // 2) Abrir la cámara con MediaPicker
        try
        {
            var photo = await MediaPicker.Default.CapturePhotoAsync();
            if (photo == null)
                return;

            // 3) Mostrar previsualización
            var stream = await photo.OpenReadAsync();
            FotoPreview.Source = ImageSource.FromFile(photo.FullPath);

            // 4) Leer bytes si necesitas enviarlos al backend
            using var ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            byte[] fotoBytes = ms.ToArray();

            // … aquí podrías preparar tu MultipartFormDataContent y enviarlo …
        }
        catch (FeatureNotSupportedException)
        {
            await DisplayAlert("Error", "La cámara no está soportada en este dispositivo.", "OK");
        }
        catch (PermissionException)
        {
            await DisplayAlert("Error", "No se concedieron permisos para la cámara.", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error inesperado", ex.Message, "OK");
        }
    }


    private void CargarPropietarios()
    {
        // Aquí deberías obtener la lista de propietarios de tu base de datos.
        // Este es solo un ejemplo con datos simulados.
        Propietarios.Add(new Propietario { Nombre = "Juan Pérez", Id = 1 });
        Propietarios.Add(new Propietario { Nombre = "María López", Id = 2 });
        Propietarios.Add(new Propietario { Nombre = "Carlos García", Id = 3 });
        Propietarios.Add(new Propietario { Nombre = "Juan Pérez", Id = 4 });
        Propietarios.Add(new Propietario { Nombre = "María López", Id = 5 });
        Propietarios.Add(new Propietario { Nombre = "Carlos García", Id = 6 });


        // Asegúrate de asignar esta lista al Picker
        PropietariosListView.ItemsSource = Propietarios.Take(5);
    }

    // Filtra la lista de propietarios al escribir en el campo de búsqueda
    private void BuscarPropietarioEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        var searchQuery = e.NewTextValue.ToLower();
        var filteredList = Propietarios
            .Where(p => p.Nombre.ToLower().Contains(searchQuery))
            .Take(5) // Limitar el número de resultados a 5
            .ToList();

        // Asignar la lista filtrada al CollectionView
        PropietariosListView.ItemsSource = filteredList;
    }

    private void PropietariosListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // tomamos el primer elemento seleccionado
        var seleccionado = e.CurrentSelection.FirstOrDefault() as Propietario;
        if (seleccionado == null)
            return;

        // guardamos la selección
        SelectedPropietario = seleccionado;

        // reflejamos su nombre en el Entry
        BuscarPropietarioEntry.Text = seleccionado.Nombre;

        // opcional: ocultar la lista tras elegir
        PropietariosListView.IsVisible = false;
    }

    // Evento para guardar la cita
    private async void Guardar_Clicked(object sender, EventArgs e)
    {
        if (SelectedPropietario != null)
        {
            await DisplayAlert("Guardado", "El paciente ha sido registrado.", "OK");
            await Application.Current.MainPage.Navigation.PushAsync(new Inventario());
        }
        else
        {
            await DisplayAlert("Error", "Debes seleccionar un propietario.", "OK");
        }
    }

    // Evento para cancelar la operación
    private async void Cancelar_Clicked(object sender, EventArgs e)
    {
        await Application.Current.MainPage.Navigation.PushAsync(new PacientesPages());
    }

    private async void OnAgregarPropietario_Clicked(object sender, EventArgs e)
    {
        // Navega a la página de crear propietario.
        await Navigation.PushAsync(new NuevoPropietario());
    }

}