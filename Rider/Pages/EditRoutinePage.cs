using MAUI_GymApp.Rider.Database.Objects;

namespace MAUI_GymApp;

public class EditRoutinePage : ContentPage
{
    private readonly VerticalStackLayout _layout;
    private List<RoutineDataPasser> _routineExerciseList = new();
    private Entry nameBox;
    private RoutinePasser routine;

    public EditRoutinePage(RoutinePasser passedRoutine)
    {
        Background = Colors.White;
        routine = passedRoutine;
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
        LoadRoutine();
        Content = new ScrollView { Content = _layout };
    }

    private void LoadRoutine()
    {
        nameBox.Text = routine.Name;

        List<RoutineExercisesPasser> routineExercisesList = RoutineExercises.GetRoutineExercisesByRoutineId(routine.RoutineId);

        foreach (RoutineExercisesPasser exercise in routineExercisesList)
        {
            _layout.Children.Add(UIControls.BuildRoutineCard(Exercise.GetExerciseById(exercise.ExerciseId).Name, _routineExerciseList);
        }
    }

    private async void OnSaveClicked(object? sender, EventArgs e)
    {
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
            _layout.Children.Add(UIControls.BuildRoutineCard((string)picker.ItemsSource[selectedIndex], _routineExerciseList));
        }
    }
}