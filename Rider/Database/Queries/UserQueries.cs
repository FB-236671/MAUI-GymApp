using MAUI_GymApp.Database;
using MAUI_GymApp.Database.Passers;
using Microsoft.Data.Sqlite;

namespace MAUI_GymApp.Rider.Database.Queries;

public class UserQueries
{
    public static void CreateUser(UserPasser userObject)
    {
        string sql = "INSERT INTO user (Name, Age, Weight, Sex) VALUES (?, ?, ?)";
        using SqliteCommand command = new SqliteCommand(sql, Tables.Connection);
        command.Parameters.AddWithValue("@Name", userObject.Name);
        command.Parameters.AddWithValue("@Age", userObject.Age);
        command.Parameters.AddWithValue("@Weight", userObject.Weight);
        command.Parameters.AddWithValue("@Sex", userObject.Sex);
        Tables.Connection.Open();
        command.ExecuteNonQuery();
    }
}