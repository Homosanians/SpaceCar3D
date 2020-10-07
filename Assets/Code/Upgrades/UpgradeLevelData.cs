public class UpgradeLevelData
{
    public int Level { get; set; } = 0;

    public int Cost { get; set; } = 0;

    public bool Unlocked { get; set; } = false;

    public VehicleData VehicleData { get; set; } = new VehicleData();
}
