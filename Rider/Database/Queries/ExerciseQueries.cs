using MAUI_GymApp.Database;
using MAUI_GymApp.Database.Passers;
using Microsoft.Data.Sqlite;

namespace MAUI_GymApp.Rider.Database.Queries;

public class ExerciseQueries
{
    public static List<ExercisePasser> GetExercises()
    {
        List<ExercisePasser> exerciseList = new List<ExercisePasser>();
        string sql = "SELECT * FROM exercise";
        SqliteCommand command = new SqliteCommand(sql, Tables.Connection);
        Tables.Connection.Open();
        SqliteDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            ExercisePasser exerciseObject = new ExercisePasser();
            exerciseObject.ExerciseId = reader.GetInt32(0);
            exerciseObject.Name = reader.GetString(1);
            exerciseObject.MuscleId = reader.GetInt32(2);
            exerciseList.Add(exerciseObject);
        }
        Tables.Connection.Close();
        return exerciseList;
    }

    public static ExercisePasser GetExerciseByName(string name)
    {
        ExercisePasser exerciseObject = new ExercisePasser();
        string sql = "SELECT * FROM exercise WHERE name = @name";
        SqliteCommand command = new SqliteCommand(sql, Tables.Connection);
        command.Parameters.AddWithValue("@name", name);
        Tables.Connection.Open();
        SqliteDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            exerciseObject.ExerciseId = reader.GetInt32(0);
            exerciseObject.Name = reader.GetString(1);
            exerciseObject.MuscleId = reader.GetInt32(2);
        }
        Tables.Connection.Close();
        return exerciseObject;
    }
}