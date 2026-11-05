using MAUI_GymApp.Rider.Database.Queries;

namespace MAUI_GymApp.Rider.Database.Objects;

public class Equipment
{
    public static List<EquipmentParser> GetEquipment()
    {
        string sql = "SELECT * FROM equipment";

        return GeneralQueries.GetMultiple(sql, reader => new EquipmentParser
        {
            EquipmentId = reader.GetInt32(0),
            Name = reader.GetString(1),
            Multiplier = reader.GetDouble(2)
        });
    }
    
    public static EquipmentParser GetEquipmentByName(string name)
    {
        string sql = "SELECT * FROM equipment WHERE name = @name";
        
        return GeneralQueries.GetOne(sql, reader => new EquipmentParser
            {
                EquipmentId = reader.GetInt32(0),
                Name = reader.GetString(1),
                Multiplier = reader.GetDouble(2)
            },
            command => command.Parameters.AddWithValue("@name", name)
        );
    }

    public static EquipmentParser GetEquipmentById(int id)
    {
        string sql = "SELECT * FROM equipment WHERE equipmentId = @equipmentId";
        
        return GeneralQueries.GetOne(sql, reader => new EquipmentParser
            {
                EquipmentId = reader.GetInt32(0),
                Name = reader.GetString(1),
                Multiplier = reader.GetDouble(2)
            },
            command => command.Parameters.AddWithValue("@equipmentId", id)
        );
    }
    
    
    public static List<string> EquipmentNameList()
    {
        List<string> equipmentList = new List<string>();
        List<EquipmentParser> passerList = GetEquipment();

        foreach (EquipmentParser passer in passerList)
        {
            equipmentList.Add(passer.Name);
        }

        return equipmentList;
    }
}

public class EquipmentParser
{
    public int EquipmentId { get; set; }
    public string Name { get; set; }
    public double Multiplier { get; set; }
}