namespace MAUI_GymApp.Views;

// First screen in the sketch: "Start Empty Workout" / "Create New Workout"
// boxes on top, and a rounded "Routine" card with edit + play buttons.
public class WorkoutTab : ContentView
{
    public WorkoutTab()
    {
        Button startEmpty = MakeBoxButton("Start Empty Workout");
        Button createNew = MakeBoxButton("Create New Workout");
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

    private static Button MakeBoxButton(string text) => new()
    {
        Text = text,
        TextColor = Colors.Black,
        BackgroundColor = Colors.White,
        BorderColor = Colors.Black,
        BorderWidth = 2,
        CornerRadius = 6,
        FontSize = 18,
        Padding = new Thickness(8)
    };

    private static View BuildRoutineCard()
    {
        Label routineLabel = new Label
        {
            Text = "Routine 1",
            FontSize = 28,
            TextColor = Colors.Black,
            VerticalOptions = LayoutOptions.Center
        };

        Button deleteButton = new Button()
        {
            Text = "🗑",
            FontSize = 22,
            TextColor = Colors.Black,
            BackgroundColor = Colors.Red,
            BorderColor = Colors.Black,
            BorderWidth = 2,
            CornerRadius = 6,
            WidthRequest = 48,
            HeightRequest = 48,
            Padding = 0
        };

        Button editButton = new Button
        {
            Text = "✎",
            FontSize = 22,
            TextColor = Colors.Black,
            BackgroundColor = Colors.White,
            BorderColor = Colors.Black,
            BorderWidth = 2,
            CornerRadius = 6,
            WidthRequest = 48,
            HeightRequest = 48,
            Padding = 0
        };

        Button playButton = new Button
        {
            Text = "▶", // play triangle
            FontSize = 22,
            TextColor = Colors.Black,
            BackgroundColor = Colors.Green,
            BorderColor = Colors.Black,
            BorderWidth = 2,
            CornerRadius = 6,
            WidthRequest = 48,
            HeightRequest = 48,
            Padding = 0
        };

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
        inner.Add(playButton, 2);
        inner.Add(deleteButton, 3);

        return new Border
        {
            StrokeShape = new Microsoft.Maui.Controls.Shapes.RoundRectangle { CornerRadius = 28 },
            Stroke = Colors.Black,
            StrokeThickness = 2,
            BackgroundColor = Colors.White,
            Content = inner
        };
    }
}
