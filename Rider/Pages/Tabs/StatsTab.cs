using MAUI_GymApp.Rider.Database.Objects;

namespace MAUI_GymApp.Views;

public class StatsTab : ContentView
{
    private readonly VerticalStackLayout _layout;
    private UserRepository _user;

    public StatsTab(UserRepository user)
    {
        Button editButton = UIElements.MakeButton("Edit", Colors.Black, Colors.White, Colors.Black, 2, 6, 20, 4);
        Button logoutButton = UIElements.MakeButton("Logout", Colors.Black, Colors.White, Colors.Black, 2, 6, 20, 4);
        Button deleteButton = UIElements.MakeButton("Delete", Colors.Black, Colors.White, Colors.Black, 2, 6, 20, 4);
        _user = user;

        _layout = new VerticalStackLayout()
        {
            Padding = new Thickness(16, 16, 16, 0),
            Spacing = 16,
            Children =
            {
                UIElements.MakeLabel("User Statistics", 30), UIElements.MakeLine(),
                UIElements.MakeLabel(_user.Name, 30),
                new HorizontalStackLayout()
                {
                    Spacing = 12,
                    Children = { editButton, logoutButton, deleteButton }
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
        
        foreach (View chart in ChartStats.StatsTab(_user.UserId))
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
        await Navigation.PushAsync(new ProfilePage(_user));
    }

    private void OnLogoutButtonClick(object? sender, EventArgs e)
    {
        GeneralControls.GoToLogin();
    }

    private async void OnDeleteButtonClick(object? sender, EventArgs e)
    {
        bool confirmed = await Application.Current!.Windows[0].Page!.DisplayAlert("Delete Profile", "Are you sure you want to delete this profile", "Confirm", "Cancel");
        
        if (!confirmed) return;
        
        User.DeleteUser(_user.UserId);
        GeneralControls.GoToLogin();
    }
}
