using System.Globalization;
using Veterinaria.Models;

namespace Veterinaria.Views;

public partial class EditarCita : ContentPage
{
    private DateTime fechaSeleccionada;
    private Cita citaEditando;

    // Constructor para nueva cita
    public EditarCita()
    {
        InitializeComponent();
        fechaSeleccionada = DateTime.Now;
        LlenarCalendario();
    }

    // Constructor para editar cita
    public EditarCita(Cita cita) : this()
    {
        citaEditando = cita;
        if (citaEditando != null)
        {
            // Poblar los campos de la UI
            NombreEntry.Text = citaEditando.Paciente?.Nombre ?? "";
            MotivoEntry.Text = citaEditando.Motivo ?? "";
            fechaSeleccionada = citaEditando.FechaCita;
            // Si tienes Picker para horario, selecciona el valor correspondiente aquí
            // Si tienes más campos, asígnalos aquí
        }
    }

    private void OnDateSelected(object sender, EventArgs e)
    {
        // Obtienes el botón que fue presionado
        var selectedButton = (Button)sender;
        var selectedDate = selectedButton.Text; // Aquí obtienes la fecha seleccionada

        // Aquí puedes usar la fecha seleccionada
        DisplayAlert("Fecha seleccionada", "Seleccionaste el día: " + selectedDate, "OK");
    }

    // Método para llenar el calendario con los días del mes
    private void LlenarCalendario()
    {
        var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

        // Calcular el día de la semana en que comienza el mes (0 = domingo, 1 = lunes,...)
        int dayOfWeek = (int)firstDayOfMonth.DayOfWeek;
        dayOfWeek = (dayOfWeek == 0) ? 7 : dayOfWeek; // Ajustamos el domingo para que sea el último (7)

        // Limpiar el Grid de días
        CalendarioGrid.Children.Clear();

        // Definir las filas y columnas
        int row = 1, col = dayOfWeek - 1;

        // Mostrar el mes y el año actuales en la parte superior
        string monthName = firstDayOfMonth.ToString("MMMM yyyy", new CultureInfo("es-MX"));
        lblFecha.Text = monthName;

        // Agregar los días de la semana (Lun, Mar, Mié, ...)
        string[] diasSemana = new[] { "L", "M", "Mi", "J", "V", "S", "D" };
        for (int i = 0; i < 7; i++)
        {
            var label = new Label
            {
                Text = diasSemana[i],
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = 14
            };
            Grid.SetColumn(label, i);
            Grid.SetRow(label, 0);
            CalendarioGrid.Children.Add(label);
        }

        // Llenar las fechas en el calendario
        for (int day = 1; day <= daysInMonth; day++)
        {
            var button = new Button
            {
                Text = day.ToString(),
                BackgroundColor = Colors.White,
                TextColor = Colors.Black,
                WidthRequest = 50,
                HeightRequest = 50
            };

            // Colocar el evento cuando se hace clic en el día
            button.Clicked += (sender, e) => OnDateSelected(sender, e);

            // Colocamos el botón en la posición adecuada
            Grid.SetRow(button, row);
            Grid.SetColumn(button, col);
            CalendarioGrid.Children.Add(button);

            // Ajustar la columna y fila para el siguiente día
            col++;
            if (col > 6)  // Si llegamos al final de la semana (domingo), pasamos a la siguiente fila
            {
                col = 0;
                row++;
            }
        }
    }

    private async void Guardar_Clicked(object sender, EventArgs e)
    {
        // Obtén los valores de la UI
        string nombrePaciente = NombreEntry.Text;
        string motivo = MotivoEntry.Text;
        // Puedes obtener el horario seleccionado del Picker si lo necesitas

        if (citaEditando != null)
        {
            // EDITAR: Actualiza la cita existente
            if (citaEditando.Paciente != null)
                citaEditando.Paciente.Nombre = nombrePaciente;
            citaEditando.Motivo = motivo;
            citaEditando.FechaCita = fechaSeleccionada; // Ya está actualizado si el usuario cambió la fecha

            await DisplayAlert("Editado", "La cita ha sido actualizada.", "OK");
        }
        else
        {
            // Lógica para crear una nueva cita (opcional)
        }

        await Navigation.PopAsync();
    }

    private async void OnAgregarMedicinaClicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new Veterinaria.Views.Modals.AgregarMedicinaModal());
    }

    private async void OnAgregarServicioClicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new Veterinaria.Views.Modals.AgregarServicioModal());
    }
}
