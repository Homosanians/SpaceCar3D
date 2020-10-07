using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinySauseEvents : MonoBehaviour
{
    public void TheGameHasStarted()
    {
        TinySauce.OnGameStarted();
    }

    public void TheGameHasEnded()
    {
        TinySauce.OnGameFinished(PersistenceProvider.Instance.GameData.Stars);
    }
}
