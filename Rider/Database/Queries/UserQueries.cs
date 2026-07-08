using MAUI_GymApp.Database;
using MAUI_GymApp.Database.Passers;
using Microsoft.Data.Sqlite;

namespace MAUI_GymApp.Rider.Database.Queries;

public class UserQueries
{
    public static void CreateUser(UserPasser userObject)
    {
        string sql = "INSERT INTO user (Name, Age, Weight, Sex) VALUES (@Name, @Age, @Weight, @Sex)";
        using SqliteCommand command = new SqliteCommand(sql, Tables.Connection);
        command.Parameters.AddWithValue("@Name", userObject.Name);
        command.Parameters.AddWithValue("@Age", userObject.Age);
        command.Parameters.AddWithValue("@Weight", userObject.Weight);
        command.Parameters.AddWithValue("@Sex", userObject.Sex);
        Tables.Connection.Open();
        command.ExecuteNonQuery();
        Tables.Connection.Close();
    }

    public static List<UserPasser> GetUsers()
    {
        List<UserPasser> userList = new List<UserPasser>();
        string sql = "SELECT * FROM user";
        using SqliteCommand command = new SqliteCommand(sql, Tables.Connection);
        Tables.Connection.Open();
        using SqliteDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            UserPasser userObject = new UserPasser();
            userObject.UserId = reader.GetInt32(0);
            userObject.Name = reader.GetString(1);
            userObject.Age = reader.GetInt32(2);
            userObject.Weight = reader.GetInt32(3);
            userObject.Sex = reader.GetString(4);
            userList.Add(userObject);
        }
        Tables.Connection.Close();
        return userList;
    }
}