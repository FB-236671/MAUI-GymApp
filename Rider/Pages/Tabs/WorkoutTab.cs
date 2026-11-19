using MAUI_GymApp.Rider.Database.Objects;

namespace MAUI_GymApp.Views;

public class WorkoutTab : ContentView
{
    private List<WorkoutRepository> workoutList = Workout.GetWorkoutsUntilDate(MainPage.User.UserId, DateTimeOffset.UtcNow.AddDays(-56).ToUnixTimeSeconds());
    private List<string> filterList = new List<string>();
    private Picker filterSelector = new Picker();

    public TrophiesTab(UserRepository user)
    {
        filterSelector = UIElements.MakeSelector("Filter", filterList, 20);

        VerticalStackLayout layout = new VerticalStackLayout
        {
            Padding = new Thickness(16, 16, 16, 0),
            Spacing = 8,
            Children =
            {
                UIElements.MakeLabel("Workout History", 30),
                filterSelector
            }
        };

        foreach (WorkoutRepository workout in workoutList)
        {

        }
    }

    private static View BuildHistoryCard(WorkoutRepository workout, int filterNum)
    {
        VerticalStackLayout historyLayout = new VerticalStackLayout()
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
                        UIElements.MakeLabel(Routine.GetRoutineByWorkout(workout.WorkoutId).Name.ToString(), 10),
                        UIElements.MakeLabel(workout.Duration.ToString(), 10),
                        UIElements.MakeLabel(workout.Date.ToString(), 10),
                        UIElements.MakeLabel(workout.Volume.ToString(), 10)
                    }
                },
                UIElements.MakeLine(2, Colors.Black),
            }
        };

        List<WorkoutExercisesRepository> exercises = Exercise.GetExercisesByWorkout(workout.WorkoutId);

        for (int i = 0; i <= filterNum; i++)
        {
            historyLayout.Children.Add(new HorizontalStackLayout()
            {
                Spacing = 8,
                Children =
                {
                    UIElements.MakeLabel("x " + exercises[i].Sets + " " + Exercise.GetExerciseById(exercises[i].ExerciseId).ToString, 10)
                }
            });
        }
    }
}
