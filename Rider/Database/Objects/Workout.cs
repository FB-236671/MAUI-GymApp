using MAUI_GymApp.Rider.Database.Queries;

namespace MAUI_GymApp.Rider.Database;

public class Workout
{ 
    public static void SaveWorkout(WorkoutPasser workoutObject, List<WorkoutExercisesPasser> workoutExercisesList, List<List<WorkoutSetsPasser>> workoutSetsListList)
    {
        string workoutSql = "INSERT INTO workout (UserId, Duration, Volume) VALUES (@UserId, @Duration, @Volume); SELECT last_insert_rowid();";
        
        int workoutId = GeneralQueries.GetOne(workoutSql, reader => reader.GetInt32(0), command =>
        {
            command.Parameters.AddWithValue("@UserId", workoutObject.UserId);
            command.Parameters.AddWithValue("@Duration", workoutObject.Duration);
            command.Parameters.AddWithValue("@Volume", workoutObject.Volume);
        });
        
        string exercisesSql = "INSERT INTO workoutexercises (WorkoutId, ExerciseId, Sets) VALUES (@WorkoutId, @ExerciseId, @Sets); SELECT last_insert_rowid();";

        List<int> workoutExercisesIdList = new List<int>();
        foreach (WorkoutExercisesPasser workoutExercisesObject in workoutExercisesList)
        {
            int workoutExercisesId = GeneralQueries.GetOne(exercisesSql, reader => reader.GetInt32(0), command =>
            {
                command.Parameters.AddWithValue("@WorkoutId", workoutId);
                command.Parameters.AddWithValue("@ExerciseId", workoutExercisesObject.ExerciseId);
                command.Parameters.AddWithValue("@Sets", workoutExercisesObject.Sets);
            });
            workoutExercisesIdList.Add(workoutExercisesId);
        }
        
        string setssql = "INSERT INTO workoutsets (WorkoutExercisesId, SetNumber, Weight, Reps) VALUES (@WorkoutExercisesId, @SetNumber, @Weight, @Reps)";

        for (int exerciseCount = 0; exerciseCount < workoutSetsListList.Count; exerciseCount++)
        {
            List<WorkoutSetsPasser> workoutSetsList = workoutSetsListList[exerciseCount];
            int workoutExercisesId = workoutExercisesIdList[exerciseCount];

            foreach (WorkoutSetsPasser workoutSetsObject in workoutSetsList)
            {
                GeneralQueries.NonQuery(setssql, command =>
                {
                    command.Parameters.AddWithValue("@WorkoutExercisesId", workoutExercisesId);
                    command.Parameters.AddWithValue("@SetNumber", workoutSetsObject.SetNumber);
                    command.Parameters.AddWithValue("@Weight", workoutSetsObject.Weight);
                    command.Parameters.AddWithValue("@Reps", workoutSetsObject.Reps);
                });
            }            
        }
    }
}

public class WorkoutPasser
{
    public int WorkoutId { get; set; }
    public int UserId { get; set; }
    public int Duration { get; set; }
    public int Volume { get; set; }
}

public class WorkoutExercisesPasser
{
    public int WorkoutExercisesId { get; set; }
    public int WorkoutId { get; set; }
    public int ExerciseId { get; set; }
    public int Sets { get; set; }
}

public class WorkoutSetsPasser
{
    public int WorkoutExercisesId { get; set; }
    public int SetNumber { get; set; }
    public int Weight { get; set; }
    public int Reps { get; set; }
}

public class SetPasser
{
    public int SetNumber { get; set; }
    public Entry Weight { get; set; }
    public Entry Reps { get; set; }
    public CheckBox CheckBox { get; set; }
    public double EquipmentMultiplier { get; set; }
}