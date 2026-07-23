using MAUI_GymApp.Rider.Database.Queries;

namespace MAUI_GymApp.Rider.Database.Objects;

public class Routine
{
    public static void CreateRoutine(RoutinePasser routineObject)
    {
        string sql = "INSERT INTO routine (UserId, Name) VALUES (@UserId, @Name)";
        
        GeneralQueries.NonQuery(sql, command => 
        {
            command.Parameters.AddWithValue("@UserId", routineObject.UserId);
            command.Parameters.AddWithValue("@Name", routineObject.Name);
        });
    }

    public static List<RoutinePasser> GetRoutinesByUser(int userId)
    {
        string sql = "SELECT * FROM routine WHERE userId = @userId";

        return GeneralQueries.GetMultiple(sql, reader => new RoutinePasser()
            {
                RoutineId = reader.GetInt32(0),
                UserId = reader.GetInt32(1),
                Name = reader.GetString(2)
            },
            command => command.Parameters.AddWithValue("@userId", userId)
        );
    }

    public static RoutinePasser GetRoutineByName(string name)
    {
        string sql = "SELECT * FROM routine WHERE name = @name";
        
        return GeneralQueries.GetOne(sql, reader => new RoutinePasser()
            {
                RoutineId = reader.GetInt32(0),
                UserId = reader.GetInt32(1),
                Name = reader.GetString(2)
            },
            command => command.Parameters.AddWithValue("@name", name)
        );
    }

    public static void DeleteRoutine(int routineId)
    {
        string sql = "DELETE FROM routine WHERE routineId = @routineId";
        
        GeneralQueries.NonQuery(sql, command => command.Parameters.AddWithValue("@routineId", routineId));
    }
}

public class RoutinePasser
{
    public int RoutineId { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; }
}