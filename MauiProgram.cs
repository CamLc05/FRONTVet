using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Handlers.Compatibility; // ¡Asegúrate de añadir esta línea!

namespace Veterinaria; // Tu namespace actual

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("Quicksand-Light.ttf", "QuicksandLight");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .ConfigureMauiHandlers(handlers => // ¡Añade este bloque!
            {
#if ANDROID
                // Para Android, elimina el subrayado nativo
                handlers.AddHandler(typeof(Entry), typeof(Veterinaria.CustomEntryHandler));
#elif IOS
                // Para iOS, quita el estilo de borde por defecto
                handlers.AddHandler(typeof(Entry), typeof(Veterinaria.CustomEntryHandler));
#elif WINDOWS
                // Para Windows, ajusta el grosor y color del borde
                handlers.AddHandler(typeof(Entry), typeof(Veterinaria.CustomEntryHandler));
#endif
            });


#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}