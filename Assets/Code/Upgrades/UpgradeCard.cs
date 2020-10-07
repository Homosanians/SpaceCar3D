using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class UpgradeCard
{
    // Kepp for serialziaton
    public UpgradeCard() { }

    public UpgradeCard(int CardId)
    {
        this.CardId = CardId;

        if (CardId == 0)
        {
            throw new Exception("0 id is reserved for an error. Card Id must be greater than 0.");
        }
    }

    public UpgradeCard UpdateCardName(string newCardName)
    {
        this.CardName = newCardName;

        return this;
    }

    public UpgradeCard UpdateCardDescription(string newCardDescription)
    {
        this.CardDescription = newCardDescription;

        return this;
    }

    /// <summary>
    /// Greater than 0. 0 is reserved for an error.
    /// </summary>
    public int CardId { get; set; } = 0;

    public string CardName { get; set; }  = "Undefined";

    public string CardDescription { get; set; }  = "Undefined";

    public List<UpgradeLevelData> VehicleLevels { get; set; } = new List<UpgradeLevelData>();

    public int AcquiredLevel
    {
        get
        {
            return VehicleLevels.Count(x => x.Unlocked.Equals(true));
        }
    }

    public UpgradeLevelData NextLevelCard
    {
        get
        {
            /*if (AcquiredLevel < MaxLevel)
            {
                // -1 поскольку нумерация в массиве начинается с 0.
                return VehicleLevels[AcquiredLevel - 1];
            }
            else
            {
                throw new Exception("Next level doesn't exist.");
            }
            */

            if (VehicleLevels.Any(x => x.Unlocked.Equals(false)))
            {
                return VehicleLevels.OrderBy(x => x.Level).First(x => x.Unlocked.Equals(false));
            }
            else
            {
                throw new Exception("Next level doesn't exist.");
            }
        }
    }

    public int MaxLevel
    {
        get
        {
            return VehicleLevels.Count;
        }
    }

    /// <summary>
    /// Уровень карточки может быть повышен.
    /// </summary>
    public bool CanBeUpgraded
    {
        get
        {
            return MaxLevel > AcquiredLevel;
        }
    }

    /// <summary>
    /// Есть звезды на приобритение улучшения.
    /// </summary>
    public bool EnoughStarsToUpgrade
    {
        get
        {
            if (VehicleLevels.Any(x => x.Level.Equals(AcquiredLevel + 1)))
            {
                return VehicleLevels.First(x => x.Level.Equals(AcquiredLevel + 1)).Cost <= PersistenceProvider.Instance.GameData.Stars;
            }
            return false;
        }
    }
}
