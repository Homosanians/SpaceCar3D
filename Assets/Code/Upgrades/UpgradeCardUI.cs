using System.Linq;
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

    private void Awake()
    {
        /*System.Xml.Serialization.XmlSerializer xsSubmit = new System.Xml.Serialization.XmlSerializer(typeof(UpgradeCard));
        var xml = "";
        
        using (var sww = new System.IO.StringWriter())
        {
            using (System.Xml.XmlWriter writer = System.Xml.XmlWriter.Create(sww))
            {
                xsSubmit.Serialize(writer, new UpgradeCard(cardId));
                xml = sww.ToString();
            }
        }

        Debug.Log(xml);*/

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

            UpdateUI();
        }
    }
}
