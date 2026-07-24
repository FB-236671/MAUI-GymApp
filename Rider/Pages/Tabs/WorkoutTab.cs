using MAUI_GymApp.Rider.Database.Objects;

namespace MAUI_GymApp.Views;

public class WorkoutTab : ContentView
{
    private readonly VerticalStackLayout _layout;
    private bool _routinesLoaded;

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

        _layout = new VerticalStackLayout
        {
            Padding = new Thickness(16, 16, 16, 0),
            Spacing = 16,
            Children = { topRow }
        };
        
        Content = new ScrollView { Content = _layout };
        Loaded += OnLoaded;
    }

    private async void OnStartClick(object? sender, EventArgs e)
    {
        if (sender is not Button button || button.CommandParameter is not RoutinePasser routineObject)
            return;
        
        await Navigation.PushAsync(new WorkoutPage(routineObject));
    }
    
    private void OnEditClick(object? sender, EventArgs e)
    {
        
    }
    
    private void OnDeleteClick(object? sender, EventArgs e)
    {
        if (sender is not Button button || button.CommandParameter is not int routineId)
            return;
        
        Routine.DeleteRoutine(routineId);

        if (button.Parent?.Parent is View card)
            _layout.Remove(card);
    }

    private void OnLoaded(object? sender, EventArgs e)
    {
        if (_routinesLoaded)
            return;
        _routinesLoaded = true;

        try
        {
            List<RoutinePasser> routineList = Routine.GetRoutinesByUser(MainPage.User.UserId);
            foreach (RoutinePasser routine in routineList)
                _layout.Add(BuildRoutineCard(routine));
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Failed to load routines: {ex}");
            _layout.Add(Controls.MakeLabel("Couldn't load routines.", 16));
        }
    }
    
    private async void OnCreateClick(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new CreateWorkoutPage());
    }

    private View BuildRoutineCard(RoutinePasser routineObject)
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

        Button start = Controls.MakeButton("▶", Colors.Black, Colors.White, Colors.White, 2, 6, 22, 48, 48, 0);
        Button edit = Controls.MakeButton("✎", Colors.Black, Colors.White, Colors.White, 2, 6, 22, 48, 48, 0);
        Button delete = Controls.MakeButton("🗑", Colors.Black, Colors.White, Colors.White, 2, 6, 22, 48, 48, 0);

        start.CommandParameter = routineObject;
        delete.CommandParameter = routineObject.RoutineId;
        start.Clicked += OnStartClick;
        delete.Clicked += OnDeleteClick;
        
        inner.Add(Controls.MakeLabel(routineObject.Name, 24), 0);
        inner.Add(start, 1);
        inner.Add(edit, 2);
        inner.Add(delete, 3);

        return new Border
        {
            BackgroundColor = Colors.LightGray,
            Content = inner
        };
    }
}
