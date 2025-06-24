using System.Collections.ObjectModel;
using Veterinaria.Models;
using Veterinaria.Services;
/*
namespace Veterinaria.Views;

public partial class Ventas : ContentPage
{
	public Ventas()
	{
		InitializeComponent();
		// Instancia de ServicioService (puedes inyectar HttpClient según tu configuración)
		_servicioService = new ServicioService(new HttpClient());

		// Llenar la lista de ventas con los servicios
		CargarServicios();

	}
	
	private async void CargarServicios()
	{
		var servicios = await _servicioService.ObtenerServiciosAsync();
		VentasCollection.Clear();
		foreach (var servicio in servicios)
		{
			VentasCollection.Add(new Venta
			{
				Tipo = servicio.Nombre,
				Fecha = DateTime.Now.ToString("dddd dd MMMM yyyy"), // Puedes ajustar la fecha según tu lógica
				Total = $"${servicio.Costo:0.00} MXN"
			});
		}
		VentasList.ItemsSource = VentasCollection;
	}
}

public class Venta
{
	public string Tipo { get; set; }
	public string Fecha { get; set; }
	public string Total { get; set; }
}
	
	*/
