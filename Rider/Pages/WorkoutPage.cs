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
    private Label _timerLabel = UIElements.MakeLabel("__:__:__", 30);
    private List<List<SetPasser>> _setListList = new();
    private List<RoutineExercisesPasser> _exercises = new();
    
    public WorkoutPage(RoutinePasser routineObject)
    {
        _timer = GeneralControls.StartTimer(_stopwatch, _timerLabel, Dispatcher);
        
        Button saveButton = UIElements.MakeButton("Finish", Colors.Black, Colors.White, Colors.Black, 2, 6, 20, new Thickness());
        
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
                        UIElements.MakeLabel(_routineObject.Name, 25), 
                        _timerLabel
                    }
                },
                UIElements.MakeLine(2, Colors.Black),
                saveButton
            }
        };

        foreach (RoutineExercisesPasser exercise in _exercises)
        {
            _layout.Children.Add(UIControls.BuildWorkoutCard(exercise, _setListList));
        }

        saveButton.Clicked += OnSaveClicked;
        Content = new ScrollView { Content = _layout };
    }

    private void OnSaveClicked(object? sender, EventArgs e)
    {
        _timer.Stop();
        _stopwatch.Stop();
        
        SaveControls.SaveWholeWorkout(_exercises, _setListList, _stopwatch);
        
        GeneralControls.GoHome();
    }
}