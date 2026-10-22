using MAUI_GymApp.Rider.Database.Queries;

namespace MAUI_GymApp.Rider.Database.Objects;

public class Category
{
    public static List<ChartPasser> GetCategoryChartStats(int userId)
    {
        string sql = "SELECT ws.sets, c.name FROM workoutexercises ws JOIN workout w ON ws.workoutId = w.workoutId JOIN exercise e ON ws.exerciseId = e.exerciseId JOIN muscle m ON e.muscleId = m.muscleId JOIN category c ON m.categoryId = c.categoryId WHERE w.userId = @userId";

        return GeneralQueries.GetMultiple(sql, reader => new ChartPasser()
            {
                Sets = reader.GetInt32(0),
                Name = reader.GetString(1)
            },
            command => command.Parameters.AddWithValue("@userId", userId)
        );
    }
}