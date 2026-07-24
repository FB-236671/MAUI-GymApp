using MAUI_GymApp.Rider.Database.Queries;

namespace MAUI_GymApp.Rider.Database;

public class Workout
{
    public static void SaveWorkout(WorkoutPasser workoutObject)
    {
        string sql = "INSERT INTO workout (UserId, Duration, Volume) VALUES (@UserId, @Duration, @Volume)";
        
        GeneralQueries.NonQuery(sql, command =>
        {
            command.Parameters.AddWithValue("@UserId", workoutObject.UserId);
            command.Parameters.AddWithValue("@Duration", workoutObject.Duration);
            command.Parameters.AddWithValue("@Volume", workoutObject.Volume);
        });
    }
}

public class WorkoutPasser
{
    public int WorkoutId { get; set; }
    public int UserId { get; set; }
    public int Duration { get; set; }
    public int Volume { get; set; }
}