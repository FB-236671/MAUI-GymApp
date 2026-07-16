using MAUI_GymApp.Database.Passers;
using MAUI_GymApp.Rider.Database.Queries;

namespace MAUI_GymApp;

public class CreateWorkoutPage : ContentPage
{
    private readonly VerticalStackLayout _layout;
    private List<(string _name, Entry _setEntry, Entry _restEntry, Picker _equipmentPicker)> _routineExerciseList = new();
    private Entry nameBox;
    
    public CreateWorkoutPage()
    {
        Background = Colors.White;
        Picker picker = Controls.MakeSelector("Add an Exercise", ExerciseList(), 18);
        Button saveButton = Controls.MakeButton("Save", Colors.Black, Colors.White, Colors.Black, 2, 6, 20, new Thickness());
        nameBox = Controls.MakeTextBox("Name", Colors.Gray, Colors.Black, 30, 250, LayoutOptions.Center);
        
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
                                new BoxView { HeightRequest = 2, Color = Colors.Black }
                            }
                        }
                    }
                },
                new BoxView { HeightRequest = 2, Color = Colors.Black },
                saveButton,
                picker
            }
        };
        picker.SelectedIndexChanged += OnPickerSelectedIndexChanged;
        saveButton.Clicked += OnSaveClicked;
        Content = _layout;
    }

    private void OnSaveClicked(object? sender, EventArgs e)
    {
        RoutinePasser routineObject = new RoutinePasser();
        routineObject.UserId = MainPage.User.UserId;
        routineObject.Name = nameBox.Text;
        RoutineQueries.CreateRoutine(routineObject);
        int routineId = RoutineQueries.GetRoutineByName(routineObject.Name).RoutineId;
        
        foreach ((string _name, Entry _setEntry, Entry _restEntry, Picker _equipmentPicker) exercise in _routineExerciseList)
        {
            ExercisesPasser exercisesObject = new ExercisesPasser();
            exercisesObject.RoutineId = routineId;
            exercisesObject.ExerciseId = ExerciseQueries.GetExerciseByName(exercise._name).ExerciseId;
            exercisesObject.Sets = Convert.ToInt32(exercise._setEntry.Text);
            exercisesObject.Rest = Convert.ToInt32(exercise._restEntry.Text);
            exercisesObject.EquipmentId = EquipmentQueries.GetEquipmentByName((string)exercise._equipmentPicker.SelectedItem).EquipmentId;
            ExercisesQueries.AddExercises(exercisesObject);
        }
        
        Application.Current!.MainPage = new NavigationPage(new MainPage())
        {
            BarBackgroundColor = Colors.White,
            BarTextColor = Colors.Black,
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

    private static List<string> EquipmentList()
    {
        List<string> equipmentList = new List<string>();
        List<EquipmentPasser> passerList = EquipmentQueries.GetEquipment();

        foreach (EquipmentPasser passer in passerList)
        {
            equipmentList.Add(passer.Name);
        }

        return equipmentList;
    }

    private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        Picker picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;

        if (selectedIndex != -1)
        {
            _layout.Children.Add(BuildWorkoutCard((string)picker.ItemsSource[selectedIndex]));
        }
    }

    private View BuildWorkoutCard(string name)
    {
        Entry setEntry = Controls.MakeTextBox("Sets", Colors.White, Colors.Black, 12, 30, LayoutOptions.End);
        Entry restEntry = Controls.MakeTextBox("Rest", Colors.White, Colors.Black, 12, 50, LayoutOptions.End);
        Picker equipmentPicker = Controls.MakeSelector("Equipment", EquipmentList(), 12);
        
        VerticalStackLayout workoutLayout = new VerticalStackLayout()
        {
            Padding = new Thickness(20, 14),
            Spacing = 12,
            Children =
            {
                Controls.MakeLabel(name, 22, Colors.Black, LayoutOptions.Center),
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

        _routineExerciseList.Add((name, setEntry, restEntry, equipmentPicker));

        return new Border
        {
            BackgroundColor = Colors.LightGray,
            Content = workoutLayout
        };
    }
}