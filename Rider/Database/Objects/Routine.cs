using MAUI_GymApp.Rider.Database.Queries;

namespace MAUI_GymApp.Rider.Database.Objects;

public class Routine
{
    public static int CreateRoutine(RoutineRepository routineObject)
    {
        string sql = "INSERT INTO routine (UserId, Name) VALUES (@UserId, @Name); SELECT last_insert_rowid();";
        
        return GeneralQueries.GetOne(sql, reader => reader.GetInt32(0), command => 
        {
            command.Parameters.AddWithValue("@UserId", routineObject.UserId);
            command.Parameters.AddWithValue("@Name", routineObject.Name);
        });
    }

    public static List<RoutineRepository> GetRoutinesByUser(int userId)
    {
        string sql = "SELECT * FROM routine WHERE userId = @userId";

        return GeneralQueries.GetMultiple(sql, reader => new RoutineRepository()
            {
                RoutineId = reader.GetInt32(0),
                UserId = reader.GetInt32(1),
                Name = reader.GetString(2)
            },
            command => command.Parameters.AddWithValue("@userId", userId)
        );
    }

    public static List<RoutineRepository> GetRoutineByWorkout(int workoutId)
    {
        string sql = "SELECT * FROM routine WHERE workoutId = @workoutId";

        return GeneralQueries.GetOne(sql, reader => new RoutineRepository()
            {
                RoutineId = reader.GetInt32(0),
                UserId = reader.GetInt32(1),
                Name = reader.GetString(2)
            },
            command => command.Parameters.AddWithValue("@workoutId", workoutId)
        );
    }

    public static void DeleteRoutine(int routineId)
    {
        string sql = "DELETE FROM routine WHERE routineId = @routineId";
        
        GeneralQueries.NonQuery(sql, command => command.Parameters.AddWithValue("@routineId", routineId));
    }

    public static void AddRoutineExercises(RoutineExercisesRepository routineExercisesObject)
    {
        string sql = "INSERT INTO routineexercises (RoutineId, ExerciseId, Sets, Rest, EquipmentId) VALUES (@RoutineId, @ExerciseId, @Sets, @Rest, @EquipmentId)";

        GeneralQueries.NonQuery(sql, command =>
        {
            command.Parameters.AddWithValue("@RoutineId", routineExercisesObject.RoutineId);
            command.Parameters.AddWithValue("@ExerciseId", routineExercisesObject.ExerciseId);
            command.Parameters.AddWithValue("@Sets", routineExercisesObject.Sets);
            command.Parameters.AddWithValue("@Rest", routineExercisesObject.Rest);
            command.Parameters.AddWithValue("@EquipmentId", routineExercisesObject.EquipmentId);
        });
    }

    public static void DeleteRoutineExercises(int routineId)
    {
        string sql = "DELETE FROM routineexercises WHERE routineId = @routineId";

        GeneralQueries.NonQuery(sql, command => command.Parameters.AddWithValue("@routineId", routineId));
    }

    public static List<RoutineExercisesRepository> GetRoutineExercisesByRoutineId(int routineId)
    {
        string sql = "SELECT * FROM routineexercises WHERE routineId = @routineId";

        return GeneralQueries.GetMultiple(sql, reader => new RoutineExercisesRepository()
        {
            RoutineId = reader.GetInt32(0),
            ExerciseId = reader.GetInt32(1),
            Sets = reader.GetInt32(2),
            Rest = reader.GetInt32(3),
            EquipmentId = reader.GetInt32(4)
        },
            command => command.Parameters.AddWithValue("@routineId", routineId)
        );
    }
}

public class RoutineRepository
{
    public int RoutineId { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; }
}

public class RoutineExercisesRepository
{
    public int RoutineId { get; set; }
    public int ExerciseId { get; set; }
    public int Sets { get; set; }
    public int Rest { get; set; }
    public int EquipmentId { get; set; }
}