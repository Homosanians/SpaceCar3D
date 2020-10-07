using System.Linq;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField]
    private float gravity = -10f;

    [SerializeField]
    private float sphereOfInfluenceRatio = 4f;

    [SerializeField]
    [Range(0, 1)]
    private float rotateBodyStartHeightOffset = 0f;

    public bool Visited { get; private set; } = false;

    public float RadiusCoreToSurface { get; private set; }

    public float RadiusCoreToBeginningSphereOfInfluence { get; private set; }

    public float RadiusSurfaceToBeginningSphereOfInfluence { get; private set; }

    private void Awake()
    {
        // Убедиться что это шар
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.x, transform.localScale.x);

        SphereCollider[] colliders = GetComponents<SphereCollider>();

        if (colliders.Length == 0)
        {
            gameObject.AddComponent<SphereCollider>();
        }

        if (!colliders.Any(x => x.isTrigger))
        {
            SphereCollider newTriger = gameObject.AddComponent<SphereCollider>();
            newTriger.isTrigger = true;
            newTriger.radius *= sphereOfInfluenceRatio;
        }

        // Найдено экспериментально
        // Linq выражение ".First(x => x.isTrigger == false)" ищет внешний коллайдер поскольку он есть тригер
        RadiusCoreToSurface = GetComponents<SphereCollider>().First(x => x.isTrigger == false).radius * transform.localScale.x;

        RadiusCoreToBeginningSphereOfInfluence = RadiusCoreToSurface * sphereOfInfluenceRatio;
        RadiusSurfaceToBeginningSphereOfInfluence = RadiusCoreToBeginningSphereOfInfluence - RadiusCoreToSurface;
    }

    public void MarkAsVisited()
    {
        Visited = true;
        gravity = 0;
    }

    private void OnTriggerStay(Collider other)
    {
        Rigidbody subjectRb = other.GetComponentInParent<Rigidbody>();

        if (!subjectRb.Equals(null))
        {
            if (!Visited)
            {
                Attract(subjectRb);
                RotateBody(subjectRb);
            }
        }
    }

    private void Attract(Rigidbody body)
    {
        Vector3 normalized = (body.position - transform.position).normalized;

        float pullForce = gravity * GetDistanceFactor01(body.transform);
        pullForce = -Mathf.Abs(pullForce);

        body.AddForce(-body.velocity); // ЕТакже как вариант увеличивать rigidbody.drag при вхождении в сферу влияния и уменьшать значение по мере приближения
        body.AddForce(normalized * pullForce);
    }

    private void RotateBody(Rigidbody body)
    {
        Vector3 normalized = (body.position - transform.position).normalized;
        Quaternion b = Quaternion.FromToRotation(body.transform.up, normalized) * body.rotation;

        float interpolateFactor = Mathf.Clamp01(GetDistanceFactor01(body.transform) - rotateBodyStartHeightOffset);

        // Предел 0.1. Нет информации почему. // y=x^2 потому что при y=x поворот происходит сразу же
        body.MoveRotation(Quaternion.Slerp(body.rotation, b, Mathf.Pow(interpolateFactor, 2) / 10));
    }

    /// <summary>
    /// Соотношение дальности от поверхности планеты до субъекта.
    /// </summary>
    /// <returns>Соотношение дальности в пределах 0 и 1. Запредельные значение обрезаются. 1 - на поверхности, 0 - вне гравитации планеты.</returns>
    private float GetDistanceFactor01(Transform subject)
    {
        return Mathf.Clamp01(MathUtil.Map(Vector3.Distance(transform.position, subject.position) - RadiusCoreToSurface, 0, RadiusSurfaceToBeginningSphereOfInfluence, 1, 0));
    }
}
