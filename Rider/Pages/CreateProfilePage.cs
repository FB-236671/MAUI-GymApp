using MAUI_GymApp.Rider.Database.Objects;

namespace MAUI_GymApp;

public class CreateProfilePage : ContentPage
{
    private Entry _nameEntry;
    private Entry _ageEntry;
    private Entry _weightEntry;
    private Entry _sexEntry;

    public CreateProfilePage()
    {
        BackgroundColor = Colors.White;
        Button createButton = UIElementControls.MakeButton("Create Profile", Colors.Black, Colors.White, Colors.Black, 2, 6, 18, 0);
        Label nameLabel = UIElementControls.MakeLabel("Name:", 18, Colors.Black, LayoutOptions.Center);
        Label ageLabel = UIElementControls.MakeLabel("Age:", 18, Colors.Black, LayoutOptions.Center);
        Label weightLabel = UIElementControls.MakeLabel("Weight:", 18, Colors.Black, LayoutOptions.Center);
        Label sexLabel = UIElementControls.MakeLabel("Sex:", 18, Colors.Black, LayoutOptions.Center);

        _nameEntry = UIElementControls.MakeTextBox("Name", Colors.Gray, Colors.Black, 18, 180, LayoutOptions.Center);
        _ageEntry = UIElementControls.MakeTextBox("Age", Colors.Gray, Colors.Black, 18, 180, LayoutOptions.Center);
        _weightEntry = UIElementControls.MakeTextBox("Weight", Colors.Gray, Colors.Black, 18, 180, LayoutOptions.Center);
        _sexEntry = UIElementControls.MakeTextBox("Sex", Colors.Gray, Colors.Black, 18, 180, LayoutOptions.Center);
        
        VerticalStackLayout layout = new VerticalStackLayout
        {
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
        };

        createButton.Clicked += OnLoginClick;
        
        layout.Add(BuildProfileCard(nameLabel, _nameEntry));
        layout.Add(BuildProfileCard(ageLabel, _ageEntry));
        layout.Add(BuildProfileCard(weightLabel, _weightEntry));
        layout.Add(BuildProfileCard(sexLabel, _sexEntry));
        layout.Add(createButton);

        Content = layout;
    }
    
    private void OnLoginClick(object? sender, EventArgs e)
    {
        UserPasser userObject = new UserPasser();
        userObject.Name = _nameEntry.Text;
        userObject.Age = Convert.ToInt32(_ageEntry.Text);
        userObject.Weight = Convert.ToInt32(_weightEntry.Text);
        userObject.Sex = _sexEntry.Text;
        userObject.UserId = User.CreateUser(userObject);
        MainPage.User = userObject;
        
        Application.Current!.MainPage = new NavigationPage(new MainPage())
        {
            BarBackgroundColor = Colors.White,
            BarTextColor = Colors.Black,
        };
    }
    
    private static View BuildProfileCard(Label label, Entry entry)
    {
        Border textBox = new Border
        {
            BackgroundColor = Colors.White,
            Padding = new Thickness(16, 0),
            VerticalOptions = LayoutOptions.Center,
            Content = new HorizontalStackLayout
            {
                Spacing = 8,
                VerticalOptions = LayoutOptions.Center,
                Children = { entry }
            }
        };

        Grid inner = new Grid
        {
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Auto },
                new ColumnDefinition { Width = GridLength.Auto },
                new ColumnDefinition { Width = GridLength.Auto }
            },
            ColumnSpacing = 12,
            Padding = new Thickness(20, 14)
        };

        inner.Add(label, 0);
        inner.Add(textBox, 1);

        return new Border
        {
            BackgroundColor = Colors.LightGray,
            Content = inner
        };
    }
}