using System;
using System.Collections.Generic;

namespace Veterinaria.Models
{
    public class Cita
    {
        public int Id { get; set; }
        public Paciente Paciente { get; set; }
        public Usuario Veterinario { get; set; }
        public DateTime FechaCita { get; set; } // Renombrado de Fecha_cita a FechaCita (PascalCase)
        public string Motivo { get; set; }
        public EstadoCita Status { get; set; } // Usa tu enum EstadoCita
        public string Detalles { get; set; }
        public float? Peso { get; set; } // Cambiado a float? para permitir nulos si es opcional
        public Dictionary<Producto, int> Medicamentos { get; set; } // Renombrado a plural y Producto
        public List<Servicio> Servicios { get; set; } // Renombrado a plural
        public DateTime FechaCreacion { get; set; } // Renombrado de Fecha_creacion a FechaCreacion (PascalCase)

        // Constructor para inicializar colecciones y evitar NullReferenceException
        public Cita()
        {
            Medicamentos = new Dictionary<Producto, int>();
            Servicios = new List<Servicio>();
        }
    }
}