using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    [SerializeField]
    private GameObject starCollectVFX;

    [SerializeField]
    private GameObject cameraShaker;

    private void Awake()
    {
        //cameraShaker = GameObject.Find("Main Camera");
        cameraShaker = Camera.main.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(starCollectVFX, transform.position, transform.rotation);
        cameraShaker.GetComponent<SimpleCameraShakeInCinemachine>().ShakeOnce();
        PersistenceProvider.Instance.GameData.Stars += 1;
        Destroy(transform.parent.gameObject);
    }
}
