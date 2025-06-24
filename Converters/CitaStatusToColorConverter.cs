using System;
using System.Globalization;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Veterinaria.Models; // Asegúrate de que el namespace coincida

namespace Veterinaria.Converters
{
    public class CitaStatusToColorConverter : IValueConverter // Renombrado aquí
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is EstadoCita status) // Usa EstadoCita
            {
                return status switch
                {
                    EstadoCita.pendiente => Colors.Orange,
                    EstadoCita.activa => Colors.Blue,
                    EstadoCita.atendida => Colors.Green,
                    _ => Colors.Gray,
                };
            }
            return Colors.Gray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}