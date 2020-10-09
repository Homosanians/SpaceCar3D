using System;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField]
    private GameObject[] gameObjectsToHide;

    private void Start()
    {
        if (!PersistenceProvider.Instance.GameData.TutorialPassed)
        {
            ExecuteTutorial();
            PersistenceProvider.Instance.GameData.TutorialPassed = true;
        }
    }

    private void ExecuteTutorial()
    {
        foreach(GameObject go in gameObjectsToHide)
        {
            go.SetActive(false);
        }
    }
}
