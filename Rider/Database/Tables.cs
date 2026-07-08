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
            "category (categoryId integer primary key autoincrement, name text UNIQUE)",
            "exercise (exerciseId integer primary key autoincrement, name text UNIQUE, muscleId integer)",
            "muscle (muscleId integer primary key autoincrement, name text UNIQUE, categoryId integer)",
            "equipment (equipmentId integer primary key autoincrement, name text UNIQUE, multiplier integer)",

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

    public static void CategoryPrepopulation()
    {
        string[] categories = new[]
        {
            "Arms", "Legs", "Back", "Chest", "Abs"
        };
        
        Connection.Open();
        foreach (string category in categories)
        {
            string query = "INSERT OR IGNORE INTO category (name) VALUES (@name)";
            SqliteCommand command = new SqliteCommand(query, Connection);
            command.Parameters.AddWithValue("@name", category);
            command.ExecuteNonQuery();
        }
        Connection.Close();
    }

    public static void ExercisePrepopulation()
    {
        string[,] exercises = new string[,]
        {
            { "Bicep Curl", "1" },
            { "Chest Press", "14" },
            { "Chest Flys", "14" },
            { "Lat Pulldown", "5" },
            {"Shoulder Press", "6"},
            {"Shrug", "8"},
            {"Wrist Curl", "3"},
            {"Lat Raises", "6"},
            {"Squat", "9"},
            {"Leg Curl", "10"},
            {"Leg Extension", "9"},
            {"Crunch", "13"},
            {"Leg Raises", "13"},
            {"Calf Raises", "4"},
            {"Tricep Extension", "2"},
            {"Row", "12"},
            {"Hip Thrust", "7"},
            {"Back Extension", "11"},
            {"Deadlift", "7"},
            {"Tricep Dip", "2"},
            {"Pullover", "5"},
            {"Pull Up", "5"},
            {"Chin Up", "1"},
            {"Reverse Curl", "3"},
            {"Bench Press", "14"}
        };
        
        Connection.Open();
        for (int i = 0; i < exercises.GetLength(0); i++)
        {
            string query = "INSERT OR IGNORE INTO exercise (name, muscleId) VALUES (@name, @muscleId)";
            SqliteCommand command = new SqliteCommand(query, Connection);
            command.Parameters.AddWithValue("@name", exercises[i,0]);
            command.Parameters.AddWithValue("@muscleId", exercises[i,1]);
            command.ExecuteNonQuery();
        }
        Connection.Close();
    }

    public static void MusclePrepopulation()
    {
        string[,] muscles = new string[,]
        {
            { "Bicep", "1" },
            { "Tricep", "1" },
            { "Forearm", "1" },
            { "Calf", "2" },
            {"Lats", "3"},
            {"Shoulders", "1"},
            {"Glutes", "2"},
            {"Traps", "3"},
            {"Quadriceps", "2"},
            {"Hamstrings", "2"},
            {"Lower Back", "3"},
            {"Upper Back", "3"},
            {"Abs", "5"},
            {"Chest", "4"}
        };
        
        Connection.Open();
        for (int i = 0; i < muscles.GetLength(0); i++)
        {
            string query = "INSERT OR IGNORE INTO muscle (name, categoryId) VALUES (@name, @categoryId)";
            SqliteCommand command = new SqliteCommand(query, Connection);
            command.Parameters.AddWithValue("@name", muscles[i, 0]);
            command.Parameters.AddWithValue("@categoryId", muscles[i, 1]);
            command.ExecuteNonQuery();
        }
        Connection.Close();
    }

    public static void EquipmentPrepopulation()
    {
        string[,] equipment = new string[,]
        {
            {"Dumbell", "2"},
            {"Barbell", "1"},
            {"No Pulley", "1"},
            {"One Pulley", "0.5"},
            {"Two Pulley", "0.25"},
            {"Smithing Machine", "0.75"}
        };
        
        Connection.Open();
        for (int i = 0; i < equipment.GetLength(0); i++)
        {
            string query = "INSERT OR IGNORE INTO equipment (name, multiplier) VALUES (@name, @multiplier)";
            SqliteCommand command = new SqliteCommand(query, Connection);
            command.Parameters.AddWithValue("@name", equipment[i, 0]);
            command.Parameters.AddWithValue("@multiplier", equipment[i, 1]);
            command.ExecuteNonQuery();
        }
        Connection.Close();
    }
}