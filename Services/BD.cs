using System;
using System.Collections.Generic;
using Veterinaria.Models;

namespace Veterinaria.Services
{
    public static class FakeDatabase
    {
        private static List<Propietario> _propietarios = new()
        {
            new Propietario { Nombre = "Juan", Apellido = "Pérez", Num_telefono = 5551234, Email = "juan@example.com", Direccion = "Ruiz", Fecha_creacion = DateTime.Now },
            new Propietario { Nombre = "Ana", Apellido = "Gómez", Num_telefono = 5555678, Email = "ana@example.com", Direccion = "Ruiz", Fecha_creacion = DateTime.Now  },
            new Propietario { Nombre = "Carlos",  Apellido ="Ruiz", Num_telefono = 5559876, Email = "carlos@example.com", Direccion = "Ruiz", Fecha_creacion = DateTime.Now  }
        };

        private static List<Paciente> _pacientes = new()
        {
            new Paciente
            {
                Nombre = "Chocolatito",
                Especie = TipoEspecie.Perro,
                Raza = "Beagle",
                Foto_perfil = "fondoinicio.png",
                Fecha_nacimiento = new DateTime(2019, 5, 20),
                Sexo = TipoSexo.Hembra,
                Propietario = _propietarios[0],
                Padecimiento = "Alergia alimentaria",
                Citas = new List<Cita>
                {
                    { 
                        new Cita {Id = 2, Fecha_cita = new DateTime(2023, 1, 15), Motivo = "Vacunación"}
                    },
                },
                Intervenciones = "cirugia de corazón",
                Vacunas = new List<string>
                {
                    "Vacuna antirrábica",
                    "Vacuna triple"
                }
            },
            new Paciente
            {
                Nombre = "Michi",
                Especie = TipoEspecie.Gato,
                Raza = "Persa",
                Foto_perfil = "perrito1.png",
                Fecha_nacimiento = new DateTime(2019, 5, 20),
                Sexo = TipoSexo.Hembra,
                Propietario = _propietarios[0],
                Padecimiento = "Alergia alimentaria",
                Citas = new List<Cita>
                {
                    { 
                        new Cita {Id = 2, Fecha_cita = new DateTime(2023, 1, 15), Motivo = "Vacunación"}
                    },
                },
                Intervenciones = "cirugia de corazón",
                Vacunas = new List<string>
                {
                    "Vacuna antirrábica",
                    "Vacuna triple"
                }
                },
            
            new Paciente
            {
            Nombre = "Michi",
            Especie = TipoEspecie.Gato,
            Raza = "Persa",
            Foto_perfil = "perrito1.png",
            Fecha_nacimiento = new DateTime(2019, 5, 20),
            Sexo = TipoSexo.Hembra,
            Propietario = _propietarios[0],
            Padecimiento = "Alergia alimentaria",
            Citas = new List<Cita>
            {
                { 
                    new Cita {Id = 2, Fecha_cita = new DateTime(2023, 1, 15), Motivo = "Vacunación"}
                },
            },
            Intervenciones = "cirugia de corazón",
            Vacunas = new List<string>
            {
                "Vacuna antirrábica",
                "Vacuna triple"
            }
        }
            };

        public static List<Propietario> ObtenerPropietarios() => _propietarios;

        public static List<Paciente> ObtenerPacientes() => _pacientes;

        public static void AgregarPaciente(Paciente paciente) => _pacientes.Add(paciente);

        public static void AgregarPropietario(Propietario propietario) => _propietarios.Add(propietario);
    }
}
