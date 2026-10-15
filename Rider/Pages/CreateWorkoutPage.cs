using MAUI_GymApp.Rider.Database.Objects;
using System.Threading.Tasks;

namespace MAUI_GymApp;

public class CreateWorkoutPage : ContentPage
{
    private readonly VerticalStackLayout _layout;
    private List<RoutineDataPasser> _routineExerciseList = new();
    private Entry nameBox;
    
    public CreateWorkoutPage()
    {
        Background = Colors.White;
        Picker picker = UIElements.MakeSelector("Add an Exercise", Exercise.ExerciseNameList(), 18);
        Button saveButton = UIElements.MakeButton("Save", Colors.Black, Colors.White, Colors.Black, 2, 6, 20, new Thickness());
        nameBox = UIElements.MakeTextBox("Name", Colors.Gray, Colors.Black, 30, 250, LayoutOptions.Center);
        
        _layout = new VerticalStackLayout()
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
                        new VerticalStackLayout()
                        {
                            Spacing = 0,
                            Children =
                            {
                                nameBox,
                                UIElements.MakeLine(2, Colors.Black)
                            }
                        }
                    }
                },
                UIElements.MakeLine(2, Colors.Black),
                saveButton,
                picker
            }
        };
        
        picker.SelectedIndexChanged += OnPickerSelectedIndexChanged;
        saveButton.Clicked += OnSaveClicked;
        Content = new ScrollView { Content = _layout };
    }

    private async void OnSaveClicked(object? sender, EventArgs e)
    {
        var errors = new List<string>();
        
        if (TypeChecker.StringCheck(nameBox.Text, "Name") is string nameError) { errors.Add(nameError); }

        foreach (RoutineDataPasser exercise in _routineExerciseList)
        {
            if (TypeChecker.IntegerCheck(exercise.SetEntry.Text, "Sets", exercise.Name, out int sets) is string setsError) { errors.Add(setsError); }
            if (TypeChecker.IntegerCheck(exercise.RestEntry.Text, "Rest", exercise.Name, out int rest) is string restError) { errors.Add(restError); }
        }
        
        if (errors.Count > 0)
        {
            await DisplayAlert("Input Error", string.Join("\n", errors), "Close");
            return;
        }
        
        RoutinePasser routineObject = new RoutinePasser();
        routineObject.UserId = MainPage.User.UserId;
        routineObject.Name = nameBox.Text.Trim();
        Routine.CreateRoutine(routineObject);
        int routineId = Routine.GetRoutineByName(routineObject.Name).RoutineId;
        
        foreach (RoutineDataPasser exercise in _routineExerciseList)
        {
            RoutineExercisesPasser routineExercisesObject = new RoutineExercisesPasser();
            routineExercisesObject.RoutineId = routineId;
            routineExercisesObject.ExerciseId = Exercise.GetExerciseByName(exercise.Name).ExerciseId;
            routineExercisesObject.Sets = Convert.ToInt32(exercise.SetEntry.Text);
            routineExercisesObject.Rest = Convert.ToInt32(exercise.RestEntry.Text);
            routineExercisesObject.EquipmentId = Equipment.GetEquipmentByName((string)exercise.EquipmentPicker.SelectedItem).EquipmentId;
            RoutineExercises.AddRoutineExercises(routineExercisesObject);
        }
        
        GeneralControls.GoHome();
    }

    private async Task OnPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        Picker picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;

        foreach (RoutineDataPasser exercise in _routineExerciseList)
        {
            if (exercise.Name == (string)picker.ItemsSource[selectedIndex])
            {
                await DisplayAlert("Picker Error", "You cannot select two of the same exercise", "Close");
            }
        }

        if (selectedIndex != -1)
        {
            _layout.Children.Add(BuildCreateWorkoutCard((string)picker.ItemsSource[selectedIndex]));
        }
    }

    private View BuildCreateWorkoutCard(string name)
    {
        Entry setEntry = UIElements.MakeTextBox("Sets", Colors.White, Colors.Black, 12, 35, LayoutOptions.End);
        Entry restEntry = UIElements.MakeTextBox("Rest", Colors.White, Colors.Black, 12, 50, LayoutOptions.End);
        Picker equipmentPicker = UIElements.MakeSelector("Equipment", Equipment.EquipmentNameList(), 12);
        
        VerticalStackLayout workoutLayout = new VerticalStackLayout()
        {
            Padding = new Thickness(20, 14),
            Spacing = 12,
            Children =
            {
                UIElements.MakeLabel(name, 22, Colors.Black, LayoutOptions.Center),
                new HorizontalStackLayout()
                {
                    Spacing = 8,
                    Children =
                    {
                        setEntry,
                        restEntry,
                        equipmentPicker
                    }
                }
            }
        };

        RoutineDataPasser routineData = new RoutineDataPasser();
        routineData.Name = name;
        routineData.SetEntry = setEntry;
        routineData.RestEntry = restEntry;
        routineData.EquipmentPicker = equipmentPicker;
        _routineExerciseList.Add(routineData);

        return UIElements.MakeCard(Colors.LightGray, workoutLayout);
    }
}