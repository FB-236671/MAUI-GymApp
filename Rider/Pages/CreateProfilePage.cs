using MAUI_GymApp.Rider.Database.Objects;

namespace MAUI_GymApp;

public class CreateProfilePage : ContentPage
{
    private Entry _nameEntry;
    private Entry _ageEntry;
    private Entry _weightEntry;
    private Picker _sexPicker;

    public CreateProfilePage()
    {
        BackgroundColor = Colors.White;
        Button createButton = UIElements.MakeButton("Create Profile", Colors.Black, Colors.White, Colors.Black, 2, 6, 18, 0);
        Label nameLabel = UIElements.MakeLabel("Name:", 18, Colors.Black, LayoutOptions.Center);
        Label ageLabel = UIElements.MakeLabel("Age:", 18, Colors.Black, LayoutOptions.Center);
        Label weightLabel = UIElements.MakeLabel("Weight:", 18, Colors.Black, LayoutOptions.Center);
        Label sexLabel = UIElements.MakeLabel("Sex:", 18, Colors.Black, LayoutOptions.Center);

        _nameEntry = UIElements.MakeTextBox("Name", Colors.Gray, Colors.Black, 18, 180, LayoutOptions.Center);
        _ageEntry = UIElements.MakeIntBox("Age", Colors.Gray, Colors.Black, 18, 180, LayoutOptions.Center);
        _weightEntry = UIElements.MakeIntBox("Weight", Colors.Gray, Colors.Black, 18, 180, LayoutOptions.Center);
        _sexPicker = UIElements.MakeSelector("Sex", new List<string> { "Male", "Female" }, 18);
        _sexPicker.WidthRequest = 180;
        
        VerticalStackLayout layout = new VerticalStackLayout
        {
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
        };

        createButton.Clicked += OnLoginClick;
        
        layout.Add(BuildProfileCard(nameLabel, _nameEntry));
        layout.Add(BuildProfileCard(ageLabel, _ageEntry));
        layout.Add(BuildProfileCard(weightLabel, _weightEntry));
        layout.Add(BuildProfileCard(sexLabel, _sexPicker));
        layout.Add(createButton);

        Content = layout;
    }
    
    private async void OnLoginClick(object? sender, EventArgs e)
    {
        var errors = new List<string>();

        if (TypeChecker.StringCheck(_nameEntry.Text, "Name") is string nameError) { errors.Add(nameError); }
        if (TypeChecker.IntegerCheck(_ageEntry.Text, "Age", out int age) is string ageError) { errors.Add(ageError); }
        if (TypeChecker.IntegerCheck(_weightEntry.Text, "Weight", out int weight) is string weightError) { errors.Add(weightError); }
        if (_sexPicker.SelectedItem == null) { errors.Add("Please select an option for Sex"); }

        if (errors.Count > 0)
        {
            await DisplayAlertAsync("Input Error", string.Join("\n", errors), "Close");
            return;
        }

        UserPasser userObject = new UserPasser
        {
            Name = _nameEntry.Text.Trim(),
            Age = age,
            Weight = weight,
            Sex = (string)_sexPicker.SelectedItem,
        };
        userObject.UserId = User.CreateUser(userObject);
        MainPage.User = userObject;
        
        Application.Current!.MainPage = new NavigationPage(new MainPage())
        {
            BarBackgroundColor = Colors.White,
            BarTextColor = Colors.Black,
        };
    }
    
    private static View BuildProfileCard(Label label, View entry)
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