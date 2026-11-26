using MAUI_GymApp.Rider.Database.Queries;

namespace MAUI_GymApp.Rider.Database.Objects;

public class Chart
{
    public static List<ChartRepository> GetMuscleChartStats(int userId)
    {
        string sql = "SELECT ws.sets, m.name FROM workoutexercises ws JOIN workout w ON ws.workoutId = w.workoutId JOIN exercise e ON ws.exerciseId = e.exerciseId JOIN muscle m ON e.muscleId = m.muscleId WHERE w.userId = @userId";

        return GeneralQueries.GetMultiple(sql, reader => new ChartRepository()
            {
                Value = reader.GetInt32(0),
                Name = reader.GetString(1)
            },
            command => command.Parameters.AddWithValue("@userId", userId)
        );
    }
    
    public static List<ChartRepository> GetCategoryChartStats(int userId)
    {
        string sql = "SELECT ws.sets, c.name FROM workoutexercises ws JOIN workout w ON ws.workoutId = w.workoutId JOIN exercise e ON ws.exerciseId = e.exerciseId JOIN muscle m ON e.muscleId = m.muscleId JOIN category c ON m.categoryId = c.categoryId WHERE w.userId = @userId";

        return GeneralQueries.GetMultiple(sql, reader => new ChartRepository()
            {
                Value = reader.GetInt32(0),
                Name = reader.GetString(1)
            },
            command => command.Parameters.AddWithValue("@userId", userId)
        );
    }


    // NEED TO DO

    public static List<ExerciseStatsRepository> GetExerciseStats(int exerciseId)
    {
        string sql = "";

        return GeneralQueries.GetMultiple(sql, reader => new ExerciseStatsRepository()
        {
            ExerciseId = exerciseId,
            Date = reader.GetInt32(0),
            Weight = reader.GetInt32(1),
            OneRM = reader.GetInt32(2)
        },
        command => command.Parameters.AddWithValue("@exerciseId", exerciseId));
    }

    public class ChartRepository
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }

    public class ExerciseStatsRepository
    {
        public int ExerciseId { get; set; }
        public long Date { get; set; }
        public double Weight { get; set; }
        public int OneRM { get; set; }
    }
}