using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui.Markup;
using MAUI_GymApp.Database;

namespace MAUI_GymApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        
        MauiAppBuilder builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkitMarkup()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("orbitron-bold.otf", "OrbitronBold");
            });

        Tables.CreateTables();
        
        return builder.Build();
    }
}