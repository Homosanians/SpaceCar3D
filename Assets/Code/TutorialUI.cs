using System;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    private void Start()
    {
        if (!PersistenceProvider.Instance.GameData.TutorialPassed)
        {
            Execute();
        }
    }

    private void Execute()
    {
        // ...
        throw new System.NotImplementedException();

        PersistenceProvider.Instance.GameData.TutorialPassed = true;
    }
}
