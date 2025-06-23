using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Veterinaria.Services;

namespace Veterinaria.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly AuthService _authService;
        private readonly Page _page;

        private string _email;
        private string _password;
        private bool _isBusy;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        public bool IsBusy
        {
            get => _isBusy;
            set { _isBusy = value; OnPropertyChanged(); }
        }

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        public LoginViewModel(AuthService authService, Page page)
        {
            _authService = authService;
            _page = page;

            LoginCommand = new Command(async () => await Login(), () => !IsBusy);
            RegisterCommand = new Command(async () => await NavigateToRegister());
        }

        private async Task Login()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                ((Command)LoginCommand).ChangeCanExecute();

                if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
                {
                    await _page.DisplayAlert("Error", "Por favor ingresa email y contraseña", "OK");
                    return;
                }

                var isAuthenticated = await _authService.Login(Email, Password);

                if (isAuthenticated)
                {
                    // Reemplaza esto por tu lógica de navegación
                    await _page.Navigation.PushAsync(new MainPage());
                }
                else
                {
                    await _page.DisplayAlert("Error", "Credenciales incorrectas", "OK");
                }
            }
            catch (Exception ex)
            {
                await _page.DisplayAlert("Error", $"Error al iniciar sesión: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
                ((Command)LoginCommand).ChangeCanExecute();
            }
        }

        private async Task NavigateToRegister()
        {
            //await _page.Navigation.PushAsync(new RegisterPage());
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
