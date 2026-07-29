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
            "category (categoryId integer PRIMARY KEY AUTOINCREMENT, name text UNIQUE)",
            "exercise (exerciseId integer PRIMARY KEY AUTOINCREMENT, name text UNIQUE, muscleId integer)",
            "muscle (muscleId integer PRIMARY KEY AUTOINCREMENT, name text UNIQUE, categoryId integer)",
            "equipment (equipmentId integer PRIMARY KEY AUTOINCREMENT, name text UNIQUE, multiplier integer)",
            "trophy (exerciseId integer PRIMARY KEY REFERENCES exercise(exerciseId), platinum integer, gold integer, silver integer, bronze integer)",

            "user (userId integer PRIMARY KEY AUTOINCREMENT, name text, age integer, weight integer, sex text)",
            "record (userId integer, exerciseId integer, bestWeight integer, oneRM integer, PRIMARY KEY (userId, exerciseId))",

            "workout (workoutId integer PRIMARY KEY AUTOINCREMENT, userId integer, duration integer, volume integer)",
            "workoutexercises (workoutexercisesId integer PRIMARY KEY AUTOINCREMENT, workoutId integer, exerciseId integer, sets integer)",
            "workoutsets (workoutexercisesId integer, setnumber integer, weight integer, reps integer, PRIMARY KEY (workoutexercisesId, setnumber))",
            "routine (routineId integer PRIMARY KEY AUTOINCREMENT, userId integer, name text)",
            "routineexercises (routineId integer, exerciseId integer, sets integer, rest integer, equipmentId integer, PRIMARY KEY (routineId, exerciseId))"
        };

        Connection.Open();
        foreach (string table in tables)
        {
            string query = "CREATE TABLE IF NOT EXISTS " + table;
            using SqliteCommand command = new SqliteCommand(query, Connection);
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
            using SqliteCommand command = new SqliteCommand(query, Connection);
            command.Parameters.AddWithValue("@name", category);
            command.ExecuteNonQuery();
        }
        Connection.Close();
    }

    public static void ExercisePrepopulation()
    {
        string[,] exercises = new [,]
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
            using SqliteCommand command = new SqliteCommand(query, Connection);
            command.Parameters.AddWithValue("@name", exercises[i,0]);
            command.Parameters.AddWithValue("@muscleId", exercises[i,1]);
            command.ExecuteNonQuery();
        }
        Connection.Close();
    }

    public static void MusclePrepopulation()
    {
        string[,] muscles = new [,]
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
            using SqliteCommand command = new SqliteCommand(query, Connection);
            command.Parameters.AddWithValue("@name", muscles[i, 0]);
            command.Parameters.AddWithValue("@categoryId", muscles[i, 1]);
            command.ExecuteNonQuery();
        }
        Connection.Close();
    }

    public static void EquipmentPrepopulation()
    {
        string[,] equipment = new [,]
        {
            {"No Equipment", "0"},
            {"Dumbbell", "2"},
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
            using SqliteCommand command = new SqliteCommand(query, Connection);
            command.Parameters.AddWithValue("@name", equipment[i, 0]);
            command.Parameters.AddWithValue("@multiplier", equipment[i, 1]);
            command.ExecuteNonQuery();
        }
        Connection.Close();
    }

    public static void TrophyPrepopulation()
    {
        string[,] trophies = new [,]
        {
            {"50", "35", "25", "10"},
            {"20", "15", "10", "5"},
            {"75", "58", "42", "25"},
            {"75", "60", "48", "35"},
            {"50", "40", "30", "20"},
            {"90", "70", "55", "40"},
            {"60", "45", "30", "15"},
            {"30", "21", "13", "6"},
            {"140", "110", "85", "60"},
            {"65", "50", "40", "30"},
            {"90", "75", "50", "40"},
            {"25", "18", "12", "6"},
            {"15", "11", "7", "3"},
            {"110", "90", "70", "50"},
            {"40", "32", "24", "15"},
            {"90", "72", "55", "38"},
            {"140", "110", "85", "60"},
            {"35", "26", "18", "10"},
            {"160", "130", "100", "70"},
            {"40", "28", "16", "5"},
            {"38", "30", "22", "14"},
            {"35", "22", "12", "3"},
            {"40", "25", "15", "5"},
            {"40", "25", "15", "10"},
            {"130", "105", "80", "55"}
        };
        
        Connection.Open();
        for (int i = 0; i < trophies.GetLength(0); i++)
        {
            string query = "INSERT OR IGNORE INTO trophy (exerciseId, platinum, gold, silver, bronze) VALUES (@exerciseId, @platinum, @gold, @silver, @bronze)";
            using SqliteCommand command = new SqliteCommand(query, Connection);
            command.Parameters.AddWithValue("@exerciseId", i + 1);
            command.Parameters.AddWithValue("@platinum", trophies[i, 0]);
            command.Parameters.AddWithValue("@gold", trophies[i, 1]);
            command.Parameters.AddWithValue("@silver", trophies[i, 2]);
            command.Parameters.AddWithValue("@bronze", trophies[i, 3]);
            command.ExecuteNonQuery();
        }
        Connection.Close();
    }
}