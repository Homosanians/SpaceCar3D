using System.Collections.Generic;

/// <summary>
/// Суммирует VehicleData по приобетённым прокачкам.
/// </summary>
public class VehicleDataProvider
{
    public VehicleData Data { get; private set; }

    public VehicleDataProvider(VehicleData initialVehicleData, List<UpgradeCard> upgrades)
    {
        foreach(var upgrade in upgrades)
        {
            foreach (var level in upgrade.VehicleLevels)
            {
                if (level.Unlocked)
                {
                    initialVehicleData += level.VehicleData;
                }
            }
        }

        this.Data = initialVehicleData;
    }
}
