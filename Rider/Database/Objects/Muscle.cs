using MAUI_GymApp.Rider.Database.Queries;

namespace MAUI_GymApp.Rider.Database.Objects;

public class Muscle
{
    public static List<ChartPasser> GetMuscleChartStats(int userId)
    {
        string sql = "SELECT ws.sets, m.name FROM workoutexercises ws JOIN workout w ON ws.workoutId = w.workoutId JOIN exercise e ON ws.exerciseId = e.exerciseId JOIN muscle m ON e.muscleId = m.muscleId WHERE w.userId = @userId";

        return GeneralQueries.GetMultiple(sql, reader => new ChartPasser()
            {
                Sets = reader.GetInt32(0),
                Name = reader.GetString(1)
            },
            command => command.Parameters.AddWithValue("@userId", userId)
        );
    }
}

public class MusclePasser
{
    public int MuscleId { get; set; }
    public string Name { get; set; }
    public int CategoruId { get; set; }
}

public class ChartPasser
{
    public string Name { get; set; }
    public int Sets { get; set; }
}