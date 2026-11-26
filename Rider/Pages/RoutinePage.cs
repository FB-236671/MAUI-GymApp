using MAUI_GymApp.Rider.Database.Objects;

namespace MAUI_GymApp;

public class RoutinePage : ContentPage
{
    private readonly VerticalStackLayout _layout;
    private List<RoutineDataRepository> _routineExerciseList = new();
    private Entry _nameBox;
    private RoutineRepository? _routine;
    private UserRepository _user;

    public RoutinePage(UserRepository user, RoutineRepository? parsedRoutine = null)
    {
        _user = user;
        Background = Colors.White;
        Picker picker = UIElements.MakeSelector("Add an Exercise", Exercise.ExerciseNameList(), Colors.Black, 18);
        Button saveButton = UIElements.MakeButton("Save", Colors.Black, Colors.White, Colors.Black, 2, 6, 20, 4);
        _nameBox = UIElements.MakeTextBox("Name", Colors.Gray, Colors.Black, 30, 250, LayoutOptions.Start);
        _nameBox.HorizontalOptions = LayoutOptions.Start;

        _layout = new VerticalStackLayout()
        {
            Padding = new Thickness(16, 16, 16, 0),
            Spacing = 16,
            Children =
            {
                _nameBox,
                UIElements.MakeLine(),
                saveButton,
                picker
            }
        };

        picker.SelectedIndexChanged += OnPickerSelectedIndexChanged;
        saveButton.Clicked += OnSaveClicked;
        if (parsedRoutine != null)
        {
            _routine = parsedRoutine;
            SaveControls.LoadRoutine(_nameBox, _routine, _layout, _routineExerciseList);
        }
        Content = new ScrollView { Content = _layout };
    }

    public async void onRemoveRoutineCard(object? sender, EventArgs e)
    {
        _layout.Children.RemoveAt();
        // Where the name of the buildroutinecard is the same 
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

        if (TypeChecker.StringCheck(_nameBox.Text, "Name") is string nameError) { errors.Add(nameError); }
        if (_routineExerciseList.Count == 0) { errors.Add("Add at least one exercise first"); }

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

        if (_routine != null)
        {
            Routine.DeleteRoutine(_routine.RoutineId);
            Routine.DeleteRoutineExercises(_routine.RoutineId);
        }

        RoutineRepository routineObject = new RoutineRepository();
        routineObject.UserId = _user.UserId;
        routineObject.Name = _nameBox.Text.Trim();
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