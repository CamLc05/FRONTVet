using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinaria.Models;


namespace Veterinaria.Views;

public partial class DetallesVenta : ContentPage
{
    public DetallesVenta()
    {
        InitializeComponent();
    }
    
    private async void Cancelar_Clicked(object sender, EventArgs e)
    {
        await Application.Current.MainPage.Navigation.PushAsync(new Ventas());
    }
}