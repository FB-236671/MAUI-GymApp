using MAUI_GymApp.Rider.Database.Objects;
using Microcharts;
using SkiaSharp;

namespace MAUI_GymApp.Views;

public class StatsTab : ContentView
{
    private readonly VerticalStackLayout _layout;

    public StatsTab()
    {
        Button editButton = UIElements.MakeButton("Edit", Colors.Black, Colors.White, Colors.Black, 2, 6, 20, 4);
        Button logoutButton = UIElements.MakeButton("Logout", Colors.Black, Colors.White, Colors.Black, 2, 6, 20, 4);
        UserParser user = MainPage.User;

        _layout = new VerticalStackLayout()
        {
            Padding = new Thickness(16, 16, 16, 0),
            Spacing = 16,
            Children =
            {
                new HorizontalStackLayout()
                {
                    Spacing = 40,
                    Children =
                    {
                        UIElements.MakeLabel(user.Name, 30), editButton, logoutButton
                    }
                },
                new HorizontalStackLayout()
                {
                    Spacing = 80,
                    Children =
                    {
                        UIElements.MakeLabel("Age: " + user.Age, 10),
                        UIElements.MakeLabel("Weight: " + user.Weight, 10),
                        UIElements.MakeLabel("Sex: " + user.Sex, 10),
                    }
                }
            }
        };
        
        foreach (View chart in ChartStats.All(user.UserId))
        {
            _layout.Children.Add(chart);
        }

        editButton.Clicked += OnEditButtonClick;
        logoutButton.Clicked += OnLogoutButtonClick;
        Content = new ScrollView { Content = _layout };
    }

    private async void OnEditButtonClick(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProfilePage(MainPage.User));
    }

    private void OnLogoutButtonClick(object? sender, EventArgs e)
    {
        GeneralControls.GoToLogin();
    }
}
