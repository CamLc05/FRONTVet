using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Veterinaria.Models; // Asegúrate de que el namespace coincida
using System; // Necesario para DateTime

namespace Veterinaria.ViewModels
{
    public class CitasViewModel : INotifyPropertyChanged
    {
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

        public CitasViewModel()
        {
            Citas = new ObservableCollection<Cita>();
            LoadCitas(); // Llama a LoadCitas para cargar los datos cuando se crea el ViewModel

            SeleccionarCitaCommand = new Command<Cita>(async (cita) =>
            {
                if (cita != null)
                {
                    CitaSeleccionada = cita;
                    await Shell.Current.DisplayAlert("Cita Seleccionada", $"Has seleccionado la cita de {cita.Paciente.Nombre} para el {cita.FechaCita:dd/MM/yyyy HH:mm} por {cita.Motivo}.", "OK");
                    // Aquí podrías agregar lógica para navegar a una página de detalles o edición
                    // Ejemplo: await Shell.Current.GoToAsync($"citaForm?citaId={cita.Id}");
                }
            });
        }

        private void LoadCitas()
        {
            // Datos de ejemplo para Paciente, Usuario, Producto, Servicio
            var paciente1 = new Paciente { Id = 1, Nombre = "Fido Canino" };
            var paciente2 = new Paciente { Id = 2, Nombre = "Michi Felino" };
            var veterinario1 = new Usuario { Id = 101, Nombre_usuario = "Dr. Animal", Rol = RolUsuario.Veterinario }; // Asumo que es 'Nombre', no 'Nombre_usuario'
            var producto1 = new Producto { Id = 1, Nombre = "Analgésico Vet" };
            var servicio1 = new Servicio { Id = 1, Nombre = "Consulta General", Costo = 50.0f };
            var servicio2 = new Servicio { Id = 2, Nombre = "Vacunación", Costo = 30.0f };


            // *** CITA PARA HOY ***
            // DateTime.Now.Date asegura que solo la fecha se use, luego agregamos la hora.
            // La fecha actual es 23 de junio de 2025.
            Citas.Add(new Cita
            {
                Id = 101,
                Paciente = paciente1,
                Veterinario = veterinario1,
                FechaCita = DateTime.Now.Date.AddHours(14).AddMinutes(0), // HOY a las 2:00 PM
                Motivo = "Revisión de rutina",
                Status = EstadoCita.Activa, // O Pendiente, según lo que quieras mostrar
                Detalles = "Chequeo general y desparasitación.",
                Peso = 12.5f,
                Medicamentos = new Dictionary<Producto, int> { { producto1, 2 } },
                Servicios = new List<Servicio> { servicio1 },
                FechaCreacion = DateTime.Now.Date
            });

            // *** CITA PARA HOY MÁS TARDE ***
            Citas.Add(new Cita
            {
                Id = 102,
                Paciente = paciente2,
                Veterinario = veterinario1,
                FechaCita = DateTime.Now.Date.AddHours(16).AddMinutes(30), // HOY a las 4:30 PM
                Motivo = "Vacunación anual",
                Status = EstadoCita.Pendiente,
                Detalles = "Vacuna antirrábica.",
                Peso = 5.0f,
                Medicamentos = new Dictionary<Producto, int>(),
                Servicios = new List<Servicio> { servicio2 },
                FechaCreacion = DateTime.Now.Date
            });


            // Citas para mañana, pasado mañana, y hace 5 días (tus ejemplos originales)
            Citas.Add(new Cita
            {
                Id = 1,
                Paciente = paciente1,
                Veterinario = veterinario1,
                FechaCita = DateTime.Now.AddDays(1).Date.AddHours(10), // Mañana
                Motivo = "Revisión anual",
                Status = EstadoCita.Pendiente,
                Detalles = "Chequeo general y vacunas.",
                Peso = 15.5f,
                Medicamentos = new Dictionary<Producto, int> { { producto1, 1 } },
                Servicios = new List<Servicio> { servicio1 },
                FechaCreacion = DateTime.Now.AddDays(-1).Date
            });

            Citas.Add(new Cita
            {
                Id = 2,
                Paciente = paciente2,
                Veterinario = veterinario1,
                FechaCita = DateTime.Now.AddDays(2).Date.AddHours(14), // Pasado mañana
                Motivo = "Consulta por cojera",
                Status = EstadoCita.Activa,
                Detalles = "Revisar pata trasera derecha.",
                Peso = 5.2f,
                Medicamentos = new Dictionary<Producto, int>(),
                Servicios = new List<Servicio> { servicio1 },
                FechaCreacion = DateTime.Now.AddDays(-2).Date
            });

            Citas.Add(new Cita
            {
                Id = 3,
                Paciente = paciente1,
                Veterinario = veterinario1,
                FechaCita = DateTime.Now.AddDays(-5).Date.AddHours(9), // Hace 5 días
                Motivo = "Vacunación",
                Status = EstadoCita.Atendida,
                Detalles = "Vacuna antirrábica aplicada.",
                Peso = 15.0f,
                Medicamentos = new Dictionary<Producto, int>(),
                Servicios = new List<Servicio> { servicio2 },
                FechaCreacion = DateTime.Now.AddDays(-5).Date
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propiedad = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propiedad));
        }
    }
}