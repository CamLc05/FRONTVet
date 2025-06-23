namespace Veterinaria
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPage());

        }

        public void GoToMainApp()
        {
            MainPage = new AppShell(); // Al loguearse cambiamos a AppShell (con menú)
        }

        public void GoToLogin()
        {
            MainPage = new NavigationPage(new LoginPage()); // Al cerrar sesión regresamos al login sin menú
        }
    }
}

