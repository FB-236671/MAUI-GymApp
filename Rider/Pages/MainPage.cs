using MAUI_GymApp.Database.Passers;
using MAUI_GymApp.Views;

namespace MAUI_GymApp;

public class MainPage : ContentPage
{
    private readonly ContentView _contentHost;
    private readonly List<TabButton> _tabs = new();

    public static UserPasser User;

    public MainPage()
    {
        BackgroundColor = Colors.White;
        NavigationPage.SetHasNavigationBar(this, false);
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

        Show(0, new WorkoutTab());
    }

    private void Show(int index, View view)
    {
        _contentHost.Content = view;
        for (var i = 0; i < _tabs.Count; i++)
            _tabs[i].SetSelected(i == index);
    }
}
