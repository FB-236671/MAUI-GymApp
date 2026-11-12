using MAUI_GymApp.Rider.Database.Objects;

namespace MAUI_GymApp;

public static class ChartStats
{
    private static readonly string[] Categories = { "Arms", "Legs", "Back", "Chest", "Abs" };

    private static readonly string[] Muscles =
    {
        "Bicep", "Tricep", "Forearm", "Calf", "Lats", "Shoulders", "Glutes",
        "Chest", "Quadriceps", "Hamstrings", "Traps", "Abs", "Upper Back", "Lower Back"
    };

    public static List<View> All(int userId) => new()
    {
        new ChartCard(
            new ChartDefinition
            {
                Title = "Category Chart",
                Type = ChartType.Radar,
                GetEntries = () => ChartEntries.FromLabels(Categories, Chart.GetCategoryChartStats(userId))
            },
            new ChartDefinition
            {
                Title = "Muscle Chart",
                Type = ChartType.Radar,
                GetEntries = () => ChartEntries.FromLabels(Muscles, Chart.GetMuscleChartStats(userId))
            }),

        new ChartCard(
            new ChartDefinition()
            {
                Title = "Weekly Duration",
                Type = ChartType.Bar,
                GetEntries = () => ChartEntries.ByWeek(8, Workout.GetWorkoutsUntilDate(userId, DateTimeOffset.UtcNow.AddDays(-56).ToUnixTimeSeconds()), workout => workout.Duration)
            },
            new ChartDefinition()
            {
                Title = "Weekly Volume",
                Type = ChartType.Bar,
                GetEntries = () => ChartEntries.ByWeek(8, Workout.GetWorkoutsUntilDate(userId, DateTimeOffset.UtcNow.AddDays(-56).ToUnixTimeSeconds()), workout => workout.Volume)
            })
    };
}