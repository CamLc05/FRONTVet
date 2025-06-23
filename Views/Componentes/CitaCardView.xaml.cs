namespace Veterinaria.Views // Asegúrate de que el namespace coincida
{
    public partial class CitaCardView : ContentView
    {
        public static readonly BindableProperty CitaProperty =
            BindableProperty.Create(nameof(Cita), typeof(Models.Cita), typeof(CitaCardView), null);

        public Models.Cita Cita
        {
            get => (Models.Cita)GetValue(CitaProperty);
            set => SetValue(CitaProperty, value);
        }

        public CitaCardView()
        {
            InitializeComponent();
            BindingContext = this; // Asegura que el BindingContext de este ContentView sea la Cita
        }
    }
}