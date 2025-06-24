using Veterinaria.Views;
using Veterinaria.ViewModels;
using Veterinaria.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Veterinaria;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();


        // Fix: Pass an instance of HttpClient to UsuarioService as required by its constructor  
        var httpClient = new HttpClient();
        var usuarioService = new UsuarioService(httpClient);

        var viewModel = new LoginViewModel(usuarioService, this);
        viewModel.MostrarMensaje = async (titulo, mensaje, boton) =>
            await DisplayAlert(titulo, mensaje, boton);
        BindingContext = viewModel;
    }

    private async void onClick(object sender, EventArgs e)
    {
        ((App)Application.Current).GoToMainApp();
    }
    private async void onClickRegistro(object sender, EventArgs e)
    {
        await Application.Current.MainPage.Navigation.PushAsync(new Registro());
    }

}
    

