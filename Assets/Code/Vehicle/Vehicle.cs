using GameAnalyticsSDK;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody))]
public class Vehicle : MonoBehaviour
{
    [Header("Controls")]

    [SerializeField]
    private float tapHold = 0f;

    [SerializeField]
    private float tapHoldThreshold = 0.1f;

    [Header("Mics")]

    [SerializeField]
    private PlanetSummoner planetSummoner;

    [SerializeField]
    private Joystick joystick;

    [SerializeField]
    private GameObject jumpVFX;

    [SerializeField]
    private GameObject refuelVFX;

    [SerializeField]
    private GameObject trailVFX;

    [SerializeField]
    private GameObject planetExplosionVFX;

    [Header("Events")]

    [SerializeField]
    private UnityEvent OnReady;

    [SerializeField]
    private UnityEvent OnJump;

    [SerializeField]
    private UnityEvent OnLanded;

    [SerializeField]
    private UnityEvent OnRanOutOfFuel;

    [SerializeField]
    private UnityEvent OnRefueled;

    // Properties

    /// <summary>
    /// The planet you was attracted to latest time.
    /// </summary>
    
    public VehicleDataProvider VehicleDataProvider { get; private set; }

    public Planet LatestPlanet { get; private set; }

    public VehicleMovementStates State { get; private set; } = VehicleMovementStates.Waiting;

    public float FuelAmount
    {
        get
        {
            return fuelAmount;
        }
        private set
        {
            if (value > VehicleDataProvider.Data.FuelMax)
            {
                fuelAmount = VehicleDataProvider.Data.FuelMax;
            }
            else if (value > 0)
            {
                fuelAmount = value;
            }
            else
            {
                OnRanOutOfFuel.Invoke();
            }
        }
    }

    // Fields

    private float fuelAmount;

    private float rotationInput;

    private VehicleMovement movement;
    private Rigidbody rb;

    private void Awake()
    {
        VehicleDataProvider = new VehicleDataProvider(new UpgradesUpdatesData().InitialVehicleData,
            PersistenceProvider.Instance.GameData.UpgradeCards);

        rb = GetComponent<Rigidbody>();
        movement = new VehicleMovement(rb, transform);

        FuelAmount = VehicleDataProvider.Data.FuelMax;

        OnReady.Invoke();
    }

    private void Start()
    {

        OnReady.AddListener(() =>
        {
            State = VehicleMovementStates.Grounded;
        });

        OnRefueled.AddListener(() =>
        {
            if (State == VehicleMovementStates.NoFuel)
            {
                State = VehicleMovementStates.InSpace;
            }
        });

        OnRanOutOfFuel.AddListener(() =>
        {
            State = VehicleMovementStates.NoFuel;
        });

        OnLanded.AddListener(() => {
            try
            {
                GameAnalytics.NewDesignEvent("Events:OnLanded:Once", 1);
            }
            catch (System.Exception) { }
        });
    }

    private void OnEnable()
    {
        // Подписаться на событие
        UpgradeCardController.OnAnyCardUpgrade += OnAnyCardUpgradeEventHandler;
    }

    private void OnDisable()
    {
        // Отписаться от события. Если не отписаться от ивентов C#, то после перезагрузки сцены или подобной ситуации,
        // при вызове события, будет происходить несколько вывозов обработчика, что есть плохо.
        UpgradeCardController.OnAnyCardUpgrade -= OnAnyCardUpgradeEventHandler;
    }

    private void FixedUpdate()
    {
        if (State == VehicleMovementStates.Grounded)
        {
            movement.Update(rotationInput, VehicleDataProvider.Data.MoveSpeed, VehicleDataProvider.Data.RotationSpeed);
        }
    }

    private void JumpToNextPlanet()
    {
        if (!(State == VehicleMovementStates.Grounded))
        {
            return;
        }

        LatestPlanet?.GetComponent<Planet>().MarkAsVisited();
        rb.AddForce(transform.up * VehicleDataProvider.Data.PushbackUp, ForceMode.VelocityChange);
        rb.AddForce(transform.forward * VehicleDataProvider.Data.PushbackForward, ForceMode.VelocityChange);
        State = VehicleMovementStates.InSpace;

        // Hardcoded vfx
        Instantiate(jumpVFX, transform.position, transform.rotation);

        OnJump.Invoke();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (State == VehicleMovementStates.InSpace)
        {
            if (collision.gameObject.GetComponent<Planet>())
            {
                LatestPlanet = collision.gameObject.GetComponent<Planet>();

                if (!LatestPlanet.Visited)
                {
                    State = VehicleMovementStates.Grounded;
                    OnLanded.Invoke();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (State != VehicleMovementStates.Waiting && State != VehicleMovementStates.NoFuel)
        {
            if (!other.Equals(null))
            {
                if (other.GetComponent<Planet>())
                {
                    State = VehicleMovementStates.InSpace;
                }
            }
        }
    }

    private void Update()
    {
        if (State != VehicleMovementStates.Waiting)
        {
            FuelAmount -= VehicleDataProvider.Data.FuelConsumptionRate * Time.deltaTime / 2;
            
        }

        // Input
        rotationInput = joystick.Horizontal;

        // Tap
        if (State == VehicleMovementStates.Grounded)
        {
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject(0))
            {
                tapHold = Time.time;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                float passedTime = Time.time - tapHold;
                if (passedTime <= tapHoldThreshold)
                {
                    JumpToNextPlanet();
                }
                tapHold = 0;
            }
#else
        foreach (Touch touch in Input.touches)
        {
            if (touch.fingerId == 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(0))
                {
                    tapHold = Time.time;
                }

                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    float passedTime = Time.time - tapHold;
                    if (passedTime <= tapHoldThreshold)
                    {
                        JumpToNextPlanet();
                    }
                    tapHold = 0;
                }
            }
        }
#endif
        }
    }

    private void OnAnyCardUpgradeEventHandler()
    {
        VehicleDataProvider = new VehicleDataProvider(new UpgradesUpdatesData().InitialVehicleData,
        PersistenceProvider.Instance.GameData.UpgradeCards);
    }

    /// <summary>
    /// Fill vehicle.
    /// </summary>
    /// <param name="fraction">Value from 0 to 1.</param>
    public void AddFuel(float fraction)
    {
        FuelAmount += VehicleDataProvider.Data.FuelMax * fraction;
        Instantiate(refuelVFX, transform.position, Quaternion.identity);
        OnRefueled.Invoke();
    }

    public void StopWaiting()
    {
        State = VehicleMovementStates.InSpace;
    }

    public enum VehicleMovementStates
    {
        Grounded,
        InSpace,
        Waiting,
        NoFuel
    }
}
