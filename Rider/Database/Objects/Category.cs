using MAUI_GymApp.Rider.Database.Queries;

namespace MAUI_GymApp.Rider.Database.Objects;

public class Category
{
    public static List<ChartPasser> GetCategoryChartStats()
    {
        string sql = "SELECT ws.sets, c.name FROM workoutexercises ws JOIN exercise e ON ws.exerciseId = e.exerciseId JOIN muscle m ON e.muscleId = m.muscleId JOIN category c ON m.categoryId = c.categoryId";

        return GeneralQueries.GetMultiple(sql, reader => new ChartPasser()
        {
            Sets = reader.GetInt32(0),
            Name = reader.GetString(1)
        });
    }
}

public class CategoryPasser
{
    public int CategoryId { get; set; }
    public string Name { get; set; }
}