using System.Globalization;

namespace Veterinaria.Views;

public partial class Citas : ContentPage
{
    public string FechaFormateada { get; set; }
    public Citas()
	{
		InitializeComponent();
        MostrarSemanaActual();

        void MostrarSemanaActual()
        {
            var cultura = new CultureInfo("es-MX");
            string[] diasSemana = new[] { "Lun", "Mar", "Mié", "Jue", "Vie", "Sáb" };

            SemanaGrid.Children.Clear();
            SemanaGrid.ColumnDefinitions.Clear();

            for (int i = 0; i < 6; i++)
            {
                SemanaGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            }

            DateTime hoy = DateTime.Now;
            int offset = ((int)hoy.DayOfWeek + 6) % 7; // Lunes = 0
            DateTime lunesSemana = hoy.AddDays(-offset);

            // Fila 0: nombres de días
            for (int col = 0; col < 6; col++)
            {
                var label = new Label
                {
                    Text = diasSemana[col],
                    HorizontalTextAlignment = TextAlignment.Center,
                    FontSize = 17
                };
                Grid.SetColumn(label, col);
                Grid.SetRow(label, 0);
                SemanaGrid.Children.Add(label);
            }

            // Fila 1: números del día
            for (int col = 0; col < 6; col++)
            {
                DateTime dia = lunesSemana.AddDays(col);
                bool esDiaActual = dia.Date == DateTime.Now.Date;

                if (esDiaActual)
                {
                    var frame = new Frame
                    {
                        Padding = 0,
                        CornerRadius = 20,
                        HasShadow = false,
                        BackgroundColor = Color.FromArgb("#F17105"),
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        WidthRequest = 40,
                        HeightRequest = 40,
                        Content = new Label
                        {
                            Text = dia.Day.ToString(),
                            HorizontalTextAlignment = TextAlignment.Center,
                            VerticalTextAlignment = TextAlignment.Center,
                            FontSize = 18,
                            TextColor = Colors.White,
                        }
                    };
                    Grid.SetColumn(frame, col);
                    Grid.SetRow(frame, 1);
                    SemanaGrid.Children.Add(frame);
                }
                else
                {
                    var diaLabel = new Label
                    {
                        Text = dia.Day.ToString(),
                        FontSize = 18,
                        TextColor = Colors.Black,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center,
                        WidthRequest = 40,
                        HeightRequest = 40,
                    };
                    Grid.SetColumn(diaLabel, col);
                    Grid.SetRow(diaLabel, 1);
                    SemanaGrid.Children.Add(diaLabel);
                }

            }
        }


        var cultura = new CultureInfo("es-MX"); // o "es-ES" también sirve
        FechaFormateada = DateTime.Now.ToString("dddd d 'de' MMMM", cultura);

        // Aplica capitalización a la primera letra del día
        FechaFormateada = char.ToUpper(FechaFormateada[0]) + FechaFormateada.Substring(1);

        // Asignar al Label si lo haces directo
        lblFecha.Text = FechaFormateada;
    }

    private async void OnEditar(object sender, EventArgs e)
    {
        await Application.Current.MainPage.Navigation.PushAsync(new Calendario());
    }


}