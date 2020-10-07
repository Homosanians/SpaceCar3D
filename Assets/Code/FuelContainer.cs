using UnityEngine;

public class FuelContainer : MonoBehaviour
{

    [Range(0, 100)]
    [SerializeField]
    private int refillPercentage = 100;

    private Vehicle vehicle;

    private void Start()
    {
        vehicle = FindObjectOfType<Vehicle>();
    }

    private void OnTriggerEnter(Collider other)
    {
        vehicle.AddFuel((float)refillPercentage / 100f);
        Destroy(transform.parent.gameObject);
    }
}