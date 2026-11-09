using System.Diagnostics;
using System.Threading.Tasks;
using MAUI_GymApp.Rider.Database.Objects;

namespace MAUI_GymApp;

public class Workout : ContentPage
{
    private readonly VerticalStackLayout _layout;
    private readonly HorizontalStackLayout _subLayout;
    private IDispatcherTimer _timer;
    private Stopwatch _stopwatch = new();
    private Label _timerLabel = UIElements.MakeLabel("__:__:__", 30);
    private List<List<SetParser>> _setListList = new();
    private List<RoutineExercisesParser> _exercises = new();
    private RoutineParser? _routineObject;

    public Workout(RoutineParser? routineObject = null)
    {
        Background = Colors.White;
        _timer = GeneralControls.StartTimer(_stopwatch, _timerLabel, Dispatcher);
        _exercises = RoutineExercises.GetRoutineExercisesByRoutineId(routineObject.RoutineId);

        Button saveButton = UIElements.MakeButton("Save", Colors.Black, Colors.White, Colors.Black, 2, 6, 20, new Thickness());

        _layout = new VerticalStackLayout()
        {
            Padding = new Thickness(16, 16, 16, 0),
            Spacing = 16,
            Children =
            {
                _subLayout = new HorizontalStackLayout()
                {
                    Spacing = 30,
                    Children =
                    {
                        UIElements.MakeLabel("New Routine", 25),
                        _timerLabel
                    }
                },
                UIElements.MakeLine(2, Colors.Black),
                buttons, exercisePicker
            }
        };

        string name = "Name";
        if (routineObject == null)
        {
            _routineObject = routineObject;
            Button finishButton = UIElements.MakeButton("Finish", Colors.Black, Colors.White, Colors.Black, 2, 6, 20, new Thickness());
            Picker exercisePicker = UIElements.MakeSelector("Add an Exercise", Exercise.ExerciseNameList(), 18);

            Grid buttons = new Grid
            {
                ColumnDefinitions =
            {
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Star }
            },
                HeightRequest = 40,
                ColumnSpacing = 0
            };
            buttons.Add(saveButton, 0);
            buttons.Add(finishButton, 1);

            name = _routineObject.Name;

            _layout.Children.Add(buttons);
            _layout.Children.Add(exercisePicker);
        }
        else
        {
            _layout.Children.Add(saveButton);
            _layout.Childre
        }
    }
}