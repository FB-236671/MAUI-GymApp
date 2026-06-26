using MAUI_GymApp.Database.Passers;
using MAUI_GymApp.Rider.Database.Queries;
using Microsoft.Maui.Controls.Shapes;

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
        Button createButton = Controls.MakeButton("Create Profile", Colors.Black, Colors.White, Colors.Black, 2, 6, 18, 0);
        Label nameLabel = Controls.MakeLabel("Name:", 18, Colors.Black, LayoutOptions.Center);
        Label ageLabel = Controls.MakeLabel("Age:", 18, Colors.Black, LayoutOptions.Center);
        Label weightLabel = Controls.MakeLabel("Weight:", 18, Colors.Black, LayoutOptions.Center);
        Label sexLabel = Controls.MakeLabel("Sex:", 18, Colors.Black, LayoutOptions.Center);

        _nameEntry   = new Entry { Placeholder = "Text", FontFamily = "OrbitronBold", BackgroundColor = Colors.Transparent, TextColor = Colors.Black, WidthRequest = 140, VerticalOptions = LayoutOptions.Center };
        _ageEntry    = new Entry { Placeholder = "Text", FontFamily = "OrbitronBold", BackgroundColor = Colors.Transparent, TextColor = Colors.Black, WidthRequest = 140, VerticalOptions = LayoutOptions.Center };
        _weightEntry = new Entry { Placeholder = "Text", FontFamily = "OrbitronBold", BackgroundColor = Colors.Transparent, TextColor = Colors.Black, WidthRequest = 140, VerticalOptions = LayoutOptions.Center };
        _sexEntry    = new Entry { Placeholder = "Text", FontFamily = "OrbitronBold", BackgroundColor = Colors.Transparent, TextColor = Colors.Black, WidthRequest = 140, VerticalOptions = LayoutOptions.Center };

        View nameCard   = BuildProfileCard(nameLabel,   _nameEntry);
        View ageCard    = BuildProfileCard(ageLabel,    _ageEntry);
        View weightCard = BuildProfileCard(weightLabel, _weightEntry);
        View sexCard    = BuildProfileCard(sexLabel,    _sexEntry);

        VerticalStackLayout layout = new VerticalStackLayout
        {
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
        };

        createButton.Clicked += OnLoginClick;
        
        layout.Add(nameCard);
        layout.Add(ageCard);
        layout.Add(weightCard);
        layout.Add(sexCard);
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
        
        UserQueries.CreateUser(userObject);
        MainPage.User = userObject;
        
        Application.Current!.MainPage = new MainPage();
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