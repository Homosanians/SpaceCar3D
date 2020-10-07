using UnityEngine;
using UnityEngine.UI;

public class FuelUI : MonoBehaviour
{
    [SerializeField]
    private Image fuelFill;

    private Vehicle vehicle;

    private void Start()
    {
        vehicle = FindObjectOfType<Vehicle>();
    }

    private void Update()
    {
        fuelFill.fillAmount = vehicle.FuelAmount;
    }
}
