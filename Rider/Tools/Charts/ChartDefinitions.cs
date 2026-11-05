using Microcharts;
using Microcharts.Maui;

namespace MAUI_GymApp;

public class ChartDefinition
{
    public string Title { get; set; }
    public ChartType Type { get; set; }
    public Func<ChartEntry[]> GetEntries { get; set; }
    public int Height { get; set; } = 360;
    public int TextSize { get; set; } = 20;

    public ChartView Build() => UIElements.MakeChart(Type, Height, GetEntries(), TextSize);
}

public enum ChartType { Radar, Bar, Line}