using MAUI_GymApp.Rider.Database.Queries;
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
            "equipment (equipmentId integer PRIMARY KEY AUTOINCREMENT, name text UNIQUE, multiplier double)",
            "trophy (exerciseId integer PRIMARY KEY REFERENCES exercise(exerciseId), platinum integer, gold integer, silver integer, bronze integer)",

            "user (userId integer PRIMARY KEY AUTOINCREMENT, name text, age integer, weight integer, sex text)",
            "record (userId integer, exerciseId integer, bestWeight integer, oneRM integer, PRIMARY KEY (userId, exerciseId))",

            "workout (workoutId integer PRIMARY KEY AUTOINCREMENT, userId integer, duration integer, volume integer, date long)",
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

    public static void TablePrePopulation()
    {
        GeneralQueries.PrePopulator("CategoryList.txt", "INSERT OR IGNORE INTO category (name) VALUES (@name)", new []{ "@name" });
        GeneralQueries.PrePopulator("EquipmentList.txt", "INSERT OR IGNORE INTO equipment (name, multiplier) VALUES (@name, @multiplier)", new [] { "@name", "@multiplier" });
        GeneralQueries.PrePopulator("ExerciseList.txt", "INSERT OR IGNORE INTO exercise (name, muscleId) VALUES (@name, @muscleId)", new []{ "@name", "@muscleId" });
        GeneralQueries.PrePopulator("MuscleList.txt", "INSERT OR IGNORE INTO muscle (name, categoryId) VALUES (@name, @categoryId)", new [] { "@name", "@categoryId" });
        GeneralQueries.PrePopulator("TrophyList.txt", "INSERT OR IGNORE INTO trophy (exerciseId, platinum, gold, silver, bronze) VALUES (@exerciseId, @platinum, @gold, @silver, @bronze)", new [] { "@exerciseId", "@platinum", "@gold", "@silver", "@bronze" });
    }
}