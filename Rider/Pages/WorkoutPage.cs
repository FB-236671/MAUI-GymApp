using System.Diagnostics;
using MAUI_GymApp.Rider.Database;
using MAUI_GymApp.Rider.Database.Objects;

namespace MAUI_GymApp;

public class SetPasser
{
    public int setNumber { get; set; }
    public Entry weight { get; set; }
    public Entry reps { get; set; }
    public CheckBox checkBox { get; set; }
    public int equipmentMultiplier { get; set; }
}

public class WorkoutPage : ContentPage
{
    private readonly VerticalStackLayout _layout;
    private RoutinePasser _routineObject;
    private IDispatcherTimer _timer;
    private Stopwatch _stopwatch = new();
    private Label _timerLabel = Controls.MakeLabel("__:__:__", 30);
    private List<List<SetPasser>> _setListList = new();
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
        List<(int bestWeight, int oneRM)> records = new();
        
        foreach (List<SetPasser> setList in _setListList)
        {
            List<WorkoutSetsPasser> workoutSetsList = new List<WorkoutSetsPasser>();
            int drawback = 0;
            int heaviestWeight = 0;
            int best1RM = 0;
            
            foreach (SetPasser set in setList)
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
                    workoutSetsObject.Weight = Convert.ToInt32(set.weight.Text) * set.equipmentMultiplier;
                    workoutSetsObject.Reps = Convert.ToInt32(set.reps.Text);
                    workoutSetsList.Add(workoutSetsObject);

                    if (heaviestWeight < workoutSetsObject.Weight)
                    {
                        heaviestWeight = workoutSetsObject.Weight;
                    }

                    int oneRM = Calculator.OneRepMax(workoutSetsObject.Weight, workoutSetsObject.Reps);

                    if (best1RM < oneRM)
                    {
                        best1RM = oneRM;
                    }
                }
            }
            workoutSetsListList.Add(workoutSetsList);
            records.Add((heaviestWeight, best1RM));
        }
        
        workoutObject.UserId = MainPage.User.UserId;
        workoutObject.Duration = (int)_stopwatch.Elapsed.TotalSeconds;
        workoutObject.Volume = totalVolume;
        
        Workout.SaveWorkout(workoutObject, workoutExercisesList, workoutSetsListList);

        for (int i = 0; i < _exercises.Count; i++)
        {
            RecordPasser bestRecordObject = Record.GetRecordById(workoutObject.UserId, _exercises[i].ExerciseId);

            int previousBestWeight;
            int previousOneRM;

            if (bestRecordObject == null)
            {
                previousBestWeight = 0;
                previousOneRM = 0;
            }
            else
            {
                previousBestWeight = bestRecordObject.BestWeight;
                previousOneRM = bestRecordObject.OneRM;
            }

            if (previousBestWeight < records[i].bestWeight || previousOneRM < records[i].oneRM)
            {
                RecordPasser recordObject = new RecordPasser();
                recordObject.UserId = workoutObject.UserId;
                recordObject.ExerciseId = _exercises[i].ExerciseId;
                recordObject.BestWeight = Math.Max(previousBestWeight, records[i].bestWeight);
                recordObject.OneRM = Math.Max(previousOneRM, records[i].oneRM);

                Record.SaveRecord(recordObject);
            }
        }
        
        Application.Current!.MainPage = new NavigationPage(new MainPage())
        {
            BarBackgroundColor = Colors.White,
            BarTextColor = Colors.Black,
        };
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
                        Controls.MakeButton(Equipment.GetEquipmentById(routineExercisesObject.EquipmentId).Name[0].ToString(), Colors.White, Colors.Black, Colors.Black, 5, 2, 22, new Thickness())
                    }
                },
                new BoxView { HeightRequest = 2, Color = Colors.Black },
            }
        };

        List<SetPasser> setList = new();
        
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

            SetPasser set = new SetPasser();
            set.setNumber = i;
            set.weight = weight;
            set.reps = reps;
            set.checkBox = checkBox;
            set.equipmentMultiplier = Equipment.GetEquipmentById(routineExercisesObject.EquipmentId).Multiplier;
            setList.Add(set);
        }

        _setListList.Add(setList);
        
        return new Border()
        {
            BackgroundColor = Colors.LightGray,
            Content = workoutLayout
        };
    }
}