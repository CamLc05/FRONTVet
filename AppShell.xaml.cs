using Veterinaria.Views;

namespace Veterinaria
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //Routing.RegisterRoute("pacienteForm", typeof(PacienteFormPage)); 
            Shell.SetNavBarIsVisible(this, false);
            Routing.RegisterRoute(nameof(PacienteDetalle), typeof(PacienteDetalle));
            Routing.RegisterRoute(nameof(EditarPaciente), typeof(EditarPaciente));
            Routing.RegisterRoute(nameof(PacienteNuevo), typeof(PacienteNuevo));


        }

        private void OnLogoutClicked(object sender, EventArgs e)
        {
            ((App)Application.Current).GoToLogin(); // Regresar al login sin menú
        }
    }
}
