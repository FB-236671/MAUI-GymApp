using MAUI_GymApp.Database.Passers;
using MAUI_GymApp.Rider.Database.Queries;

namespace MAUI_GymApp;

public class CreateWorkoutPage : ContentPage
{
    public CreateWorkoutPage()
    {
        Background = Colors.White;
        Content = new VerticalStackLayout
        {
            Padding = new Thickness(16, 16, 16, 0),
            Spacing = 16,
            Children =
            {
                new HorizontalStackLayout()
                {
                    Spacing = 8,
                    Children =
                    {
                        Controls.MakeLabel("Workout Name: ", 30, Colors.Black, LayoutOptions.Center),
                        new VerticalStackLayout()
                        {
                            Spacing = 0,
                            Children =
                            {
                                Controls.MakeTextBox("Name", Colors.Black, 30, 500, LayoutOptions.Center),
                                new BoxView { HeightRequest = 2, Color = Colors.Black }
                            }
                        }
                    }
                },
                new BoxView { HeightRequest = 2, Color = Colors.Black },
                new Picker {Title = "Add an Exercise", ItemsSource = ExerciseList(), TextColor = Colors.Black, FontFamily = "OrbitronBold", TitleColor = Colors.Black},
                BuildWorkoutCard(),
            }
        };
    }

    private static List<string> ExerciseList()
    {
        List<string> exerciseList = new List<string>();
        List<ExercisePasser> passerList = ExerciseQueries.GetExercises();
        
        foreach (ExercisePasser passer in passerList)
        {
            exerciseList.Add(passer.Name);
        }
        return exerciseList;
    }

    // private static void OnPickerSelectedIndexChanged(object sender, EventArgs e)
    // {
    //     Picker picker = (Picker)sender;
    //     int selectedIndex = picker.SelectedIndex;
    //     
    //     if ()
    // }

    private static View BuildWorkoutCard()
    {
        Label routineLabel = Controls.MakeLabel("Exercise 1", 24, Colors.Black, LayoutOptions.Center);

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

        inner.Add(routineLabel, 0);

        return new Border
        {
            BackgroundColor = Colors.LightGray,
            Content = inner
        };
    }
}