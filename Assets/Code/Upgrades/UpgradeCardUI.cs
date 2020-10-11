using DG.Tweening;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCardUI : MonoBehaviour
{
    [SerializeField]
    private int cardId;

    [SerializeField]
    private Text nameText;

    [SerializeField]
    private Text descriptionText;

    [SerializeField]
    private Text costText;

    private UpgradeCardController upgradeCardController;

    private Vector2 startPosition;

    private void Awake()
    {
        startPosition = transform.position;

        upgradeCardController = new UpgradeCardController(cardId);

        UpdateUI();
    }

    private void UpdateUI()
    {
        nameText.text = upgradeCardController.Card.CardName;
        descriptionText.text = upgradeCardController.Card.CardDescription;

        if (upgradeCardController.Card.CanBeUpgraded)
        {
            costText.text = upgradeCardController.Card.NextLevelCard.Cost.ToString();
        }
        else
        {
            costText.text = "MAX".ToString();
        }
    }

    public void Upgrade()
    {
        if (upgradeCardController.Card.CanBeUpgraded)
        {
            var updated = upgradeCardController.TryUpgrade();

            PlayAnimation(updated);

            UpdateUI();
        }
    }

    private void PlayAnimation(bool successfulUpgrade)
    {
        transform.position = startPosition;

        if (successfulUpgrade)
        {
            transform.DOMoveY(startPosition.y - 40, 0.1f, true).OnComplete(() =>
            {
                transform.DOMoveY(startPosition.y, 0.2f, true);
            });
        }
        else
        {
            transform.DOShakePosition(0.2f, 20, 30).OnComplete(() =>
            {
                transform.position = startPosition;
            });
        }
    }
}
