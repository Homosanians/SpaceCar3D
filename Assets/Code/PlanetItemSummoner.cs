using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Planet))]
public class PlanetItemSummoner : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;

    public int minAmount;
    public int maxAmount;

    private Planet planet;

    private void Awake()
    {
        planet = GetComponent<Planet>();

        for (int i = 0; i < Random.Range(minAmount, maxAmount); i++)
        {
            Vector3 spawnPosition = Random.onUnitSphere * (planet.RadiusCoreToSurface + prefab.transform.lossyScale.y) + planet.transform.position;

            GameObject creation = Instantiate(prefab, spawnPosition, Quaternion.identity);
            creation.transform.parent = planet.transform;

            creation.transform.LookAt(planet.transform.position);
            creation.transform.Rotate(new Vector3(-90, 0, 0));
        }
    }
}
