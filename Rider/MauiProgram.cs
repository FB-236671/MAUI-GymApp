using CommunityToolkit.Maui.Markup;
using MAUI_GymApp.Database;
using Microcharts.Maui;

namespace MAUI_GymApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        
        MauiAppBuilder builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkitMarkup()
            .UseMicrocharts()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("orbitron-bold.otf", "OrbitronBold");
            });

        Tables.CreateTables();
        Tables.TablePrePopulation();
        
        return builder.Build();
    }
}