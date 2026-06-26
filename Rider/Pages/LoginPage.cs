namespace MAUI_GymApp;

public class LoginPage : ContentPage
{
    public LoginPage()
    {
        BackgroundColor = Colors.White;
        Button createProfileButton = Controls.MakeButton("Create New Profile", Colors.Black, Colors.White, Colors.Black, 2, 6, 18, 0);
        Button testButton = Controls.MakeButton("Finlay", Colors.Black, Colors.White, Colors.Black, 2, 6, 18, 0);

        VerticalStackLayout layout = new VerticalStackLayout
        {
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
        };

        testButton.Clicked += OnLoginClick;
        createProfileButton.Clicked += OnCreateProfileClick;

        layout.Add(createProfileButton);
        layout.Add(testButton);

        Content = layout;

    }
    
    private void OnLoginClick(object? sender, EventArgs e)
    {
        Application.Current!.MainPage = new MainPage();
    }

    private void OnCreateProfileClick(object? sender, EventArgs e)
    {
        Navigation.PushAsync(new CreateProfilePage());
    }
}