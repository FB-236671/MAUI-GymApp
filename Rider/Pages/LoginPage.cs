using MAUI_GymApp.Rider.Database.Objects;

namespace MAUI_GymApp;

public class LoginPage : ContentPage
{
    public LoginPage()
    {
        BackgroundColor = Colors.White;
        Button createProfileButton = UIElements.MakeButton("Create New Profile", Colors.Black, Colors.White, Colors.Black, 2, 6, 18, 4);
        
        VerticalStackLayout layout = new VerticalStackLayout
        {
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
        };
        
        List<UserRepository> userList = User.GetUsers();
        foreach (UserRepository userObject in userList)
        {
            Button button = UIElements.MakeButton(userObject.Name, Colors.Black, Colors.White, Colors.Black, 2, 6, 18, 4);
            button.Clicked += (sender, e) => OnLoginClick(userObject);
            layout.Add(button);
        }
        
        createProfileButton.Clicked += OnCreateProfileClick;
        layout.Add(createProfileButton);
        Content = layout;
    }
    
    private void OnLoginClick(UserRepository userObject)
    {
        MainPage.User = userObject;
        GeneralControls.GoHome();
    }

    private async void OnCreateProfileClick(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProfilePage());
    }
}