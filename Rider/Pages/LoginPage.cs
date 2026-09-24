using MAUI_GymApp.Rider.Database.Objects;

namespace MAUI_GymApp;

public class LoginPage : ContentPage
{
    public LoginPage()
    {
        BackgroundColor = Colors.White;
        Button createProfileButton = UIElementControls.MakeButton("Create New Profile", Colors.Black, Colors.White, Colors.Black, 2, 6, 18, 0);
        
        VerticalStackLayout layout = new VerticalStackLayout
        {
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
        };
        
        List<UserPasser> userList = User.GetUsers();
        foreach (UserPasser userObject in userList)
        {
            Button button = UIElementControls.MakeButton(userObject.Name, Colors.Black, Colors.White, Colors.Black, 2, 6, 18, 0);
            button.Clicked += (sender, e) => OnLoginClick(userObject);
            layout.Add(button);
        }
        
        createProfileButton.Clicked += OnCreateProfileClick;
        layout.Add(createProfileButton);
        Content = layout;
    }
    
    private void OnLoginClick(UserPasser userObject)
    {
        MainPage.User = userObject;
        GeneralControls.GoHome();
    }

    private async void OnCreateProfileClick(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new CreateProfilePage());
    }
}