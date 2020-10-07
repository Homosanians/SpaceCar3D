using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarsUI : MonoBehaviour
{
    [SerializeField]
    private Text starsAmount;

    private void Update()
    {
        starsAmount.text = PersistenceProvider.Instance.GameData.Stars.ToString();
    }
}
