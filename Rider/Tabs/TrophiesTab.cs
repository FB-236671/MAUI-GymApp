using Microsoft.Maui.Controls.Shapes;

namespace MAUI_GymApp.Views;

// Second screen in the sketch: search bar with Filter / Sort on the right,
// then a table of Trophy / Name / 1RM.
public class TrophiesTab : ContentView
{
    public TrophiesTab()
    {
        Content = new VerticalStackLayout
        {
            Padding = new Thickness(16, 16, 16, 0),
            Spacing = 0,
            Children =
            {
                BuildSearchRow(),
                BuildTable()
            }
        };
    }

    private static View BuildSearchRow()
    {
        Border searchBar = new Border
        {
            StrokeShape = new RoundRectangle { CornerRadius = 24 },
            Stroke = Colors.Black,
            StrokeThickness = 2,
            BackgroundColor = Colors.White,
            Padding = new Thickness(16, 0),
            VerticalOptions = LayoutOptions.Center,
            Content = new HorizontalStackLayout
            {
                Spacing = 8,
                VerticalOptions = LayoutOptions.Center,
                Children =
                {
                    new Label { Text = "🔍", FontSize = 16, VerticalOptions = LayoutOptions.Center },
                    new Entry
                    {
                        Placeholder = "Search",
                        BackgroundColor = Colors.Transparent,
                        TextColor = Colors.Black,
                        WidthRequest = 180,
                        VerticalOptions = LayoutOptions.Center
                    }
                }
            }
        };

        var filterButton = MakeSmallButton("Filter");
        var sortButton = MakeSmallButton("Sort");
        Grid.SetRow(sortButton, 1);

        var rightColumn = new Grid
        {
            RowDefinitions =
            {
                new RowDefinition { Height = GridLength.Star },
                new RowDefinition { Height = GridLength.Star }
            },
            Children = { filterButton, sortButton }
        };
        Grid.SetColumn(rightColumn, 1);

        var grid = new Grid
        {
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = new GridLength(110) }
            },
            ColumnSpacing = 12,
            HeightRequest = 100,
            Children = { searchBar }
        };
        grid.Add(rightColumn, 1);

        return grid;
    }

    private static Button MakeSmallButton(string text) => new()
    {
        Text = text,
        TextColor = Colors.Black,
        BackgroundColor = Colors.White,
        BorderColor = Colors.Black,
        BorderWidth = 2,
        CornerRadius = 0,
        FontSize = 16,
        Margin = new Thickness(0, 2)
    };

    private static View BuildTable()
    {
        Grid table = new Grid
        {
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Star }
            },
            RowDefinitions =
            {
                new RowDefinition { Height = GridLength.Auto },
                new RowDefinition { Height = GridLength.Auto }
            }
        };

        // Header row
        table.Add(Cell("Trophy", bold: true), 0, 0);
        table.Add(Cell("Name", bold: true), 1, 0);
        table.Add(Cell("1RM", bold: true), 2, 0);

        // Data row
        table.Add(Cell("Gold"), 0, 1);
        table.Add(Cell("Bench Press"), 1, 1);
        table.Add(Cell("100 kg"), 2, 1);

        return new Border
        {
            Stroke = Colors.Black,
            StrokeThickness = 2,
            BackgroundColor = Colors.White,
            Margin = new Thickness(0, 8, 0, 0),
            Content = table
        };
    }

    private static View Cell(string text, bool bold = false) => new Border
    {
        Stroke = Colors.Black,
        StrokeThickness = 1,
        BackgroundColor = Colors.White,
        Padding = new Thickness(10, 14),
        Content = new Label
        {
            Text = text,
            FontSize = 18,
            TextColor = Colors.Black,
            FontAttributes = bold ? FontAttributes.Bold : FontAttributes.None,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        }
    };
}
