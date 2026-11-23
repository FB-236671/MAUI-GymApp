using MAUI_GymApp.Rider.Database.Queries;

namespace MAUI_GymApp.Rider.Database.Objects;

public class Workout
{
    public static void SaveWorkout(WorkoutRepository workoutObject, List<WorkoutExercisesRepository> workoutExercisesList, List<List<WorkoutSetsRepository>> workoutSetsListList)
    {
        string workoutSql = "INSERT INTO workout (userId, duration, volume, date) VALUES (@UserId, @Duration, @Volume, @Date); SELECT last_insert_rowid();";

        int workoutId = GeneralQueries.GetOne(workoutSql, reader => reader.GetInt32(0), command =>
        {
            command.Parameters.AddWithValue("@UserId", workoutObject.UserId);
            command.Parameters.AddWithValue("@Duration", workoutObject.Duration);
            command.Parameters.AddWithValue("@Volume", workoutObject.Volume);
            command.Parameters.AddWithValue("@Date", workoutObject.Date);
        });

        string exercisesSql = "INSERT INTO workoutexercises (workoutId, exerciseId, sets) VALUES (@WorkoutId, @ExerciseId, @Sets); SELECT last_insert_rowid();";

        List<int> workoutExercisesIdList = new List<int>();
        foreach (WorkoutExercisesRepository workoutExercisesObject in workoutExercisesList)
        {
            int workoutExercisesId = GeneralQueries.GetOne(exercisesSql, reader => reader.GetInt32(0), command =>
            {
                command.Parameters.AddWithValue("@WorkoutId", workoutId);
                command.Parameters.AddWithValue("@ExerciseId", workoutExercisesObject.ExerciseId);
                command.Parameters.AddWithValue("@Sets", workoutExercisesObject.Sets);
            });
            workoutExercisesIdList.Add(workoutExercisesId);
        }

        string setsSql = "INSERT INTO workoutsets (workoutexercisesId, setnumber, weight, reps) VALUES (@WorkoutExercisesId, @SetNumber, @Weight, @Reps)";

        for (int exerciseCount = 0; exerciseCount < workoutSetsListList.Count; exerciseCount++)
        {
            List<WorkoutSetsRepository> workoutSetsList = workoutSetsListList[exerciseCount];
            int workoutExercisesId = workoutExercisesIdList[exerciseCount];

            foreach (WorkoutSetsRepository workoutSetsObject in workoutSetsList)
            {
                GeneralQueries.NonQuery(setsSql, command =>
                {
                    command.Parameters.AddWithValue("@WorkoutExercisesId", workoutExercisesId);
                    command.Parameters.AddWithValue("@SetNumber", workoutSetsObject.SetNumber);
                    command.Parameters.AddWithValue("@Weight", workoutSetsObject.Weight);
                    command.Parameters.AddWithValue("@Reps", workoutSetsObject.Reps);
                });
            }
        }
    }

    public static List<WorkoutRepository> GetWorkoutsSince(int userId, long dateTo)
    {
        string sql = "SELECT * FROM workout WHERE userId = @userId AND date > @dateTo";

        return GeneralQueries.GetMultiple(sql, reader => new WorkoutRepository
            {
                WorkoutId = reader.GetInt32(0),
                UserId = reader.GetInt32(1),
                Duration = reader.GetInt32(2),
                Volume = reader.GetInt32(3),
                Date = reader.GetInt64(4)
            },
            command =>
            {
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@dateTo", dateTo);
            }
        );
    }
    
    public static List<WorkoutExercisesRepository> GetExercisesByWorkout(int workoutId)
    {
        string sql = "SELECT * FROM workoutexercises WHERE workoutId = @workoutId";

        return GeneralQueries.GetMultiple(sql, reader => new WorkoutExercisesRepository()
            {
                WorkoutExercisesId = reader.GetInt32(0),
                WorkoutId = reader.GetInt32(1),
                ExerciseId = reader.GetInt32(2),
                Sets = reader.GetInt32(3)
            },
            command => command.Parameters.AddWithValue("@workoutId", workoutId)
        );
    }
}

public class WorkoutRepository
{
    public int WorkoutId { get; set; }
    public int UserId { get; set; }
    public int Duration { get; set; }
    public int Volume { get; set; }
    public long Date {get; set; }
}

public class WorkoutExercisesRepository
{
    public int WorkoutExercisesId { get; set; }
    public int WorkoutId { get; set; }
    public int ExerciseId { get; set; }
    public int Sets { get; set; }
}

public class WorkoutSetsRepository
{
    public int WorkoutExercisesId { get; set; }
    public int SetNumber { get; set; }
    public double Weight { get; set; }
    public int Reps { get; set; }
}
