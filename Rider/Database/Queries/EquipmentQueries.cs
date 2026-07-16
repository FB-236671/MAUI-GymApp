using MAUI_GymApp.Database;
using MAUI_GymApp.Database.Passers;
using Microsoft.Data.Sqlite;

namespace MAUI_GymApp.Rider.Database.Queries;

public class EquipmentQueries
{
    public static List<EquipmentPasser> GetEquipment()
    {
        List<EquipmentPasser> equipmentList = new List<EquipmentPasser>();
        string sql = "SELECT * FROM equipment";
        SqliteCommand command = new SqliteCommand(sql, Tables.Connection);
        Tables.Connection.Open();
        SqliteDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            EquipmentPasser equipmentObject = new EquipmentPasser();
            equipmentObject.EquipmentId = reader.GetInt32(0);
            equipmentObject.Name = reader.GetString(1);
            equipmentObject.Multiplier = reader.GetInt32(2);
            equipmentList.Add(equipmentObject);
        }
        Tables.Connection.Close();
        return equipmentList;
    }

    public static EquipmentPasser GetEquipmentByName(string name)
    {
        EquipmentPasser equipmentObject = new EquipmentPasser();
        string sql = "SELECT * FROM equipment WHERE name = @name";
        SqliteCommand command = new SqliteCommand(sql, Tables.Connection);
        command.Parameters.AddWithValue("@name", name);
        Tables.Connection.Open();
        SqliteDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            equipmentObject.EquipmentId = reader.GetInt32(0);
            equipmentObject.Name = reader.GetString(1);
            equipmentObject.Multiplier = reader.GetInt32(2);
        }
        Tables.Connection.Close();
        return equipmentObject;
    }
}