using MAUI_GymApp.Rider.Database.Queries;

namespace MAUI_GymApp.Rider.Database.Objects;

public class User
{
    public static List<UserRepository> GetUsers()
    {
        string sql = "SELECT * FROM user";

        return GeneralQueries.GetMultiple(sql, reader => new UserRepository
        {
            UserId = reader.GetInt32(0),
            Name = reader.GetString(1),
            Age = reader.GetInt32(2),
            Weight = reader.GetInt32(3),
            Sex = reader.GetString(4)
        });
    }
    
    public static int CreateUser(UserRepository userObject)
    {
        string sql = "INSERT INTO user (name, age, weight, sex) VALUES (@Name, @Age, @Weight, @Sex); SELECT last_insert_rowid();";

        return GeneralQueries.GetOne(sql, reader => reader.GetInt32(0), command =>
        {
            command.Parameters.AddWithValue("@Name", userObject.Name);
            command.Parameters.AddWithValue("@Age", userObject.Age);
            command.Parameters.AddWithValue("@Weight", userObject.Weight);
            command.Parameters.AddWithValue("@Sex", userObject.Sex);
        });
    }

    public static void EditUser(UserRepository userObject)
    {
        string sql = "UPDATE user SET name = @Name, age = @Age, weight = @Weight, sex = @Sex WHERE userId = @userId";

        GeneralQueries.NonQuery(sql, command =>
        {
            command.Parameters.AddWithValue("@Name", userObject.Name);
            command.Parameters.AddWithValue("@Age", userObject.Age);
            command.Parameters.AddWithValue("@Weight", userObject.Weight);
            command.Parameters.AddWithValue("@Sex", userObject.Sex);
            command.Parameters.AddWithValue("@userId", userObject.UserId);
        });
    }

    public static void DeleteUser(int userId)
    {
        string sql = "DELETE FROM workoutsets WHERE workoutexercisesId IN (SELECT we.workoutexercisesId FROM workoutexercises we JOIN workout w ON we.workoutId = w.workoutId WHERE w.userId = @userId);\n    DELETE FROM workoutexercises WHERE workoutId IN (SELECT workoutId FROM workout WHERE userId = @userId);\n    DELETE FROM workout WHERE userId = @userId;\n    DELETE FROM routineexercises WHERE routineId IN (SELECT routineId FROM routine WHERE userId = @userId);\n    DELETE FROM routine WHERE userId = @userId;\n    DELETE FROM record WHERE userId = @userId;\n    DELETE FROM user WHERE userId = @userId";

        GeneralQueries.NonQuery(sql, command => command.Parameters.AddWithValue("@userId", userId));
    }
}

public class UserRepository
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public int Weight { get; set; }
    public string Sex { get; set; }
}