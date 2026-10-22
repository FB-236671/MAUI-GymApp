using System.Diagnostics;
using MAUI_GymApp.Rider.Database;
using MAUI_GymApp.Rider.Database.Objects;

namespace MAUI_GymApp;

public class EmptyWorkoutPage : ContentPage
{
    private readonly VerticalStackLayout _layout;
    private List<RoutineExercisesPasser> _routineExerciseList = new();
    private Label _timerLabel = UIElements.MakeLabel("__:__:__", 30);
    private Stopwatch _stopwatch = new();
    private IDispatcherTimer _timer;
    private List<List<SetPasser>> _setListList = new();

    public EmptyWorkoutPage()
    {
        _timer = GeneralControls.StartTimer(_stopwatch, _timerLabel, Dispatcher);
        
        Background = Colors.White;
        Button saveButton = UIElements.MakeButton("Save", Colors.Black, Colors.White, Colors.Black, 2, 6, 20, new Thickness());
        Button finishButton = UIElements.MakeButton("Finish", Colors.Black, Colors.White, Colors.Black, 2, 6, 20, new Thickness());
        Picker exercisePicker = UIElements.MakeSelector("Add an Exercise", Exercise.ExerciseNameList(), 18);

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
                        UIElements.MakeLabel("New Routine", 25),
                        _timerLabel
                    }
                },
                UIElements.MakeLine(2, Colors.Black),
                buttons, exercisePicker
            }
        };
        
        exercisePicker.SelectedIndexChanged += OnPickerSelectedIndexChanged;
        saveButton.Clicked += OnSavedClicked;
        finishButton.Clicked += OnFinishedClicked;
        Content = new ScrollView { Content = _layout };
    }

    private void OnFinishedClicked(object? sender, EventArgs e)
    {
        SaveControls.SaveWholeWorkout(_routineExerciseList, _setListList, _stopwatch);

        _timer.Stop();
        _stopwatch.Stop();

        GeneralControls.GoHome();
    }

    private async void OnSavedClicked(object? sender, EventArgs e)
    {
        _timer.Stop();
        _stopwatch.Stop();

        string name = await Application.Current!.MainPage!.DisplayPromptAsync("Name", "What do you want to call your exercise", "Confirm", "Back");
        if (TypeChecker.StringCheck(name, "Name") is string nameError)
        {
            await DisplayAlert("Input Error", nameError, "Close");
            return;
        }

        RoutinePasser routineObject = new RoutinePasser();
        routineObject.UserId = MainPage.User.UserId;
        routineObject.Name = name;
        int routineId = Routine.CreateRoutine(routineObject);
        
        foreach (RoutineExercisesPasser routineExercisesObject in _routineExerciseList)
        {
            routineExercisesObject.RoutineId = routineId;
            RoutineExercises.AddRoutineExercises(routineExercisesObject);
        }

        SaveControls.SaveWholeWorkout(_routineExerciseList, _setListList, _stopwatch);

        GeneralControls.GoHome();
    }
    
    private async void OnPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        Picker picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;

        foreach (RoutineExercisesPasser exercise in _routineExerciseList)
        {
            if (Exercise.GetExerciseById(exercise.ExerciseId).Name == (string)picker.ItemsSource[selectedIndex])
            {
                await DisplayAlert("Picker Error", "You cannot select two of the same exercise", "Close");
            }
        }

        if (selectedIndex != -1)
        {
            RoutineExercisesPasser exercise = new RoutineExercisesPasser();
            
            exercise.ExerciseId = Exercise.GetExerciseByName((string)picker.ItemsSource[selectedIndex]).ExerciseId;
            
            string sets = await Application.Current!.MainPage!.DisplayPromptAsync("Sets", "How many sets for this exercise", "Confirm", "Back");
            if (TypeChecker.IntegerCheck(sets, "Sets", 1, 20, out int checkedSets) is string setsError)
            {
                await DisplayAlert("Input Error", setsError, "Close");
                return;
            }
            exercise.Sets = Convert.ToInt32(checkedSets);

            string rest = await Application.Current!.MainPage!.DisplayPromptAsync("Rest", "How much rest do you want for this exercise", "Confirm", "Back");
            if (TypeChecker.IntegerCheck(rest, "Rest", 0, 600, out int checkedRest) is string restError)
            {
                await DisplayAlert("Input Error", restError, "Close");
            }
            exercise.Rest = Convert.ToInt32(checkedRest);

            List<string> equipmentList = Equipment.EquipmentNameList();
            string chosenEquipment = await Application.Current!.MainPage!.DisplayActionSheet("Equipment", "Back", null, equipmentList.ToArray());

            if (chosenEquipment != null && chosenEquipment != "Back")
            {
                exercise.EquipmentId = Equipment.GetEquipmentByName(chosenEquipment).EquipmentId;
            }
            
            _layout.Children.Add(UIControls.BuildWorkoutCard(exercise, _setListList));
            _routineExerciseList.Add(exercise);
        }

        picker.SelectedIndex = -1;
    }
}