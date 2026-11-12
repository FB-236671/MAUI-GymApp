using MAUI_GymApp.Rider.Database.Objects;

namespace MAUI_GymApp;

public class ProfilePage : ContentPage
{
    private Entry _nameEntry;
    private Entry _ageEntry;
    private Entry _weightEntry;
    private Picker _sexPicker;

    public ProfilePage(UserParser? user = null)
    {
        BackgroundColor = Colors.White;
        Button createButton = UIElements.MakeButton("Create Profile", Colors.Black, Colors.White, Colors.Black, 2, 6, 18, 4);
        Label nameLabel = UIElements.MakeLabel("Name:", 18, Colors.Black, LayoutOptions.Center);
        Label ageLabel = UIElements.MakeLabel("Age:", 18, Colors.Black, LayoutOptions.Center);
        Label weightLabel = UIElements.MakeLabel("Weight:", 18, Colors.Black, LayoutOptions.Center);
        Label kilogramsLabel = UIElements.MakeLabel("kg", 18, Colors.Black, LayoutOptions.Center);
        Label emptyLabel = UIElements.MakeLabel("  ", 18, Colors.Black, LayoutOptions.Center);
        Label sexLabel = UIElements.MakeLabel("Sex:", 18, Colors.Black, LayoutOptions.Center);

        _nameEntry = UIElements.MakeTextBox("Name", Colors.Gray, Colors.Black, 18, 180, LayoutOptions.Center);
        _ageEntry = UIElements.MakeIntBox("Age", Colors.Gray, Colors.Black, 18, 180, LayoutOptions.Center);
        _weightEntry = UIElements.MakeIntBox("Weight", Colors.Gray, Colors.Black, 18, 180, LayoutOptions.Center);
        _sexPicker = UIElements.MakeSelector("Sex", new List<string> { "Male", "Female" }, 18);
        _sexPicker.WidthRequest = 180;

        if (user != null)
        {
            _nameEntry.Text = user.Name;
            _ageEntry.Text = Convert.ToString(user.Age);
            _weightEntry.Text = Convert.ToString(user.Weight);
            if (user.Sex == "male")
            {
                _sexPicker.SelectedIndex = 0;
            }
            else
            {
                _sexPicker.SelectedIndex = 1;
            }
        }
        
        VerticalStackLayout layout = new VerticalStackLayout
        {
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
        };

        createButton.Clicked += OnLoginClick;
        
        layout.Add(BuildProfileCard(nameLabel, _nameEntry));
        layout.Add(BuildProfileCard(ageLabel, _ageEntry));
        layout.Add(BuildProfileCard(weightLabel, _weightEntry, kilogramsLabel));
        layout.Add(BuildProfileCard(sexLabel, _sexPicker));
        layout.Add(createButton);

        Content = layout;
    }
    
    private async void OnLoginClick(object? sender, EventArgs e)
    {
        var errors = new List<string>();

        if (TypeChecker.StringCheck(_nameEntry.Text, "Name") is string nameError) { errors.Add(nameError); }
        if (TypeChecker.IntegerCheck(_ageEntry.Text, "Age", 1, 120, out int age) is string ageError) { errors.Add(ageError); }
        if (TypeChecker.IntegerCheck(_weightEntry.Text, "Weight", 20, 700, out int weight) is string weightError) { errors.Add(weightError); }
        if (_sexPicker.SelectedItem == null) { errors.Add("Please select an option for Sex"); }

        if (errors.Count > 0)
        {
            await DisplayAlert("Input Error", string.Join("\n", errors), "Close"); return;
        }

        UserParser userObject = new UserParser
        {
            Name = _nameEntry.Text.Trim(),
            Age = age,
            Weight = weight,
            Sex = (string)_sexPicker.SelectedItem,
        };
        userObject.UserId = User.CreateUser(userObject);
        MainPage.User = userObject;
        
        GeneralControls.GoHome();
    }
    
    private static View BuildProfileCard(Label label, View entry, Label? extraLabel = null)
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
                new ColumnDefinition { Width = 80 },
                new ColumnDefinition { Width = GridLength.Auto },
                new ColumnDefinition { Width = 30 }
            },
            ColumnSpacing = 12,
            Padding = new Thickness(20, 14)
        };

        inner.Add(label, 0);
        inner.Add(textBox, 1);
        if (extraLabel != null)
        {
            inner.Add(extraLabel, 2);
        }

        return new Border
        {
            BackgroundColor = Colors.LightGray,
            Content = inner
        };
    }
}