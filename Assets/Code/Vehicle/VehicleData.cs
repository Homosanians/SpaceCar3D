using System;

[Serializable]
public class VehicleData
{
    // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/operator-overloading
    public static VehicleData operator +(VehicleData a, VehicleData b)
        => new VehicleData { 
            FuelConsumptionRate = a.FuelConsumptionRate + b.FuelConsumptionRate,
            FuelMax = a.FuelMax + b.FuelMax,
            MoveSpeed = a.MoveSpeed + b.MoveSpeed,
            RotationSpeed = a.RotationSpeed + b.RotationSpeed,
            PushbackUp = a.PushbackUp + b.PushbackUp,
            PushbackForward = a.PushbackForward + b.PushbackForward
        };

    public float FuelConsumptionRate { get; set; }

    public float FuelMax { get; set; }

    public float MoveSpeed { get; set; }

    public float RotationSpeed { get; set; }

    public float PushbackUp { get; set; }

    public float PushbackForward { get; set; }
}
