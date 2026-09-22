using MAUI_GymApp.Rider.Database.Objects;

namespace MAUI_GymApp;

public class EmptyWorkoutPage : ContentPage
{
    private readonly VerticalStackLayout _layout;
    private Label _timerLabel = Controls.MakeLabel("__:__:__", 30);
    private RoutinePasser routine = new RoutinePasser();

    public EmptyWorkoutPage()
    {
        Background = Colors.White;
        Button saveButton = Controls.MakeButton("Save", Colors.Black, Colors.White, Colors.Black, 2, 6, 20, new Thickness());
        Button finishButton = Controls.MakeButton("Finish", Colors.Black, Colors.White, Colors.Black, 2, 6, 20, new Thickness());
        Picker exercisePicker = Controls.MakeSelector("Add an Exercise", ExerciseList(), 18);

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
                buttons, exercisePicker
            }
        };
        
        exercisePicker.SelectedIndexChanged += OnPickerSelectedIndexChanged;
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
    
    private static List<string> EquipmentList()
    {
        List<string> equipmentList = new List<string>();
        List<EquipmentPasser> passerList = Equipment.GetEquipment();

        foreach (EquipmentPasser passer in passerList)
        {
            equipmentList.Add(passer.Name);
        }

        return equipmentList;
    }
    
    private async void OnPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        Picker picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;

        if (selectedIndex != -1)
        {
            RoutineExercisesPasser exercise = new RoutineExercisesPasser();
            exercise.RoutineId = routine.RoutineId;
            exercise.ExerciseId = Exercise.GetExerciseByName((string)picker.ItemsSource[selectedIndex]).ExerciseId;
        
            string sets = await Application.Current!.MainPage!.DisplayPromptAsync("Sets", "How many sets for this exercise", "Confirm", "Back");
            exercise.Sets = Convert.ToInt32(sets);

            string rest = await Application.Current!.MainPage!.DisplayPromptAsync("Rest", "How much rest do you want for this exercise", "Confirm", "Back");
            exercise.Rest = Convert.ToInt32(rest);

            List<string> equipmentList = EquipmentList();
            string chosenEquipment = await Application.Current!.MainPage!.DisplayActionSheetAsync("Equipment", "Back", null, equipmentList.ToArray());

            if (chosenEquipment != null && chosenEquipment != "Back")
            {
                exercise.EquipmentId = Equipment.GetEquipmentByName(chosenEquipment).EquipmentId;
            }
            
            _layout.Children.Add(BuildWorkoutCard(exercise));
        }
    }

    private View BuildWorkoutCard(RoutineExercisesPasser exercise)
    {
        VerticalStackLayout workoutLayout = new VerticalStackLayout()
        {
            Padding = new Thickness(20, 14),
            Spacing = 12,
            Children =
            {
                new HorizontalStackLayout()
                {
                    Spacing = 20,
                    Children =
                    {
                        Controls.MakeLabel(Exercise.GetExerciseById(exercise.ExerciseId).Name, 22, Colors.Black, LayoutOptions.Center),
                        Controls.MakeButton(Equipment.GetEquipmentById(exercise.EquipmentId).Name[0].ToString(), Colors.White, Colors.Black, Colors.Black, 5, 2, 22, new Thickness())
                    }
                },
                new BoxView { HeightRequest = 2, Color = Colors.Black },
            }
        };

        for (int i = 1; i <= exercise.Sets; i++)
        {
            Entry reps = Controls.MakeTextBox("_", Colors.Gray, Colors.Black, 12, 50, LayoutOptions.End);
            Entry weight = Controls.MakeTextBox("_", Colors.Gray, Colors.Black, 12, 50, LayoutOptions.End);
            CheckBox checkBox = new CheckBox();

            workoutLayout.Children.Add(new HorizontalStackLayout()
            {
                Spacing = 8,
                Children =
                {
                    weight, reps, checkBox
                }
            });
        }
        
        return new Border()
        {
            BackgroundColor = Colors.LightGray,
            Content = workoutLayout
        };
    }
}