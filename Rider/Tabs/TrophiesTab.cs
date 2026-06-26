using Microsoft.Maui.Controls.Shapes;

namespace MAUI_GymApp.Views;

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
                        FontFamily = "OrbitronBold",
                        BackgroundColor = Colors.Transparent,
                        TextColor = Colors.Black,
                        WidthRequest = 180,
                        VerticalOptions = LayoutOptions.Center
                    }
                }
            }
        };
        
        Button filterButton = Controls.MakeButton("Filter", Colors.Black, Colors.White, Colors.Black, 2, 0, 16, new Thickness(0, 2));
        Button sortButton = Controls.MakeButton("Sort", Colors.Black, Colors.White, Colors.Black, 2, 0, 16, new Thickness(0, 2));
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
        
        table.Add(Cell("Trophy", bold: true), 0, 0);
        table.Add(Cell("Name", bold: true), 1, 0);
        table.Add(Cell("1RM", bold: true), 2, 0);
        
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
            FontFamily = "OrbitronBold",
            TextColor = Colors.Black,
            FontAttributes = bold ? FontAttributes.Bold : FontAttributes.None,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        }
    };
}