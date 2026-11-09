using MAUI_GymApp.Rider.Database.Objects;

namespace MAUI_GymApp;

public class RoutinePage : ContentPage
{
    private readonly VerticalStackLayout _layout;
    private List<RoutineDataParser> _routineExerciseList = new();
    private Entry nameBox;
    private RoutineParser? routine;

    public RoutinePage(RoutineParser? parsedRoutine = null)
    {
        Background = Colors.White;
        Picker picker = UIElements.MakeSelector("Add an Exercise", Exercise.ExerciseNameList(), 18);
        Button saveButton = UIElements.MakeButton("Save", Colors.Black, Colors.White, Colors.Black, 2, 6, 20, 4);
        nameBox = UIElements.MakeTextBox("Name", Colors.Gray, Colors.Black, 30, 250, LayoutOptions.Start);
        nameBox.HorizontalOptions = LayoutOptions.Start;

        _layout = new VerticalStackLayout()
        {
            Padding = new Thickness(16, 16, 16, 0),
            Spacing = 16,
            Children =
            {
                nameBox,
                UIElements.MakeLine(2, Colors.Black),
                saveButton,
                picker
            }
        };

        picker.SelectedIndexChanged += OnPickerSelectedIndexChanged;
        saveButton.Clicked += OnSaveClicked;
        if (parsedRoutine != null)
        {
            routine = parsedRoutine;
            SaveControls.LoadRoutine(nameBox, routine, _layout, _routineExerciseList);
        }
        Content = new ScrollView { Content = _layout };
    }

    private async void OnPickerSelectedIndexChanged(object? sender, EventArgs e)
    {
        Picker picker = (Picker)sender!;
        int selectedIndex = picker.SelectedIndex;
        if (selectedIndex == -1) { return; }

        string name = (string)picker.ItemsSource[selectedIndex]!;

        foreach (RoutineDataParser exercise in _routineExerciseList)
        {
            if (exercise.Name == name)
            {
                await DisplayAlert("Picker Error", "You cannot select two of the same exercise", "Close");
                picker.SelectedIndex = -1;
                return;
            }
        }

        _layout.Children.Add(UIControls.BuildRoutineCard(name, _routineExerciseList));
        picker.SelectedIndex = -1;
    }

    private async void OnSaveClicked(object? sender, EventArgs e)
    {
        var errors = new List<string>();

        if (TypeChecker.StringCheck(nameBox.Text, "Name") is string nameError) { errors.Add(nameError); }

        foreach (RoutineDataParser exercise in _routineExerciseList)
        {
            if (TypeChecker.IntegerIndexCheck(exercise.SetEntry.Text, "Sets", exercise.Name, 1, 20, out int sets) is string setsError) { errors.Add(setsError); }
            if (TypeChecker.IntegerIndexCheck(exercise.RestEntry.Text, "Rest", exercise.Name, 0, 600, out int rest) is string restError) { errors.Add(restError); }
            if (exercise.EquipmentPicker.SelectedItem is null) { errors.Add("Please select equipment for " + exercise.Name); }
        }

        if (errors.Count > 0)
        {
            await DisplayAlert("Input Error", string.Join("\n", errors), "Close");
            return;
        }

        Routine.DeleteRoutine(routine.RoutineId);
        RoutineExercises.DeleteRoutineExercises(routine.RoutineId);

        RoutineParser routineObject = new RoutineParser();
        routineObject.UserId = MainPage.User.UserId;
        routineObject.Name = nameBox.Text.Trim();
        int routineId = Routine.CreateRoutine(routineObject);

        foreach (RoutineDataParser exercise in _routineExerciseList)
        {
            RoutineExercisesParser routineExercisesObject = new RoutineExercisesParser();
            routineExercisesObject.RoutineId = routineId;
            routineExercisesObject.ExerciseId = Exercise.GetExerciseByName(exercise.Name).ExerciseId;
            routineExercisesObject.Sets = Convert.ToInt32(exercise.SetEntry.Text);
            routineExercisesObject.Rest = Convert.ToInt32(exercise.RestEntry.Text);
            routineExercisesObject.EquipmentId = Equipment.GetEquipmentByName((string)exercise.EquipmentPicker.SelectedItem).EquipmentId;
            RoutineExercises.AddRoutineExercises(routineExercisesObject);
        }

        GeneralControls.GoHome();
    }
}