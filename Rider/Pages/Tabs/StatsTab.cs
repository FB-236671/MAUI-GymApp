using MAUI_GymApp.Rider.Database.Objects;
using Microcharts;
using SkiaSharp;

namespace MAUI_GymApp.Views;

public class StatsTab : ContentView
{
    private readonly VerticalStackLayout _layout;
    private UserRepository _user;

    public StatsTab()
    {
        Button editButton = UIElements.MakeButton("Edit", Colors.Black, Colors.White, Colors.Black, 2, 6, 20, 4);
        Button logoutButton = UIElements.MakeButton("Logout", Colors.Black, Colors.White, Colors.Black, 2, 6, 20, 4);
        Button deleteButton = UIElements.MakeButton("Delete", Colors.Black, Colors.White, Colors.Black, 2, 6, 20, 4);
        _user = MainPage.User;

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
                        UIElements.MakeLabel(_user.Name, 30), editButton, logoutButton, deleteButton
                    }
                },
                new HorizontalStackLayout()
                {
                    Spacing = 80,
                    Children =
                    {
                        UIElements.MakeLabel("Age: " + _user.Age, 10),
                        UIElements.MakeLabel("Weight: " + _user.Weight, 10),
                        UIElements.MakeLabel("Sex: " + _user.Sex, 10),
                    }
                }
            }
        };
        
        foreach (View chart in ChartStats.All(_user.UserId))
        {
            _layout.Children.Add(chart);
        }

        editButton.Clicked += OnEditButtonClick;
        logoutButton.Clicked += OnLogoutButtonClick;
        deleteButton.Clicked += OnDeleteButtonClick;
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

    private void OnDeleteButtonClick(object? sender, EventArgs e)
    {
        User.DeleteUser(_user.UserId);
        GeneralControls.GoToLogin();
    }
}
