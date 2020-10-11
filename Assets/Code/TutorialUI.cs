using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialUI : MonoBehaviour
{
    public UnityEvent OnGameFirstStart;

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
        OnGameFirstStart.Invoke();
    }
}
