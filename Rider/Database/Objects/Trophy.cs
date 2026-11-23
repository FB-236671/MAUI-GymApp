using MAUI_GymApp.Rider.Database.Queries;

namespace MAUI_GymApp.Rider.Database.Objects;

public class Trophy
{
    public static TrophyRepository GetTrophyByExercise(int exerciseId)
    {
        string sql = "SELECT * FROM trophy WHERE exerciseId = @ExerciseId";

        return GeneralQueries.GetOne(sql, reader => new TrophyRepository()
            {
                ExerciseId = reader.GetInt32(0),
                Platinum = reader.GetInt32(1),
                Gold = reader.GetInt32(2),
                Silver = reader.GetInt32(3),
                Bronze = reader.GetInt32(4),
            },
            command =>
            {
                command.Parameters.AddWithValue("@ExerciseId", exerciseId);
            }
        );
    }
}

public class TrophyRepository
{
    public int ExerciseId { get; set; }
    public int Platinum { get; set; }
    public int Gold { get; set; }
    public int Silver { get; set; }
    public int Bronze { get; set; }
}