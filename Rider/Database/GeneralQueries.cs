using MAUI_GymApp.Database;
using Microsoft.Data.Sqlite;

namespace MAUI_GymApp.Rider.Database.Queries;

public class GeneralQueries
{ 
    public static List<T> GetAll<T>(string sql, Func<SqliteDataReader, T> mapItem)
    {
        List<T> list = new List<T>();
        using SqliteCommand command = new SqliteCommand(sql, Tables.Connection);
        Tables.Connection.Open();
        try
        {
            using SqliteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                T obj = mapItem(reader);
                list.Add(obj);
            }
        }
        finally
        {
            Tables.Connection.Close();
        }
        return list;
    }
    
    public static void NonQuery(string sql, Action<SqliteCommand> setParameters = null)
    {
        using SqliteCommand command = new SqliteCommand(sql, Tables.Connection);
        if (setParameters != null) { setParameters(command); }
        Tables.Connection.Open();
        try
        {
            command.ExecuteNonQuery();
        }
        finally
        {
            Tables.Connection.Close();
        }
    }
    
    public static T GetOne<T>(string sql, Func<SqliteDataReader, T> mapItem, Action<SqliteCommand> setParameters = null)
    {
        using SqliteCommand command = new SqliteCommand(sql, Tables.Connection);
        if (setParameters != null) { setParameters(command); }
        Tables.Connection.Open();
        try
        {
            using SqliteDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                return mapItem(reader);
            }
        }
        finally
        {
            Tables.Connection.Close();
        }
        return default;
    }

    public static List<T> GetMultiple<T>(string sql, Func<SqliteDataReader, T> mapItem, Action<SqliteCommand> setParameters = null)
    {
        List<T> list = new List<T>();
        using SqliteCommand command = new SqliteCommand(sql, Tables.Connection);
        if (setParameters != null) { setParameters(command); }
        Tables.Connection.Open();
        try
        {
            using SqliteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                T obj = mapItem(reader);
                list.Add(obj);
            }
        }
        finally
        {
            Tables.Connection.Close();
        }
        return list;
    }
    
    public static void PrePopulator(string fileName, string query, string[] fields)
    {
        Stream stream = FileSystem.OpenAppPackageFileAsync(fileName).GetAwaiter().GetResult();
        StreamReader reader = new StreamReader(stream);
        Tables.Connection.Open();
        try
        {
            string line = reader.ReadLine();
            while (line != null)
            {
                string[] words = line.Split(',');
                if (words.Length == fields.Length)
                {
                    using SqliteCommand command = new SqliteCommand(query, Tables.Connection);
                    for (int i = 0; i < fields.Length; i++)
                        command.Parameters.AddWithValue(fields[i], words[i]);
                    command.ExecuteNonQuery();
                }

                line = reader.ReadLine();
            }
        }
        finally
        {
            stream.Close();
            reader.Close();
            Tables.Connection.Close();
        }
    }
}