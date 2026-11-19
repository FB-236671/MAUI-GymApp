namespace MAUI_GymApp;

public class SetParser
{
    public int SetNumber { get; set; }
    public Entry Weight { get; set; }
    public Entry Reps { get; set; }
    public CheckBox CheckBox { get; set; }
    public double EquipmentMultiplier { get; set; }
}

public class RoutineDataRepository
{
    public string Name { get; set; }
    public Entry SetEntry { get; set; }
    public Picker RestPicker { get; set; }
    public Picker EquipmentPicker { get; set; }
}
