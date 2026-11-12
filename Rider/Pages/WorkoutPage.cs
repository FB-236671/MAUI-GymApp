using System.Diagnostics;
using MAUI_GymApp.Rider.Database.Objects;

namespace MAUI_GymApp;

public class WorkoutPage : ContentPage
{
    private readonly VerticalStackLayout _layout;
    private readonly VerticalStackLayout _restBar;
    private readonly RoutineParser? _routineObject;
    private readonly IDispatcherTimer _timer;
    private IDispatcherTimer _restTimer;
    private readonly Stopwatch _stopwatch = new();
    private readonly Stopwatch _restStopWatch = new();
    private readonly Label _restLabel = UIElements.MakeLabel("__:__", 24);
    private readonly Label _timerLabel = UIElements.MakeLabel("__:__:__", 30);
    private readonly List<List<SetParser>> _setListList = new();
    private readonly List<RoutineExercisesParser> _exercises = new();

    public WorkoutPage(RoutineParser? routineObject = null)
    {
        Background = Colors.White;
        _routineObject = routineObject;
        _timer = GeneralControls.StartTimer(_stopwatch, _timerLabel, Dispatcher);
        
        _layout = new VerticalStackLayout
        {
            Padding = new Thickness(16, 16, 16, 0),
            Spacing = 16,
            Children =
            {
                new HorizontalStackLayout
                {
                    Spacing = 30,
                    Children =
                    {
                        UIElements.MakeLabel(routineObject?.Name ?? "Empty Workout", 25),
                        _timerLabel
                    }
                },
                UIElements.MakeLine(2, Colors.Black)
            }
        };

        Button finishButton = UIElements.MakeButton("Finish", Colors.Black, Colors.White, Colors.Black, 2, 6, 20, new Thickness());
        finishButton.Clicked += OnFinishClicked;

        if (routineObject is null)
        {
            Button saveButton = UIElements.MakeButton("Save", Colors.Black, Colors.White, Colors.Black, 2, 6, 20, new Thickness());
            saveButton.Clicked += OnSaveAsRoutineClicked;

            Picker exercisePicker = UIElements.MakeSelector("Add an Exercise", Exercise.ExerciseNameList(), 18);
            exercisePicker.SelectedIndexChanged += OnPickerSelectedIndexChanged;

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

            _layout.Children.Add(buttons);
            _layout.Children.Add(exercisePicker);
        }
        else
        {
            _layout.Children.Add(finishButton);
            _exercises.AddRange(RoutineExercises.GetRoutineExercisesByRoutineId(routineObject.RoutineId));
            foreach (RoutineExercisesParser exercise in _exercises)
            {
                _layout.Children.Add(UIControls.BuildWorkoutCard(exercise, _setListList, OnCheckChanged));
            }
        }

        _restBar = new VerticalStackLayout
        {
            Spacing = 0,
            Children =
            {
                UIElements.MakeLine(2, Colors.Black),
                new HorizontalStackLayout
                {
                    Padding = new Thickness(16, 8),
                    Spacing = 10,
                    HorizontalOptions = LayoutOptions.Center,
                    Children =
                    {
                        UIElements.MakeLabel("Rest:", 24),
                        _restLabel
                    }
                }
            }
        };

        Grid root = new Grid
        {
            RowDefinitions =
            {
                new RowDefinition { Height = GridLength.Star },
                new RowDefinition { Height = GridLength.Auto }
            }
        };
        root.Add(new ScrollView { Content = _layout }, 0);
        root.Add(_restBar, 0, 1);

        Content = root;
    }
    
    private async void OnFinishClicked(object? sender, EventArgs e)
    {
        if (_exercises.Count == 0)
        {
            await DisplayAlert("Empty Workout", "Add at least one exercise first", "Close");
            return;
        }
        if (!await SaveControls.ValidateSets(_setListList)) return;

        StopTimer();
        SaveControls.SaveWholeWorkout(_exercises, _setListList, _stopwatch);
        GeneralControls.GoHome();
    }
    
    private async void OnSaveAsRoutineClicked(object? sender, EventArgs e)
    {
        if (_exercises.Count == 0) return;
        if (!await SaveControls.ValidateSets(_setListList)) return;

        string? name = await DisplayPromptAsync("Name", "What do you want to call your routine?", "Confirm", "Back");
        if (name is null) return;
        if (TypeChecker.StringCheck(name, "Name") is string nameError)
        {
            await DisplayAlert("Input Error", nameError, "Close");
            return;
        }

        int routineId = Routine.CreateRoutine(new RoutineParser { UserId = MainPage.User.UserId, Name = name });
        foreach (RoutineExercisesParser re in _exercises)
        {
            re.RoutineId = routineId;
            RoutineExercises.AddRoutineExercises(re);
        }

        StopTimer();
        SaveControls.SaveWholeWorkout(_exercises, _setListList, _stopwatch);
        GeneralControls.GoHome();
    }

    private async void OnPickerSelectedIndexChanged(object? sender, EventArgs e)
    {
        if (sender is not Picker picker || picker.SelectedIndex == -1) return;

        string selectedName = (string)picker.ItemsSource[picker.SelectedIndex];
        picker.SelectedIndex = -1;

        if (_exercises.Any(x => Exercise.GetExerciseById(x.ExerciseId).Name == selectedName))
        {
            await DisplayAlert("Picker Error", "You cannot select two of the same exercise", "Close"); return;
        }

        string? sets = await DisplayPromptAsync("Sets", "How many sets for this exercise", "Confirm", "Back", keyboard: Keyboard.Numeric);
        if (sets is null) return;
        if (TypeChecker.IntegerCheck(sets, "Sets", 1, 20, out int checkedSets) is string setsError)
        {
            await DisplayAlert("Input Error", setsError, "Close"); return;
        }

        string? rest = await DisplayPromptAsync("Rest", "How much rest do you want for this exercise", "Confirm", "Back", keyboard: Keyboard.Numeric);
        if (rest is null) return;
        if (TypeChecker.IntegerCheck(rest, "Rest", 0, 600, out int checkedRest) is string restError)
        {
            await DisplayAlert("Input Error", restError, "Close"); return;
        }

        string? chosenEquipment = await DisplayActionSheet("Equipment", "Back", null, Equipment.EquipmentNameList().ToArray());
        if (chosenEquipment is null || chosenEquipment == "Back") return;

        RoutineExercisesParser exercise = new()
        {
            ExerciseId = Exercise.GetExerciseByName(selectedName).ExerciseId,
            Sets = checkedSets,
            Rest = checkedRest,
            EquipmentId = Equipment.GetEquipmentByName(chosenEquipment).EquipmentId
        };

        _layout.Children.Add(UIControls.BuildWorkoutCard(exercise, _setListList, OnCheckChanged));
        _exercises.Add(exercise);
    }

    public void OnCheckChanged(object? sender, CheckedChangedEventArgs e)
    {
        if (!e.Value) return;
        if (sender is not CheckBox checkBox || checkBox.BindingContext is not RoutineExercisesParser routineObject) return;

        _restTimer?.Stop();
        _restTimer = GeneralControls.StartRest(_restStopWatch, _restLabel, Dispatcher, TimeSpan.FromSeconds(routineObject.Rest));
    }

    protected override bool OnBackButtonPressed() => GeneralControls.ConfirmQuitWorkout(this, _timer, _stopwatch);
    
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        StopTimer();
    }

    private void StopTimer()
    {
        _timer.Stop();
        _stopwatch.Stop();
    }
}