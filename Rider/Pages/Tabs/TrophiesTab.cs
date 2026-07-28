using MAUI_GymApp.Rider.Database.Objects;

namespace MAUI_GymApp.Views;

public class TrophiesTab : ContentView
{
    private List<RecordPasser> recordList = Record.GetRecordsByUser(MainPage.User.UserId);

    public TrophiesTab()
    {
        VerticalStackLayout layout = new VerticalStackLayout
        {
            Padding = new Thickness(16, 16, 16, 0),
            Spacing = 8,
            Children = { BuildHeaderRow() }
        };

        foreach (RecordPasser record in recordList)
            layout.Add(BuildTrophyCard(record));

        Content = new ScrollView { Content = layout };
    }

    private static Grid MakeRowGrid() => new Grid
    {
        ColumnDefinitions =
        {
            new ColumnDefinition { Width = new GridLength(48) },
            new ColumnDefinition { Width = GridLength.Star },
            new ColumnDefinition { Width = new GridLength(70) },
            new ColumnDefinition { Width = new GridLength(70) }
        },
        ColumnSpacing = 12,
        Padding = new Thickness(16, 12)
    };

    private static View BuildHeaderRow()
    {
        Grid header = MakeRowGrid();

        header.Add(Controls.MakeCell(string.Empty, 14, TextAlignment.Center), 0);
        header.Add(Controls.MakeCell("Exercise", 14, TextAlignment.Start), 1);
        header.Add(Controls.MakeCell("1RM", 14, TextAlignment.End), 2);
        header.Add(Controls.MakeCell("Best", 14, TextAlignment.End), 3);

        return header;
    }

    private static View BuildTrophyCard(RecordPasser record)
    {
        Grid inner = MakeRowGrid();
        
        inner.Add(Controls.MakeCell("🏆", 26, TextAlignment.Center), 0);
        inner.Add(Controls.MakeCell(Exercise.GetExerciseName(record.ExerciseId), 14, TextAlignment.Start), 1);
        inner.Add(Controls.MakeCell($"{record.OneRM}kg", 18, TextAlignment.End), 2);
        inner.Add(Controls.MakeCell($"{record.BestWeight}kg", 18, TextAlignment.End), 3);

        return new Border
        {
            BackgroundColor = Colors.LightGray,
            Content = inner
        };
    }

    private sealed class TrophyRow(string trophy, string exerciseName, int oneRM, int bestWeight)
    {
        public string Trophy { get; } = trophy;
        public string ExerciseName { get; } = exerciseName;
        public int OneRM { get; } = oneRM;
        public int BestWeight { get; } = bestWeight;
    }
}
