using MAUI_GymApp.Rider.Database.Objects;
using Microcharts;
using Microcharts.Maui;
using SkiaSharp;

namespace MAUI_GymApp.Views;

public class StatsTab : ContentView
{
    private string[,] CategoryTotals =
    {
        {"Arms", "0"}, {"Legs", "0"}, {"Back", "0"}, {"Chest", "0"}, {"Abs", "0"}
    };

    private string[,] MuscleTotals =
    {
        { "Bicep", "0" }, { "Tricep", "0" }, { "Forearm", "0" }, { "Calf", "0" }, { "Lats", "0" }, { "Shoulders", "0" }, { "Glutes", "0" },
        { "Traps", "0" }, { "Quadriceps", "0" }, {"Hamstrings", "0"}, {"L Back", "0"}, {"U Back", "0"}, {"Abs", "0"}, {"Chest", "0"}
    };

    private Switch _switch = new Switch() { OnColor = Colors.White, ThumbColor = Colors.Black};
    private VerticalStackLayout _layout;
    private VerticalStackLayout _card;

    public StatsTab()
    {
        _card = new VerticalStackLayout()
        {
            Padding = new Thickness(16, 12),
            Spacing = 16,
            Children =
            {
                _switch,
                Controls.MakeRadarChart(360, BuildEntries(CategoryTotals, Category.GetCategoryChartStats()), 28),
            }
        };

        _layout = new VerticalStackLayout()
        {
            Padding = new Thickness(16, 16, 16, 0),
            Spacing = 16,
            Children =
            {
                Controls.MakeLabel("Radar Charts", 24),
                new Border
                {
                    BackgroundColor = Colors.LightGray,
                    Content = _card
                }
            }
        };

        _switch.Toggled += OnToggled;

        Content = new ScrollView { Content = _layout };
    }

    private ChartEntry[] BuildEntries(string[,] totals, List<ChartPasser> setList)
    {
        ChartEntry[] entries = new ChartEntry[totals.GetLength(0)];

        for (int i = 0; i < totals.GetLength(0); i++)
        {
            int count = 0;

            foreach (ChartPasser set in setList)
            {
                if (set.Name == totals[i, 0])
                {
                    count += set.Sets;
                }
            }

            entries[i] = new ChartEntry(count)
            {
                Label = totals[i, 0],
                ValueLabel = Convert.ToString(count),
                Color = SKColor.Parse("#000000")
            };
        }

        return entries;
    }
    
    void OnToggled(object sender, ToggledEventArgs e)
    {
        if (_switch.IsToggled)
        {
            _card.Children.RemoveAt(1);
            _card.Children.Add(Controls.MakeRadarChart(360, BuildEntries(MuscleTotals, Muscle.GetMuscleChartStats()), 28));
        }
        else
        {
            _card.Children.RemoveAt(1);
            _card.Children.Add(Controls.MakeRadarChart(360, BuildEntries(CategoryTotals, Category.GetCategoryChartStats()), 28));
        }
    }
}
