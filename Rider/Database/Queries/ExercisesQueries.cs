using MAUI_GymApp.Database;
using MAUI_GymApp.Database.Passers;
using Microsoft.Data.Sqlite;

namespace MAUI_GymApp.Rider.Database.Queries;

public class ExercisesQueries
{
    public static void AddExercises(ExercisesPasser exercisesObject)
    {
        string sql = "INSERT INTO exercises (ExerciseId, Sets, Rest, EquipmentId) VALUES (@ExerciseId, @Sets, @Rest, @EquipmentId)";
        SqliteCommand command = new SqliteCommand(sql, Tables.Connection);
        command.Parameters.AddWithValue("@ExerciseId", exercisesObject.ExerciseId);
        command.Parameters.AddWithValue("@Sets", exercisesObject.Sets);
        command.Parameters.AddWithValue("@Rest", exercisesObject.Rest);
        command.Parameters.AddWithValue("@EquipmentId", exercisesObject.EquipmentId);
        Tables.Connection.Open();
        command.ExecuteNonQuery();
        Tables.Connection.Close();
    }
}