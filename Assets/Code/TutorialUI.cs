using System;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
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
        throw new System.NotImplementedException();
    }
}
