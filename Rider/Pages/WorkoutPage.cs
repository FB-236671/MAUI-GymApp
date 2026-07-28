using System.Diagnostics;
using MAUI_GymApp.Rider.Database;
using MAUI_GymApp.Rider.Database.Objects;

namespace MAUI_GymApp;

public class WorkoutPage : ContentPage
{
    private readonly VerticalStackLayout _layout;
    private RoutinePasser _routineObject;
    private IDispatcherTimer _timer;
    private Stopwatch _stopwatch = new();
    private Label _timerLabel = Controls.MakeLabel("__:__:__", 30);
    private List<List<(int setNumber, Entry weight, Entry reps, CheckBox checkBox)>> _setListList = new();
    private List<RoutineExercisesPasser> _exercises = new();
    
    public WorkoutPage(RoutinePasser routineObject)
    {
        StartTimer();
        
        Button saveButton = Controls.MakeButton("Finish", Colors.Black, Colors.White, Colors.Black, 2, 6, 20, new Thickness());
        
        Background = Colors.White;
        _routineObject = routineObject;
        _exercises = RoutineExercises.GetRoutineExercisesByRoutineId(routineObject.RoutineId);

        _layout = new VerticalStackLayout()
        {
            Padding = new Thickness(16, 16, 16, 0),
            Spacing = 16,
            Children =
            {
                new HorizontalStackLayout()
                {
                    Spacing = 40,
                    Children =
                    {
                        Controls.MakeLabel(_routineObject.Name, 30), 
                        _timerLabel
                    }
                },
                new BoxView { HeightRequest = 2, Color = Colors.Black },
                saveButton
            }
        };

        foreach (RoutineExercisesPasser exercise in _exercises)
        {
            _layout.Children.Add(BuildWorkoutCard(exercise));
        }

        saveButton.Clicked += OnSaveClicked;
        Content = new ScrollView { Content = _layout };
    }

    private void StartTimer()
    {
        _stopwatch.Start();
        _timer = Dispatcher.CreateTimer();
        _timer.Interval = TimeSpan.FromSeconds(1);
        _timer.Tick += (sender, args) => _timerLabel.Text = _stopwatch.Elapsed.ToString(@"hh\:mm\:ss");
        _timer.Start();
    }

    private void OnSaveClicked(object? sender, EventArgs e)
    {
        _timer.Stop();
        _stopwatch.Stop();

        WorkoutPasser workoutObject = new WorkoutPasser();
        List<WorkoutExercisesPasser> workoutExercisesList = new List<WorkoutExercisesPasser>();
        List<List<WorkoutSetsPasser>> workoutSetsListList = new List<List<WorkoutSetsPasser>>();

        foreach (RoutineExercisesPasser exercise in _exercises)
        {
            WorkoutExercisesPasser workoutExercisesObject = new WorkoutExercisesPasser();
            workoutExercisesObject.ExerciseId = exercise.ExerciseId;
            workoutExercisesObject.Sets = exercise.Sets;
            workoutExercisesList.Add(workoutExercisesObject);
        }

        int totalVolume = 0;
        
        foreach (List<(int setNumber, Entry weight, Entry reps, CheckBox checkBox)> setList in _setListList)
        {
            List<WorkoutSetsPasser> workoutSetsList = new List<WorkoutSetsPasser>();
            int drawback = 0;
            
            foreach ((int setNumber, Entry weight, Entry reps, CheckBox checkBox) set in setList)
            {
                if (!set.checkBox.IsChecked)
                {
                    drawback += -1;
                }
                
                else
                {
                    totalVolume += Convert.ToInt32(set.reps.Text) * Convert.ToInt32(set.weight.Text);

                    WorkoutSetsPasser workoutSetsObject = new WorkoutSetsPasser();
                    workoutSetsObject.SetNumber = set.setNumber + drawback;
                    workoutSetsObject.Weight = Convert.ToInt32(set.weight.Text);
                    workoutSetsObject.Reps = Convert.ToInt32(set.reps.Text);
                    workoutSetsList.Add(workoutSetsObject);
                }
            }
            workoutSetsListList.Add(workoutSetsList);
        }
        
        workoutObject.UserId = MainPage.User.UserId;
        workoutObject.Duration = (int)_stopwatch.Elapsed.TotalSeconds;
        workoutObject.Volume = totalVolume;
        
        Workout.SaveWorkout(workoutObject, workoutExercisesList, workoutSetsListList);
    }

    private View BuildWorkoutCard(RoutineExercisesPasser routineExercisesObject)
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
                        Controls.MakeLabel(Exercise.GetExerciseById(routineExercisesObject.ExerciseId).Name, 22, Colors.Black, LayoutOptions.Center),
                        Controls.MakeButton(GetEquipmentName(routineExercisesObject.EquipmentId).ToString(), Colors.White, Colors.Black, Colors.Black, 5, 2, 22, new Thickness())
                    }
                },
                new BoxView { HeightRequest = 2, Color = Colors.Black },
            }
        };

        List<(int setNumber, Entry weight, Entry reps, CheckBox checkBox)> setList = new();
        
        for (int i = 1; i <= routineExercisesObject.Sets; i++)
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
            
            setList.Add((i, weight, reps, checkBox));
        }

        _setListList.Add(setList);
        
        return new Border()
        {
            BackgroundColor = Colors.LightGray,
            Content = workoutLayout
        };
    }

    private char GetEquipmentName(int equipmentId)
    {
        EquipmentPasser equipmentObject = Equipment.GetEquipmentById(equipmentId);
        string name = equipmentObject.Name;
        return name[0];
    }
}