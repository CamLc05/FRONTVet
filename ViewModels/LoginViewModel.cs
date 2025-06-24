using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Veterinaria.Services;
using Veterinaria.Views;

namespace Veterinaria.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {

        private readonly UsuarioService _usuarioService;
        private readonly Page _page;

        public string Email { get; set; }
        public string Password { get; set; }
        public ICommand LoginCommand { get; }
        public Action<string, string, string>? MostrarMensaje { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        public LoginViewModel(UsuarioService usuarioService, Page page)
        {
            _page = page;
            _usuarioService = usuarioService;
            LoginCommand = new Command(async () => await LoginAsync());
        }

        private async Task LoginAsync()
        {
            var usuario = await _usuarioService.LoginAsync(Email, Password);
            if (usuario != null)
            {
                AppGlobals.UsuarioActual = usuario;
                ((App)Application.Current).GoToMainApp();
            }
            else
            {
                MostrarMensaje?.Invoke("Error", "Credenciales incorrectas", "OK");
            }

        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
