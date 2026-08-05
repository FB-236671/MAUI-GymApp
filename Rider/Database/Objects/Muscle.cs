using MAUI_GymApp.Rider.Database.Queries;

namespace MAUI_GymApp.Rider.Database.Objects;

public class Muscle
{
    public static List<ChartPasser> GetMuscleChartStats()
    {
        string sql = "SELECT ws.sets, m.name FROM workoutexercises ws JOIN exercise e ON ws.exerciseId = e.exerciseId JOIN muscle m ON e.muscleId = m.muscleId";

        return GeneralQueries.GetMultiple(sql, reader => new ChartPasser()
        {
            Sets = reader.GetInt32(0),
            Name = reader.GetString(1)
        });
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