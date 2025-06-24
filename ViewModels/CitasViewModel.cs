using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Veterinaria.Models;
using Veterinaria.Services;
using System.Threading.Tasks;
using System.Globalization;

namespace Veterinaria.ViewModels
{
    public class CitasViewModel : INotifyPropertyChanged
    {
        private readonly CitaService _citaService;

        private string _fechaFormateada;
        public string FechaFormateada
        {
            get => _fechaFormateada;
            set
            {
                if (_fechaFormateada != value)
                {
                    _fechaFormateada = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<Cita> _citas;
        public ObservableCollection<Cita> Citas
        {
            get => _citas;
            set
            {
                if (_citas != value)
                {
                    _citas = value;
                    OnPropertyChanged();
                }
            }
        }

        private Cita _citaSeleccionada;
        public Cita CitaSeleccionada
        {
            get => _citaSeleccionada;
            set
            {
                if (_citaSeleccionada != value)
                {
                    _citaSeleccionada = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand SeleccionarCitaCommand { get; }
        public ICommand RefrescarCitasCommand { get; }
        public ICommand SeleccionarDiaCommand { get; }
        public ICommand EditarCitaCommand { get; }
        public ICommand EliminarCitaCommand { get; }
        public ICommand AgregarCitaCommand { get; }



        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<DiaSemana> DiasSemana { get; set; } = new();

        public DateTime fechaSeleccionada { get; set; }

        public CitasViewModel()
        {
            CalcularSemanaActual();
            CalcularFechaFormateada();
            _citaService = new CitaService(new HttpClient());
            Citas = new ObservableCollection<Cita>();
            SeleccionarCitaCommand = new Command<Cita>(async (cita) => await SeleccionarCitaAsync(cita));
            RefrescarCitasCommand = new Command(async () => await CargarCitasPorFechaAsync(fechaSeleccionada));
            SeleccionarDiaCommand = new Command<DiaSemana>(async (dia) => await SeleccionarDiaAsync(dia));
            EditarCitaCommand = new Command<Cita>(async (cita) => await EditarCitaAsync(cita));
            EliminarCitaCommand = new Command<Cita>(async (cita) => await EliminarCitaAsync(cita));
            AgregarCitaCommand = new Command(async () => await AgregarCitaAsync());

            fechaSeleccionada = DateTime.Today;


            _ = CargarCitasPorFechaAsync(fechaSeleccionada);
        }
        private async Task EditarCitaAsync(Cita cita)
        {
            if (cita == null)
                return;

            // Aquí puedes navegar a la página de edición, pasando la cita
            await Shell.Current.GoToAsync("EditarCitaPage", true, new Dictionary<string, object>
    {
        { "Cita", cita }
    });
        }

        private async Task EliminarCitaAsync(Cita cita)
        {
            if (cita == null)
                return;

            bool confirm = await Application.Current.MainPage.DisplayAlert(
                "¿Eliminar?", $"¿Eliminar la cita de {cita.Paciente?.Nombre}?", "Sí", "Cancelar");

            if (confirm)
            {
                var exito = await _citaService.EliminarCitaAsync(cita.Id);
                if (exito)
                {
                    Citas.Remove(cita);
                    await Application.Current.MainPage.DisplayAlert("Eliminado", "Cita eliminada correctamente.", "OK");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "No se pudo eliminar la cita.", "OK");
                }
            }
        }

        private async Task AgregarCitaAsync()
        {
            // Navega a la página de nueva cita
            await Shell.Current.GoToAsync("AgregarCitaPage");
        }

        private async Task SeleccionarDiaAsync(DiaSemana dia)
        {
            if (dia != null)
            {
                fechaSeleccionada = dia.Fecha;
                await CargarCitasPorFechaAsync(fechaSeleccionada);
                foreach (var d in DiasSemana)
                    d.EsHoy = d.Fecha.Date == fechaSeleccionada.Date;
                OnPropertyChanged(nameof(DiasSemana));
            }
        }

        private void CalcularSemanaActual()
        {
            DiasSemana.Clear();
            var cultura = new CultureInfo("es-MX");
            string[] nombres = { "Lun", "Mar", "Mié", "Jue", "Vie", "Sáb" };
            DateTime hoy = DateTime.Now;
            int offset = ((int)hoy.DayOfWeek + 6) % 7; // Lunes = 0
            DateTime lunesSemana = hoy.AddDays(-offset);

            for (int i = 0; i < 6; i++)
            {
                DateTime dia = lunesSemana.AddDays(i);
                DiasSemana.Add(new DiaSemana
                {
                    Nombre = nombres[i],
                    Numero = dia.Day,
                    Fecha = dia,
                    EsHoy = dia.Date == fechaSeleccionada.Date
                });
            }

        }
        private void CalcularFechaFormateada()
        {
            var cultura = new CultureInfo("es-MX");
            var fecha = DateTime.Now.ToString("dddd d 'de' MMMM", cultura);
            FechaFormateada = char.ToUpper(fecha[0]) + fecha.Substring(1);
        }
        private async Task CargarCitasAsync()
        {
            var citas = await _citaService.ObtenerCitasAsync();
            Console.WriteLine(citas);
            Citas.Clear();
            if (citas != null)
            {
                foreach (var cita in citas)
                {
                    Console.WriteLine(cita);
                    Citas.Add(cita);
                }

            }
        }

        public async Task CargarCitasPorFechaAsync(DateTime fecha)
        {
            FechaFormateada = fecha.ToString("dd/MM/yyyy");
            var citas = await _citaService.ObtenerCitasAsync();
            Citas.Clear();
            if (citas != null)
            {
                foreach (var cita in citas.Where(c => c.FechaCita.Date == fecha.Date))
                {
                    Citas.Add(cita);
                }
            }
        }

        private async Task SeleccionarCitaAsync(Cita cita)
        {
            if (cita != null)
            {
                CitaSeleccionada = cita;
                await Shell.Current.DisplayAlert("Cita Seleccionada", $"Has seleccionado la cita de {cita.Paciente?.Nombre} para el {cita.FechaCita:dd/MM/yyyy HH:mm} por {cita.Motivo}.", "OK");
                // Aquí puedes navegar a la página de detalle o edición si lo deseas
                // await Shell.Current.GoToAsync(nameof(CitaDetallePage), true, new Dictionary<string, object> { { "Cita", cita } });
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propiedad = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propiedad));
        }
    }
    public class DiaSemana
    {
        public string Nombre { get; set; }
        public int Numero { get; set; }
        public DateTime Fecha { get; set; }
        public bool EsHoy { get; set; }
    }

}

