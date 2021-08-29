using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class PlanetSummoner : MonoBehaviour
{
    public Vector3 UniverseCenter { get; } = Vector3.zero;

    private List<Planet> summonedPlanets = new List<Planet>();

    public List<Planet> SummonedPlanets
    {
        // Skip all null values. Null values are gathered by casting Destroy() on GameObjects.
        get
        {
            summonedPlanets = summonedPlanets.Where(x => x != null).ToList();
            return summonedPlanets;
        }
        private set
        {
            summonedPlanets = value.Where(x => x != null).ToList();
        }
    }

    public List<Planet> SummonedPlanetsSortedByDistance
    {
        get
        {
            // Sorting from the center of the universe to planets' position by distance. Lowest to highest.
            return SummonedPlanets.OrderBy(x => Vector3.Distance(UniverseCenter, x.transform.position)).ToList();
        }
    }

    [SerializeField]
    private GameObject[] prefabVariants;

    [SerializeField]
    private Vehicle relativeVehicle;

    [SerializeField]
    private uint planetsAheadLimit;

    [SerializeField]
    private uint planetsBelowLimit;

    [SerializeField]
    private float angleOfPlanetSpawn;

    [SerializeField]
    private float minDistance;

    [SerializeField]
    private float maxDistance;

    [SerializeField]
    private float minPlanetSize;

    [SerializeField]
    private float maxPlanetSize;

    [Header("Y Axis")]

    [SerializeField]
    private bool enableYAxisGeneration;

    [SerializeField]
    private float angleYAxis;

    [SerializeField]
    private float maxHeightOfYAxis;

    private void Start()
    {
        StartCoroutine(TickPlanets());
    }

    /// <summary>
    /// Elementary trigonomentry formula.
    /// </summary>
    /// <param name="angle">Angle in degress</param>
    /// <param name="distance">Distance to subject</param>
    /// <returns>Offset to subject in 2D plane</returns>
    public static Vector3 GetOffsetPositionByAngleAndDistanceXZ(float angle, float distance)
    {
        // Negative value = lefthand side.
        angle *= -1;

        float x = Mathf.Cos(angle * Mathf.Deg2Rad) * distance;
        float z = Mathf.Sin(angle * Mathf.Deg2Rad) * distance;

        return new Vector3(x, 0, z);
    }

    public static Vector3 GetOffsetPositionByAngleAndDistanceY(float angle, float distance)
    {
        float y = Mathf.Sin(angle * Mathf.Deg2Rad) * distance;

        return new Vector3(0, y, 0);
    }

    IEnumerator TickPlanets()
    {
        for (int i = 0; i < planetsAheadLimit; i++)
        {
            GenerateOne();
        }

        while (true)
        {
            for (int i = 0; i < SummonedPlanets.Count; i++)
            {
                if (SummonedPlanets[i] == relativeVehicle.LatestPlanet)
                {
                    int currentPlanet = i + 1;
                    int planetsAhead = SummonedPlanets.Count - currentPlanet;
                    int planetsBelow = SummonedPlanets.Count - planetsAhead - 1;

                    if (planetsBelow > planetsBelowLimit)
                    {
                        // Найти сколько планет больше нижнего предела и удалить их.
                        for (int j = 0; j < planetsBelow - planetsBelowLimit; j++)
                        {
                            // Use real order.
                            Destroy(SummonedPlanetsSortedByDistance[j].gameObject);
                        }
                    }

                    if (planetsAhead < planetsAheadLimit)
                    {
                        // Найти сколько планет не достает до верхнего предела и создать их.
                        for (int k = 0; k < planetsAheadLimit - planetsAhead; k++)
                        {
                            GenerateOne();
                        }
                    }
                }
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    private void GenerateOne()
    {
        Vector3 finalPosition = UniverseCenter;

        if (SummonedPlanets.Count > 0)
        {
            // Биссектрисса, исходящая из нулевого угла, большого угла делит его пополам. Например: угол 90 может быть между -45 и 45.
            float angle = Random.Range(angleOfPlanetSpawn / 2 * -1, angleOfPlanetSpawn / 2);

            var lastPlanet = SummonedPlanets.Last();

            finalPosition = lastPlanet.transform.position +
                GetOffsetPositionByAngleAndDistanceXZ(angle, Random.Range(minDistance, maxDistance));

            if (enableYAxisGeneration)
            {
                finalPosition += GetOffsetPositionByAngleAndDistanceY(
                    Random.Range(angleYAxis / 2 * -1, angleYAxis / 2),
                    Random.Range(0, maxHeightOfYAxis)
                    );
            }
        }

        float randomDiameter = Random.Range(minPlanetSize, maxPlanetSize);

        GameObject modifiedPrefab = prefabVariants[Random.Range(0, prefabVariants.Length)];
        modifiedPrefab.transform.localScale = new Vector3(randomDiameter, randomDiameter, randomDiameter);

        GameObject instantialedObject = Instantiate(modifiedPrefab, finalPosition, Quaternion.identity);

        SummonedPlanets.Add(instantialedObject.GetComponent<Planet>());
    }
}
