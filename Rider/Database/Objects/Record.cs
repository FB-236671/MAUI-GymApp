using MAUI_GymApp.Rider.Database.Queries;

namespace MAUI_GymApp.Rider.Database.Objects;

public class Record
{
    public static void SaveRecord(RecordPasser recordObject)
    {
        string sql = "INSERT OR REPLACE INTO record (UserId, ExerciseId, BestWeight, OneRM) VALUES (@UserId, @ExerciseId, @BestWeight, @OneRM)";
        
        GeneralQueries.NonQuery(sql, command => 
        {
            command.Parameters.AddWithValue("@UserId", recordObject.UserId);
            command.Parameters.AddWithValue("@ExerciseId", recordObject.ExerciseId);
            command.Parameters.AddWithValue("@BestWeight", recordObject.BestWeight);
            command.Parameters.AddWithValue("@OneRM", recordObject.OneRM);
        });
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

    public static List<RecordPasser> GetRecordsByUser(int userId)
    {
        string sql = "SELECT * FROM record WHERE userId = @userId";

        return GeneralQueries.GetMultiple(sql, reader => new RecordPasser()
            {
                UserId = reader.GetInt32(0),
                ExerciseId = reader.GetInt32(1),
                BestWeight = reader.GetInt32(2),
                OneRM = reader.GetInt32(3)
            },
            command => command.Parameters.AddWithValue("@userId", userId)
        );
    }
}

public class RecordPasser
{
    public int UserId { get; set; }
    public int ExerciseId { get; set; }
    public int BestWeight { get; set; }
    public int OneRM { get; set; }
}