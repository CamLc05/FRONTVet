using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinaria.Models;

namespace Veterinaria
{
    public static class AppGlobals
    {
        public static Usuario UsuarioActual { get; set; }
        public static DateTime FechaSeleccionada { get; set; } = DateTime.Now;
    }
}
