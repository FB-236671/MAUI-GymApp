using Microsoft.Data.Sqlite;

namespace MAUI_GymApp.Database.Passers;

public class Passers {}

public class CategoryPasser
{
    public int CategoryId { get; set; }
    public string Name { get; set; }
}

public class EquipmentPasser
{
    public int EquipmentId { get; set; }
    public string Name { get; set; }
    public int Multiplier { get; set; }
}

public class ExercisePasser
{
    public int ExerciseId { get; set; }
    public string Name { get; set; }
    public int MuscleId { get; set; }
}

public class ExercisesPasser
{
    public int RoutineId { get; set; }
    public int ExerciseId { get; set; }
    public int Sets { get; set; }
    public int Rest { get; set; }
    public int EquipmentId { get; set; }
}

public class MusclePasser
{
    public int MuscleId { get; set; }
    public string Name { get; set; }
    public int CategoruId { get; set; }
}

public class RecordPasser
{
    public int UserId { get; set; }
    public int ExerciseId { get; set; }
    public int BestWeight { get; set; }
    public int OneRM { get; set; }
}

public class RoutinePasser
{
    public int RoutineId { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; }
}

public class SessionPasser
{
    public int SessionId { get; set; }
    public int UserId { get; set; }
    public int Duration { get; set; }
    public int Volume { get; set; }
}

public class UserPasser
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public int Weight { get; set; }
    public string Sex { get; set; }
}

public class WorkoutPasser
{
    public int SessionId { get; set; }
    public int ExerciseId { get; set; }
    public int Weight { get; set; }
    public int Sets { get; set; }
    public int Reps { get; set; }
}