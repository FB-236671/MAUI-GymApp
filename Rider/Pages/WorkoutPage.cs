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
    private List<(Entry reps, Entry weight, CheckBox checkBox)> _setList;
    
    public WorkoutPage(RoutinePasser routineObject)
    {
        StartTimer();
        
        Button saveButton = Controls.MakeButton("Finish", Colors.Black, Colors.White, Colors.Black, 2, 6, 20, new Thickness());
        
        Background = Colors.White;
        _routineObject = routineObject;
        List<RoutineExercisesPasser> exercises = RoutineExercises.GetRoutineExercisesByRoutineId(routineObject.RoutineId);

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

        foreach (RoutineExercisesPasser exercise in exercises)
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
        _timer.Interval = TimeSpan.FromMicroseconds(100);
        _timer.Tick += (sender, args) => _timerLabel.Text = _stopwatch.Elapsed.ToString(@"hh\:mm\:ss");
        _timer.Start();
    }

    private void OnSaveClicked(object? sender, EventArgs e)
    {
        _timer.Stop();
        _stopwatch.Stop();

        WorkoutPasser workoutObject = new WorkoutPasser();
        workoutObject.UserId = MainPage.User.UserId;
        workoutObject.Duration = (int)_stopwatch.Elapsed.TotalSeconds;
        int totalVolume = 0;
        foreach ((Entry reps, Entry weight, CheckBox checkBox) set in _setList)
        {
            if (set.checkBox.IsChecked)
            {
                totalVolume += Convert.ToInt32(set.weight.Text) * Convert.ToInt32(set.weight.Text);
            }
        }
        workoutObject.Volume = totalVolume;
        Workout.SaveWorkout(workoutObject);
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

        for (int i = 0; i < routineExercisesObject.Sets; i++)
        {
            Entry reps = Controls.MakeTextBox(" ", Colors.Gray, Colors.Black, 12, 50, LayoutOptions.End);
            Entry weight = Controls.MakeTextBox(" ", Colors.Gray, Colors.Black, 12, 50, LayoutOptions.End);
            CheckBox checkBox = new CheckBox();
            
            workoutLayout.Children.Add(new HorizontalStackLayout()
            {
                Spacing = 8,
                Children =
                {
                    reps, weight, checkBox
                }
            });
            
            _setList.Add((reps, weight, checkBox));
        }

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