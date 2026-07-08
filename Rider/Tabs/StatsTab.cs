namespace MAUI_GymApp.Views;

public class StatsTab : ContentView
{
    public StatsTab()
    {
        Content = new VerticalStackLayout
        {
            Padding = new Thickness(16, 16, 16, 0),
            Spacing = 0,
            Children =
            {
                new HorizontalStackLayout() { Children = { Controls.MakeLabel("Name: ", 22), Controls.MakeLabel(MainPage.User.Name, 22), } },
                new HorizontalStackLayout() { Children = { Controls.MakeLabel("Age: ", 22), Controls.MakeLabel(MainPage.User.Age.ToString(), 22), } },
                new HorizontalStackLayout() { Children = { Controls.MakeLabel("Weight: ", 22), Controls.MakeLabel(MainPage.User.Weight.ToString(), 22), } },
                new HorizontalStackLayout() { Children = { Controls.MakeLabel("Sex: ", 22), Controls.MakeLabel(MainPage.User.Sex, 22), } }
            }
        };
    }
}
