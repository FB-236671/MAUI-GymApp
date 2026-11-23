using MAUI_GymApp.Rider.Database.Objects;

namespace MAUI_GymApp.Views;

public class WorkoutTab : ContentView
{
    public WorkoutTab(UserRepository user)
    {
        List<WorkoutRepository> workoutList = Workout.GetWorkoutsSince(user.UserId, 0);

        VerticalStackLayout layout = new VerticalStackLayout
        {
            Padding = new Thickness(16, 16, 16, 0),
            Spacing = 8,
            Children = { UIElements.MakeLabel("Workout History", 30) }
        };

        if (workoutList.Count == 0)
        {
            layout.Children.Add(UIElements.MakeLabel("No workouts yet", 16));
        }

        foreach (WorkoutRepository workout in workoutList)
        {
            layout.Children.Add(BuildHistoryCard(workout));
        }

        Content = new ScrollView { Content = layout };
    }

    private static View BuildHistoryCard(WorkoutRepository workout)
    {
        string date = DateTimeOffset.FromUnixTimeSeconds(workout.Date).LocalDateTime.ToString("dd MMM yyyy");
        string duration = TimeSpan.FromSeconds(workout.Duration).ToString(@"hh\:mm\:ss");

        VerticalStackLayout historyLayout = new VerticalStackLayout
        {
            Padding = new Thickness(20, 14),
            Spacing = 12,
            Children =
            {
                UIElements.MakeLabel(date, 18),
                UIElements.MakeLabel("Time: " + duration + "   Volume: " + workout.Volume, 12),
                UIElements.MakeLine()
            }
        };

        foreach (WorkoutExercisesRepository exercise in Workout.GetExercisesByWorkout(workout.WorkoutId))
        {
            if (exercise.Sets == 0) continue;
            historyLayout.Children.Add(UIElements.MakeLabel(exercise.Sets + " x " + Exercise.GetExerciseName(exercise.ExerciseId), 12));
        }

        return UIElements.MakeCard(Colors.LightGray, historyLayout);
    }
}
