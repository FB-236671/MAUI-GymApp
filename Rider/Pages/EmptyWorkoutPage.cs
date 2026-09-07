using MAUI_GymApp.Rider.Database.Objects;

namespace MAUI_GymApp;

public class EmptyWorkoutPage : ContentPage
{
    private readonly VerticalStackLayout _layout;
    private Label _timerLabel = Controls.MakeLabel("__:__:__", 30);
    
    public EmptyWorkoutPage()
    {
        Background = Colors.White;
        Button saveButton = Controls.MakeButton("Save", Colors.Black, Colors.White, Colors.Black, 2, 6, 20, new Thickness());
        Button finishButton = Controls.MakeButton("Finish", Colors.Black, Colors.White, Colors.Black, 2, 6, 20, new Thickness());
        Picker picker = Controls.MakeSelector("Add an Exercise", ExerciseList(), 18);

        Grid buttons = new Grid
        {
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Star }
            },
            HeightRequest = 40,
            ColumnSpacing = 0
        };
        buttons.Add(saveButton, 0);
        buttons.Add(finishButton, 1);
        
        _layout = new VerticalStackLayout()
        {
            Padding = new Thickness(16, 16, 16, 0),
            Spacing = 16,
            Children =
            {
                new HorizontalStackLayout()
                {
                    Spacing = 30,
                    Children =
                    {
                        Controls.MakeLabel("New Routine", 25),
                        _timerLabel
                    }
                },
                new BoxView { HeightRequest = 2, Color = Colors.Black },
                buttons, picker
            }
        };
        
        picker.SelectedIndexChanged += OnPickerSelectedIndexChanged;
        Content = new ScrollView { Content = _layout };
    }
    
    private static List<string> ExerciseList()
    {
        List<string> exerciseList = new List<string>();
        List<ExercisePasser> passerList = Exercise.GetExercises();
        
        foreach (ExercisePasser passer in passerList)
        {
            exerciseList.Add(passer.Name);
        }
        return exerciseList;
    }
    
    private async void OnPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        Picker picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;
        
        RoutineExercisesPasser exercise = new RoutineExercisesPasser();
        exercise.ExerciseId = Exercise.GetExerciseByName((string)picker.ItemsSource[selectedIndex]).ExerciseId;
        
        string sets = await Application.Current!.MainPage!.DisplayPromptAsync(
            "Sets", "How many sets for this exercise", "Confirm", "Back");
        exercise.Sets = Convert.ToInt32(sets);

        // if (selectedIndex != -1)
        // {
        //     _layout.Children.Add(BuildWorkoutCard((string)picker.ItemsSource[selectedIndex]));
        // }
    }
}