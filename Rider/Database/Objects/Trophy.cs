using MAUI_GymApp.Rider.Database.Queries;

namespace MAUI_GymApp.Rider.Database.Objects;

public class Trophy
{
    public static TrophyPasser GetTrophyByExercise(int exerciseId)
    {
        string sql = "SELECT * FROM trophy WHERE ExerciseId = @ExerciseId";

        return GeneralQueries.GetOne(sql, reader => new TrophyPasser()
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
    
    public static RecordPasser GetRecordById(int userId, int exerciseId)
    {
        string sql = "SELECT * FROM record WHERE UserId = @UserId AND ExerciseId = @ExerciseId";

        return GeneralQueries.GetOne(sql, reader => new RecordPasser
            {
                UserId = reader.GetInt32(0),
                ExerciseId = reader.GetInt32(1),
                BestWeight = reader.GetInt32(2),
                OneRM = reader.GetInt32(3)
            },
            command =>
            {
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@ExerciseId", exerciseId);
            }
        );
    }
}

public class TrophyPasser
{
    public int ExerciseId { get; set; }
    public int Platinum { get; set; }
    public int Gold { get; set; }
    public int Silver { get; set; }
    public int Bronze { get; set; }
}