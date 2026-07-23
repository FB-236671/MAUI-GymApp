using MAUI_GymApp.Rider.Database.Queries;

namespace MAUI_GymApp.Rider.Database.Objects;

public class User
{
    public static List<UserPasser> GetUsers()
    {
        string sql = "SELECT * FROM user";

        return GeneralQueries.GetAll(sql, reader => new UserPasser
        {
            UserId = reader.GetInt32(0),
            Name = reader.GetString(1),
            Age = reader.GetInt32(2),
            Weight = reader.GetInt32(3),
            Sex = reader.GetString(4)
        });
    }
    
    public static int CreateUser(UserPasser userObject)
    {
        string sql = "INSERT INTO user (Name, Age, Weight, Sex) VALUES (@Name, @Age, @Weight, @Sex); SELECT last_insert_rowid();";

        return GeneralQueries.GetOne(sql, reader => reader.GetInt32(0), command =>
        {
            command.Parameters.AddWithValue("@Name", userObject.Name);
            command.Parameters.AddWithValue("@Age", userObject.Age);
            command.Parameters.AddWithValue("@Weight", userObject.Weight);
            command.Parameters.AddWithValue("@Sex", userObject.Sex);
        });
    }
}

public class UserPasser
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public int Weight { get; set; }
    public string Sex { get; set; }
}