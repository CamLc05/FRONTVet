using System.Globalization;

namespace Veterinaria.Views;

public partial class NuevaCita : ContentPage
{
    private DateTime fechaSeleccionada;
    public NuevaCita()
	{
		InitializeComponent();
        fechaSeleccionada = DateTime.Now;
        LlenarCalendario();
    }
    private void OnDateSelected(object sender, EventArgs e)
    {
        // Obtienes el bot�n que fue presionado
        var selectedButton = (Button)sender;
        var selectedDate = selectedButton.Text; // Aqu� obtienes la fecha seleccionada

        // Aqu� puedes usar la fecha seleccionada
        DisplayAlert("Fecha seleccionada", "Seleccionaste el d�a: " + selectedDate, "OK");
    }

    // M�todo para llenar el calendario con los d�as del mes
    private void LlenarCalendario()
    {
        var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

        // Calcular el d�a de la semana en que comienza el mes (0 = domingo, 1 = lunes,...)
        int dayOfWeek = (int)firstDayOfMonth.DayOfWeek;
        dayOfWeek = (dayOfWeek == 0) ? 7 : dayOfWeek; // Ajustamos el domingo para que sea el �ltimo (7)

        // Limpiar el Grid de d�as
        CalendarioGrid.Children.Clear();

        // Definir las filas y columnas
        int row = 1, col = dayOfWeek - 1;

        // Mostrar el mes y el a�o actuales en la parte superior
        string monthName = firstDayOfMonth.ToString("MMMM yyyy", new CultureInfo("es-MX"));
        lblFecha.Text = monthName;

        // Agregar los d�as de la semana (Lun, Mar, Mi�, ...)
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

            // Colocar el evento cuando se hace clic en el d�a
            button.Clicked += (sender, e) => OnDateSelected(sender, e);

            // Colocamos el bot�n en la posici�n adecuada
            Grid.SetRow(button, row);
            Grid.SetColumn(button, col);
            CalendarioGrid.Children.Add(button);

            // Ajustar la columna y fila para el siguiente d�a
            col++;
            if (col > 6)  // Si llegamos al final de la semana (domingo), pasamos a la siguiente fila
            {
                col = 0;
                row++;
            }
        }
    }



    // Evento para manejar la selecci�n de la fecha

}
