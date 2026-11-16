using MAUI_GymApp.Rider.Database.Objects;

namespace MAUI_GymApp.Views;

public class WorkoutTab : ContentView
{
    private List<WorkoutRepository> workoutList = ;
    private List<string> filterList = new List<string>();

    public TrophiesTab()
    {
        VerticalStackLayout layout = new VerticalStackLayout
        {
            Padding = new Thickness(16, 16, 16, 0),
            Spacing = 8,
            Children = 
            { 
                UIElements.MakeLabel("Workout History", 30),
                UIElements.MakeSelector("Filter", filterList, 20)
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
                        UIElements.MakeLabel(Routine.GetRoutineFromWorkout(workout.WorkoutId).Name, 22)
                    }
                },
                UIElements.MakeLine(2, Colors.Black),
            }
        };

        for (int i = 0; i <= filterNum; i++)
        {
            historyLayout.Children.Add(new HorizontalStackLayout()
            {
                Spacing = 8,
                Children =
                {

                }
            });
        }
    }
}
