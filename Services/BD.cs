using System;
using System.Collections.Generic;
using Veterinaria.Models;
using Veterinaria.Models;

namespace Veterinaria.Services
{
    public static class FakeDatabase
    {
        private static List<Propietario> _propietarios = new()
        {
            new Propietario { Nombre = "Juan", Apellido = "Pérez", Num_telefono = 5551234, Email = "juan@example.com", Direccion = "Ruiz", Fecha_creacion = DateTime.Now },
            new Propietario { Nombre = "Ana", Apellido = "Gómez", Num_telefono = 5555678, Email = "ana@example.com", Direccion = "Ruiz", Fecha_creacion = DateTime.Now },
            new Propietario { Nombre = "Carlos", Apellido = "Ruiz", Num_telefono = 5559876, Email = "carlos@example.com", Direccion = "Ruiz", Fecha_creacion = DateTime.Now }
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
                    new Cita { Id = 2, FechaCita = DateTime.Now.AddDays(1).Date.AddHours(10), Motivo = "Vacunación" }
                },
                Intervenciones = "Cirugía de corazón",
                Vacunas = new List<string> { "Vacuna antirrábica", "Vacuna triple" }
            },
            new Paciente
            {
                Nombre = "Michi",
                Especie = TipoEspecie.Gato,
                Raza = "Persa",
                Foto_perfil = "perrito1.png",
                Fecha_nacimiento = new DateTime(2019, 5, 20),
                Sexo = TipoSexo.Hembra,
                Propietario = _propietarios[1],
                Padecimiento = "Gastritis felina",
                Citas = new List<Cita>
                {
                    new Cita { Id = 3, FechaCita = DateTime.Now.AddDays(2).Date.AddHours(11), Motivo = "Consulta general" }
                },
                Intervenciones = "Limpieza dental",
                Vacunas = new List<string> { "Vacuna triple felina" }
            }
        };

        private static List<Producto> _productos = new()
        {
            new Producto
            {
                Id = 1,
                Nombre = "Antibiótico Canino",
                Gramaje = "250mg",
                Precio = 180.00f,
                Tipo = TipoProducto.Medicamento,
                Fecha_vencimiento = new DateTime(2025, 12, 31),
                Disponibilidad = 2
            },
            new Producto
            {
                Id = 2,
                Nombre = "Guantes quirúrgicos",
                Gramaje = "Talla M",
                Precio = 35.50f,
                Tipo = TipoProducto.Quirurgico,
                Fecha_vencimiento = new DateTime(2027, 6, 1),
                Disponibilidad = 100
            },
            new Producto
            {
                Id = 3,
                Nombre = "Shampoo para gatos",
                Gramaje = "300ml",
                Precio = 75.00f,
                Tipo = TipoProducto.Estetica,
                Fecha_vencimiento = new DateTime(2026, 3, 15),
                Disponibilidad = 45
            }
        };

        // Métodos públicos
        public static List<Propietario> ObtenerPropietarios() => _propietarios;
        public static List<Paciente> ObtenerPacientes() => _pacientes;
        public static List<Producto> ObtenerProductos() => _productos;

        public static void AgregarPropietario(Propietario propietario) => _propietarios.Add(propietario);
        public static void AgregarPaciente(Paciente paciente) => _pacientes.Add(paciente);
        public static void AgregarProducto(Producto producto) => _productos.Add(producto);
    }
}

