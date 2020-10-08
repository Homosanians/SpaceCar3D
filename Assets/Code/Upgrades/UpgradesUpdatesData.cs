using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class UpgradesUpdatesData
{
    public int Version { get; set; } = 1;

    public VehicleData InitialVehicleData { get; set; } = new VehicleData
    {
        FuelConsumptionRate = 0.16f,
        FuelMax = 1,
        PushbackForward = 50,
        MoveSpeed = 6,
        PushbackUp = 5,
        RotationSpeed = 60
    };

    public List<UpgradeCard> UpgradeCards { get; set; } = new List<UpgradeCard>
    {
        new UpgradeCard(CardId: 1)
        {
            CardName = "FUEL",
            CardDescription = "Makes your space adventure longer!",
            VehicleLevels = new List<UpgradeLevelData>
            {
                new UpgradeLevelData
                {
                    Level = 1,
                    Cost = 10,
                    VehicleData = new VehicleData
                    {
                        FuelConsumptionRate = -0.024f,
                    }
                },
                new UpgradeLevelData
                {
                    Level = 2,
                    Cost = 20,
                    VehicleData = new VehicleData
                    {
                        FuelConsumptionRate = -0.024f,
                    }
                },
                new UpgradeLevelData
                {
                    Level = 3,
                    Cost = 40,
                    VehicleData = new VehicleData
                    {
                        FuelConsumptionRate = -0.024f,
                    }
                },
                new UpgradeLevelData
                {
                    Level = 4,
                    Cost = 80,
                    VehicleData = new VehicleData
                    {
                        FuelConsumptionRate = -0.024f,
                    }
                },
                new UpgradeLevelData
                {
                    Level = 5,
                    Cost = 120,
                    VehicleData = new VehicleData
                    {
                        FuelConsumptionRate = -0.006f,
                    }
                },
                new UpgradeLevelData
                {
                    Level = 6,
                    Cost = 160,
                    VehicleData = new VehicleData
                    {
                        FuelConsumptionRate = -0.006f,
                    }
                },
                new UpgradeLevelData
                {
                    Level = 7,
                    Cost = 200,
                    VehicleData = new VehicleData
                    {
                        FuelConsumptionRate = -0.003f,
                    }
                },
                new UpgradeLevelData
                {
                    Level = 8,
                    Cost = 260,
                    VehicleData = new VehicleData
                    {
                        FuelConsumptionRate = -0.003f,
                    }
                },
                new UpgradeLevelData
                {
                    Level = 9,
                    Cost = 320,
                    VehicleData = new VehicleData
                    {
                        FuelConsumptionRate = -0.003f,
                    }
                },
                new UpgradeLevelData
                {
                    Level = 10,
                    Cost = 380,
                    VehicleData = new VehicleData
                    {
                        FuelConsumptionRate = -0.003f,
                    }
                }
            }
        },
        new UpgradeCard(CardId: 2)
        {
            CardName = "SPEED",
            CardDescription = "Increases car speed",
            VehicleLevels = new List<UpgradeLevelData>
            {
                new UpgradeLevelData
                {
                    Level = 1,
                    Cost = 10,
                    VehicleData = new VehicleData
                    {
                        MoveSpeed = 3f,
                        RotationSpeed = 6,
                    }
                },
                new UpgradeLevelData
                {
                    Level = 2,
                    Cost = 20,
                    VehicleData = new VehicleData
                    {
                        MoveSpeed = 2f,
                        RotationSpeed = 4,
                    }
                },
                new UpgradeLevelData
                {
                    Level = 3,
                    Cost = 40,
                    VehicleData = new VehicleData
                    {
                        MoveSpeed = 1.5f,
                        RotationSpeed = 4,
                    }
                },
                new UpgradeLevelData
                {
                    Level = 4,
                    Cost = 80,
                    VehicleData = new VehicleData
                    {
                        MoveSpeed = 1,
                        RotationSpeed = 4,
                    }
                },
                new UpgradeLevelData
                {
                    Level = 5,
                    Cost = 120,
                    VehicleData = new VehicleData
                    {
                        MoveSpeed = 0.5f,
                        RotationSpeed = 4,
                    }
                },
                new UpgradeLevelData
                {
                    Level = 6,
                    Cost = 160,
                    VehicleData = new VehicleData
                    {
                        MoveSpeed = 0.4f,
                        RotationSpeed = 4,
                    }
                },
                new UpgradeLevelData
                {
                    Level = 7,
                    Cost = 200,
                    VehicleData = new VehicleData
                    {
                        MoveSpeed = 0.4f,
                        RotationSpeed = 4,
                    }
                },
                new UpgradeLevelData
                {
                    Level = 8,
                    Cost = 260,
                    VehicleData = new VehicleData
                    {
                        MoveSpeed = 0.4f,
                        RotationSpeed = 4,
                    }
                },
                new UpgradeLevelData
                {
                    Level = 9,
                    Cost = 320,
                    VehicleData = new VehicleData
                    {
                        MoveSpeed = 0.4f,
                        RotationSpeed = 4,
                    }
                },
                new UpgradeLevelData
                {
                    Level = 10,
                    Cost = 380,
                    VehicleData = new VehicleData
                    {
                        MoveSpeed = 0.4f,
                        RotationSpeed = 4,
                    }
                }
            }
        }
    };
}
