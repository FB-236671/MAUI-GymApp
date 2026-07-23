using MAUI_GymApp.Rider.Database.Queries;

namespace MAUI_GymApp.Rider.Database.Objects;

public class RoutineExercises
{
    public static void AddRoutineExercises(RoutineExercisesPasser routineExercisesObject)
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
        string sql = "DELETE FROM routine WHERE routineId = @routineId";
        
        GeneralQueries.NonQuery(sql, command => command.Parameters.AddWithValue("@routineId", routineId));
    }
}

public class RoutineExercisesPasser
{
    public int RoutineId { get; set; }
    public int ExerciseId { get; set; }
    public int Sets { get; set; }
    public int Rest { get; set; }
    public int EquipmentId { get; set; }
}