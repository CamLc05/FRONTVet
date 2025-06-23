// CustomEntryHandler.cs
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Platform; // Necesario para ToPlatform

namespace Veterinaria // ¡Asegúrate de que este namespace coincida con tu proyecto!
{
    // Usamos 'partial' para permitir que el código específico de la plataforma se combine.
    public partial class CustomEntryHandler : EntryHandler
    {
        public CustomEntryHandler() : base(Mapper) // Usamos 'Mapper' en lugar de 'EntryMapper' para simplificar
        {
        }

        // Sobrescribimos el mapeador para interceptar las propiedades.
        public static new PropertyMapper<Entry, IEntryHandler> Mapper =
            new PropertyMapper<Entry, IEntryHandler>(EntryHandler.Mapper)
            {
                // Sobreescribe el mapeo de la propiedad Text para aplicar nuestra personalización
                [nameof(Entry.Text)] = MapText,
                // Puedes añadir más propiedades aquí si quieres personalizar otras cosas,
                // por ejemplo, el color del placeholder o el color del texto.
            };

        // Este método se ejecutará cuando el Entry se conecte al control nativo en cada plataforma
        public static void MapText(IEntryHandler handler, Entry entry)
        {
            // Llama al método base para asegurar que la funcionalidad estándar del Entry se mantenga
            EntryHandler.MapText(handler, entry);

#if ANDROID
            // En Android, eliminamos el fondo (que es el subrayado por defecto)
            handler.PlatformView.Background = null;
            // También aseguramos que el tinte de fondo sea transparente para el enfoque
            handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#endif
        }
    }
}