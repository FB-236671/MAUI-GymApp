using MAUI_GymApp.Rider.Database.Objects;

namespace MAUI_GymApp;

public class RoutinePage : ContentPage
{
    private readonly VerticalStackLayout _layout;
    private List<RoutineDataRepository> _routineExerciseList = new();
    private Entry nameBox;
    private RoutineRepository? routine;

    public RoutinePage(UserRepository user, RoutineRepository? parsedRoutine = null)
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
                UIElements.MakeLine(),
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
        if (selectedIndex == -1) return;

        string name = (string)picker.ItemsSource[selectedIndex]!;

        foreach (RoutineDataRepository exercise in _routineExerciseList)
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

        foreach (RoutineDataRepository exercise in _routineExerciseList)
        {
            if (TypeChecker.IntegerIndexCheck(exercise.SetEntry.Text, "Sets", exercise.Name, 1, 20, out int sets) is string setsError) { errors.Add(setsError); }
            if (exercise.RestPicker.SelectedItem is null) { errors.Add("Please select rest for " + exercise.Name); }
            if (exercise.EquipmentPicker.SelectedItem is null) { errors.Add("Please select equipment for " + exercise.Name); }
        }

        if (errors.Count > 0)
        {
            await DisplayAlert("Input Error", string.Join("\n", errors), "Close");
            return;
        }

        if (routine != null)
        {
            Routine.DeleteRoutine(routine.RoutineId);
            Routine.DeleteRoutineExercises(routine.RoutineId);
        }

        RoutineRepository routineObject = new RoutineRepository();
        routineObject.UserId = MainPage.User.UserId;
        routineObject.Name = nameBox.Text.Trim();
        int routineId = Routine.CreateRoutine(routineObject);

        foreach (RoutineDataRepository exercise in _routineExerciseList)
        {
            RoutineExercisesRepository routineExercisesObject = new RoutineExercisesRepository();
            routineExercisesObject.RoutineId = routineId;
            routineExercisesObject.ExerciseId = Exercise.GetExerciseByName(exercise.Name).ExerciseId;
            routineExercisesObject.Sets = Convert.ToInt32(exercise.SetEntry.Text);
            routineExercisesObject.Rest = Convert.ToInt32(exercise.RestPicker.SelectedItem);
            routineExercisesObject.EquipmentId = Equipment.GetEquipmentByName((string)exercise.EquipmentPicker.SelectedItem).EquipmentId;
            Routine.AddRoutineExercises(routineExercisesObject);
        }
        GeneralControls.GoHome();
    }
}