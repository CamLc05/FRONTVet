using Veterinaria.Views;

namespace Veterinaria
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //Routing.RegisterRoute("pacienteForm", typeof(PacienteFormPage)); 


        }

        private void OnLogoutClicked(object sender, EventArgs e)
        {
            ((App)Application.Current).GoToLogin(); // Regresar al login sin menú
        }
    }
}
