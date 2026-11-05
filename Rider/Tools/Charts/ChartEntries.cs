using MAUI_GymApp.Rider.Database.Objects;
using Microcharts;
using SkiaSharp;
using Chart = MAUI_GymApp.Rider.Database.Objects.Chart;

namespace MAUI_GymApp;

public class ChartEntries
{
    public static ChartEntry[] FromLabels(string[] labels, List<Chart.ChartParser> data)
    {
        ChartEntry[] entries = new ChartEntry[labels.Length];

        for (int i = 0; i < labels.Length; i++)
        {
            int total = 0;

            foreach (Chart.ChartParser item in data)
            {
                if (item.Name == labels[i])
                {
                    total += item.Value;
                }
            }

            entries[i] = MakeEntry(labels[i], total);
        }

        return entries;
    }

    public static ChartEntry[] ByWeek(int numOfWeeks, List<WorkoutParser> workouts, Func<WorkoutParser, int> getValue)
    {
        long secondsInWeek = 604800;
        long now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        int[] totals = new int[numOfWeeks];

        foreach (WorkoutParser workout in workouts)
        {
            int weeksAgo = (int)((now - workout.Date) / secondsInWeek);

            if (weeksAgo < numOfWeeks)
            {
                totals[numOfWeeks - 1 - weeksAgo] += getValue(workout);
            }
        }

        ChartEntry[] entries = new ChartEntry[numOfWeeks];

        for (int i = 0; i < numOfWeeks; i++)
        {
            int weeksAgo = numOfWeeks - 1 - i;
            string label;

            if (weeksAgo == 0)
            {
                label = "Now";
            }
            else
            {
                label = weeksAgo + "w";
            }

            entries[i] = MakeEntry(label, totals[i]);
        }

        return entries;
    }

    public static ChartEntry MakeEntry(string label, int value) => new(value)
    {
        Label = label,
        ValueLabel = Convert.ToString(value),
        Color = SKColor.Parse("#000000")
    };
}