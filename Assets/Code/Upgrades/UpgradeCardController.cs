using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;

public class UpgradeCardController
{
    public static event Action OnAnyCardUpgrade;

    public UpgradeCard Card { get; private set; }

    public UpgradeCardController(int cardId)
    {
        // individual card migrations somewhre here
        // individual card migrations somewhre here
        // individual card migrations somewhre here
        // individual card migrations somewhre here
        // individual card migrations somewhre here
        // individual card migrations somewhre here пошеланухй

        if (PersistenceProvider.Instance.GameData.UpgradeCards.Any(x => x.CardId == cardId))
        {
            this.Card = PersistenceProvider.Instance.GameData.UpgradeCards.First(x => x.CardId == cardId);
        }
        else if (new UpgradesUpdatesData().UpgradeCards.Any(x => x.CardId == cardId))
        {
            PersistenceProvider.Instance.GameData.UpgradeCards.Add(new UpgradesUpdatesData().UpgradeCards.First(x => x.CardId == cardId));
            this.Card = PersistenceProvider.Instance.GameData.UpgradeCards.First(x => x.CardId == cardId);
        }
        else
        {
            throw new Exception($"Any card with corresponding id ({cardId}) was not found.");
        }
    }

    public bool TryUpgrade()
    {
        if (Card.CanBeUpgraded && Card.EnoughStarsToUpgrade)
        {
            PersistenceProvider.Instance.GameData.Stars -= Card.NextLevelCard.Cost;
            Card.NextLevelCard.Unlocked = true;

            OnAnyCardUpgrade?.Invoke();
        }

        return false;
    }
}
