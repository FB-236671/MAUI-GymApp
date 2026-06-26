namespace MAUI_GymApp.Views;

public class WorkoutTab : ContentView
{
    public WorkoutTab()
    {
        Button startEmpty = Controls.MakeButton("Start Empty Workout", Colors.Black, Colors.White, Colors.Black, 2, 6, 18, new Thickness(8));
        Button createNew = Controls.MakeButton("Create New Workout", Colors.Black, Colors.White, Colors.Black, 2, 6, 18, new Thickness(8));
        Grid.SetColumn(createNew, 1);

        Grid topRow = new Grid
        {
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Star }
            },
            HeightRequest = 70,
            Children = { startEmpty, createNew }
        };

        View routineCard = BuildRoutineCard();

        Content = new VerticalStackLayout
        {
            Padding = new Thickness(16, 16, 16, 0),
            Spacing = 16,
            Children = { topRow, routineCard }
        };
    }

    private static View BuildRoutineCard()
    {
        Label routineLabel = Controls.MakeLabel("Routine 1", 24, Colors.Black, LayoutOptions.Center);

        Button deleteButton = Controls.MakeButton("🗑", Colors.Black, Colors.White, Colors.White, 2, 6, 22, 48, 48, 0);
        Button editButton = Controls.MakeButton("✎", Colors.Black, Colors.White, Colors.White, 2, 6, 22, 48, 48, 0);
        Button playButton = Controls.MakeButton("▶", Colors.Black, Colors.White, Colors.White, 2, 6, 22, 48, 48, 0);

        Grid inner = new Grid
        {
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Auto },
                new ColumnDefinition { Width = GridLength.Auto },
                new ColumnDefinition { Width = GridLength.Auto }
            },
            ColumnSpacing = 12,
            Padding = new Thickness(20, 14)
        };

        inner.Add(routineLabel, 0);
        inner.Add(editButton, 1);
        inner.Add(deleteButton, 2);
        inner.Add(playButton, 3);

        return new Border
        {
            BackgroundColor = Colors.LightGray,
            Content = inner
        };
    }
}
