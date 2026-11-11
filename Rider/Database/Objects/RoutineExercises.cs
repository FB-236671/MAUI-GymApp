using MAUI_GymApp.Rider.Database.Queries;

namespace MAUI_GymApp.Rider.Database.Objects;

public class RoutineExercises
{
    public static void AddRoutineExercises(RoutineExercisesParser routineExercisesObject)
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

    public static List<RoutineExercisesParser> GetRoutineExercisesByRoutineId(int routineId)
    {
        string sql = "SELECT * FROM routineexercises WHERE routineId = @routineId";
        
        return GeneralQueries.GetMultiple(sql, reader => new RoutineExercisesParser()
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

public class RoutineExercisesParser
{
    public int RoutineId { get; set; }
    public int ExerciseId { get; set; }
    public int Sets { get; set; }
    public int Rest { get; set; }
    public int EquipmentId { get; set; }
}

public class RoutineDataParser
{
    public string Name { get; set; }
    public Entry SetEntry { get; set; }
    public Picker RestPicker { get; set; }
    public Picker EquipmentPicker { get; set; }
}