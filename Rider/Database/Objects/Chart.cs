using MAUI_GymApp.Rider.Database.Queries;
using Microcharts;
using Microcharts.Maui;

namespace MAUI_GymApp.Rider.Database.Objects;

public class Chart
{
    public static List<ChartParser> GetMuscleChartStats(int userId)
    {
        string sql = "SELECT ws.sets, m.name FROM workoutexercises ws JOIN workout w ON ws.workoutId = w.workoutId JOIN exercise e ON ws.exerciseId = e.exerciseId JOIN muscle m ON e.muscleId = m.muscleId WHERE w.userId = @userId";

        return GeneralQueries.GetMultiple(sql, reader => new ChartParser()
            {
                Value = reader.GetInt32(0),
                Name = reader.GetString(1)
            },
            command => command.Parameters.AddWithValue("@userId", userId)
        );
    }
    
    public static List<ChartParser> GetCategoryChartStats(int userId)
    {
        string sql = "SELECT ws.sets, c.name FROM workoutexercises ws JOIN workout w ON ws.workoutId = w.workoutId JOIN exercise e ON ws.exerciseId = e.exerciseId JOIN muscle m ON e.muscleId = m.muscleId JOIN category c ON m.categoryId = c.categoryId WHERE w.userId = @userId";

        return GeneralQueries.GetMultiple(sql, reader => new ChartParser()
            {
                Value = reader.GetInt32(0),
                Name = reader.GetString(1)
            },
            command => command.Parameters.AddWithValue("@userId", userId)
        );
    }
    
    public class ChartParser
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }
}