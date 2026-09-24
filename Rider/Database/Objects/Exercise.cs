using MAUI_GymApp.Rider.Database.Queries;

namespace MAUI_GymApp.Rider.Database.Objects;

public class Exercise
{
    public static List<ExercisePasser> GetExercises()
    {
        string sql = "SELECT * FROM exercise";
        
        return GeneralQueries.GetAll(sql, reader => new ExercisePasser()
        {
            ExerciseId = reader.GetInt32(0),
            Name = reader.GetString(1),
            MuscleId = reader.GetInt32(2)
        });
    }

    public static ExercisePasser GetExerciseByName(string name)
    {
        string sql = "SELECT * FROM exercise WHERE name = @name";
        
        return GeneralQueries.GetOne(sql, reader => new ExercisePasser()
            {
                ExerciseId = reader.GetInt32(0),
                Name = reader.GetString(1),
                MuscleId = reader.GetInt32(2),
            },
            command => command.Parameters.AddWithValue("@name", name)
        );
    }

    public static ExercisePasser GetExerciseById(int id)
    {
        string sql = "SELECT * FROM exercise WHERE exerciseId = @exerciseId";
        
        return GeneralQueries.GetOne(sql, reader => new ExercisePasser()
            {
                ExerciseId = reader.GetInt32(0),
                Name = reader.GetString(1),
                MuscleId = reader.GetInt32(2),
            },
            command => command.Parameters.AddWithValue("@exerciseId", id)
        );
    }

    public static string GetExerciseName(int id)
    {
        string sql = "SELECT name FROM exercise WHERE exerciseId = @exerciseId";
        
        return GeneralQueries.GetOne(sql, reader => reader.GetString(0),
            command => command.Parameters.AddWithValue("@exerciseId", id));
    }
    
    
    public static List<string> ExerciseNameList()
    {
        List<string> exerciseList = new List<string>();
        List<ExercisePasser> passerList = GetExercises();
        
        foreach (ExercisePasser passer in passerList)
        {
            exerciseList.Add(passer.Name);
        }
        return exerciseList;
    }
}

public class ExercisePasser
{
    public int ExerciseId { get; set; }
    public string Name { get; set; }
    public int MuscleId { get; set; }
}