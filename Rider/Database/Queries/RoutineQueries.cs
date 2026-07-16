using MAUI_GymApp.Database;
using MAUI_GymApp.Database.Passers;
using Microsoft.Data.Sqlite;

namespace MAUI_GymApp.Rider.Database.Queries;

public class RoutineQueries
{
    public static void CreateRoutine(RoutinePasser routineObject)
    {
        string sql = "INSERT INTO routine (UserId, Name) VALUES (@UserId, @Name)";
        SqliteCommand command = new SqliteCommand(sql, Tables.Connection);
        command.Parameters.AddWithValue("@UserId", routineObject.UserId);
        command.Parameters.AddWithValue("@Name", routineObject.Name);
        Tables.Connection.Open();
        command.ExecuteNonQuery();
        Tables.Connection.Close();
    }

    public static RoutinePasser GetRoutineByName(string name)
    {
        RoutinePasser routineObject = new RoutinePasser();
        string sql = "SELECT * FROM routine WHERE name = @name";
        SqliteCommand command = new SqliteCommand(sql, Tables.Connection);
        command.Parameters.AddWithValue("@name", name);
        Tables.Connection.Open();
        SqliteDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            routineObject.RoutineId = reader.GetInt32(0);
            routineObject.UserId = reader.GetInt32(1);
            routineObject.Name = reader.GetString(2);
        }
        Tables.Connection.Close();
        return routineObject;
    }
}