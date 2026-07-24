using MAUI_GymApp.Rider.Database.Queries;

namespace MAUI_GymApp.Rider.Database.Objects;

public class Equipment
{
    public static List<EquipmentPasser> GetEquipment()
    {
        string sql = "SELECT * FROM equipment";

        return GeneralQueries.GetAll(sql, reader => new EquipmentPasser
        {
            EquipmentId = reader.GetInt32(0),
            Name = reader.GetString(1),
            Multiplier = reader.GetInt32(2)
        });
    }
    
    public static EquipmentPasser GetEquipmentByName(string name)
    {
        string sql = "SELECT * FROM equipment WHERE name = @name";
        
        return GeneralQueries.GetOne(sql, reader => new EquipmentPasser
            {
                EquipmentId = reader.GetInt32(0),
                Name = reader.GetString(1),
                Multiplier = reader.GetInt32(2)
            },
            command => command.Parameters.AddWithValue("@name", name)
        );
    }

    public static EquipmentPasser GetEquipmentById(int id)
    {
        string sql = "SELECT * FROM equipment WHERE equipmentId = @equipmentId";
        
        return GeneralQueries.GetOne(sql, reader => new EquipmentPasser
            {
                EquipmentId = reader.GetInt32(0),
                Name = reader.GetString(1),
                Multiplier = reader.GetInt32(2)
            },
            command => command.Parameters.AddWithValue("@equipmentId", id)
        );
    }
}

public class EquipmentPasser
{
    public int EquipmentId { get; set; }
    public string Name { get; set; }
    public int Multiplier { get; set; }
}