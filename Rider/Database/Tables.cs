using Microsoft.Data.Sqlite;

namespace MAUI_GymApp.Database;

public class Tables
{
    private static string _dbPath = Path.Combine(FileSystem.AppDataDirectory, "gymapp.db");
    public static SqliteConnection Connection = new SqliteConnection($"Data Source={_dbPath}");
    
    public static void CreateTables()
    {
        string[] tables = new[]
        {
            "category (categoryId integer primary key autoincrement, name text)",
            "exercise (exerciseId integer primary key autoincrement, name text, muscleId integer, bronzeTrophy integer, silverTrophy integer, goldTrophy integer, platinumTrophy integer)",
            "muscle (muscleId integer primary key autoincrement, name text, categoryId integer)",
            "equipment (equipmentId integer primary key autoincrement, name text, multiplier integer)",

            "user (userId integer primary key autoincrement, name text, age integer, weight integer, sex text)",
            "record (userId integer, exerciseId integer, bestWeight integer, oneRM integer, PRIMARY KEY (userId, exerciseId))",

            "session (sessionId integer primary key autoincrement, userId integer, duration integer, volume integer)",
            "workout (sessionId integer, exerciseId integer, weight integer, sets integer, reps integer, PRIMARY KEY (sessionId, exerciseId))",
            "routine (routineId integer primary key autoincrement, userId integer)",
            "exercises (routineId integer, exerciseId integer, sets integer, rest integer, PRIMARY KEY (routineId, exerciseId))"
        };

        Connection.Open();
        foreach (string table in tables)
        {
            string query = "CREATE TABLE IF NOT EXISTS " + table;
            SqliteCommand command = new SqliteCommand(query, Connection);
            command.ExecuteNonQuery();
        }
        Connection.Close();
    }
}