namespace MAUI_GymApp.Views;

public class WorkoutTab : ContentView
{
    public WorkoutTab()
    {
        Button startEmpty = Controls.MakeButton("Start Empty Workout", Colors.Black, Colors.White, Colors.Black, 2, 6, 12, new Thickness(8));
        Button createNew = Controls.MakeButton("Create New Workout", Colors.Black, Colors.White, Colors.Black, 2, 6, 12, new Thickness(8));
        Grid.SetColumn(createNew, 1);
        
        createNew.Clicked += OnCreateClick;

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
    
    private async void OnCreateClick(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new CreateWorkoutPage());
    }

    private static View BuildRoutineCard()
    {
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

        inner.Add(Controls.MakeLabel("Routine 1", 24), 0);
        inner.Add(Controls.MakeButton("✎", Colors.Black, Colors.White, Colors.White, 2, 6, 22, 48, 48, 0), 1);
        inner.Add(Controls.MakeButton("▶", Colors.Black, Colors.White, Colors.White, 2, 6, 22, 48, 48, 0), 3);

        return new Border
        {
            BackgroundColor = Colors.LightGray,
            Content = inner
        };
    }
}
