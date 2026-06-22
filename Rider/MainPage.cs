using MAUI_GymApp.Views;

namespace MAUI_GymApp;

public class MainPage : ContentPage
{
    private readonly ContentView _contentHost;
    private readonly List<TabButton> _tabs = new();

    public MainPage()
    {
        BackgroundColor = Colors.White;

        // The view shown above the tab bar; swapped when a tab is tapped.
        _contentHost = new ContentView();

        TabButton workoutTab = new TabButton("🏋", "Workout", () => Show(0, new WorkoutTab()));
        TabButton trophiesTab = new TabButton("🏆", "Trophies", () => Show(1, new TrophiesTab()));
        TabButton statsTab = new TabButton("📊", "Stats", () => Show(2, new StatsTab()));
        _tabs.Add(workoutTab);
        _tabs.Add(trophiesTab);
        _tabs.Add(statsTab);

        Grid tabBarGrid = new Grid
        {
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Star }
            },
            HeightRequest = 72,
            BackgroundColor = Colors.White
        };
        tabBarGrid.Add(workoutTab, 0);
        tabBarGrid.Add(trophiesTab, 1);
        tabBarGrid.Add(statsTab, 2);

        // Thin divider line above the tab bar, as in the sketch.
        VerticalStackLayout tabBar = new VerticalStackLayout
        {
            Spacing = 0,
            Children =
            {
                new BoxView { HeightRequest = 2, Color = Colors.Black },
                tabBarGrid
            }
        };

        Grid root = new Grid
        {
            RowDefinitions =
            {
                new RowDefinition { Height = GridLength.Star },
                new RowDefinition { Height = GridLength.Auto }
            }
        };
        root.Add(_contentHost, 0);
        root.Add(tabBar, 0, 1);

        Content = root;

        // Start on the Workout tab.
        Show(0, new WorkoutTab());
    }

    private void Show(int index, View view)
    {
        _contentHost.Content = view;
        for (var i = 0; i < _tabs.Count; i++)
            _tabs[i].SetSelected(i == index);
    }
}

// A single bottom-bar tab: an icon over a label. The selected tab is rendered
// bold and black; unselected tabs are a lighter grey.
public class TabButton : VerticalStackLayout
{
    private static readonly Color SelectedColor = Colors.Black;
    private static readonly Color UnselectedColor = Color.FromArgb("#9E9E9E");

    private readonly Label _icon;
    private readonly Label _text;

    public TabButton(string icon, string text, Action onTapped)
    {
        Spacing = 2;
        Padding = new Thickness(0, 8);
        HorizontalOptions = LayoutOptions.Fill;
        VerticalOptions = LayoutOptions.Fill;
        BackgroundColor = Colors.White;

        _icon = new Label
        {
            Text = icon,
            FontSize = 24,
            HorizontalOptions = LayoutOptions.Center
        };

        _text = new Label
        {
            Text = text,
            FontSize = 14,
            HorizontalOptions = LayoutOptions.Center
        };

        Children.Add(_icon);
        Children.Add(_text);

        var tap = new TapGestureRecognizer();
        tap.Tapped += (_, _) => onTapped();
        GestureRecognizers.Add(tap);
    }

    public void SetSelected(bool selected)
    {
        var color = selected ? SelectedColor : UnselectedColor;
        var attributes = selected ? FontAttributes.Bold : FontAttributes.None;

        _icon.TextColor = color;
        _icon.FontAttributes = attributes;
        _text.TextColor = color;
        _text.FontAttributes = attributes;
    }
}
