namespace MAUI_GymApp;

public class App : Application
{
    protected override Window CreateWindow(IActivationState? activationState)
    {
        NavigationPage navPage = new NavigationPage(new LoginPage())
        {
            BarBackgroundColor = Colors.White,
            BarTextColor = Colors.Black,
        };

        return new Window(navPage);
    }
}