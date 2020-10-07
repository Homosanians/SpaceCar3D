using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Мистер жопа.
/// </summary>
public class UpgradeCardMigrations
{
    /// <summary>
    /// Биг попа.
    /// </summary>
    public UpgradeCardMigrations()
    {
        throw new NotImplementedException();
    }
    // если файл с картами устарел, обновить модели, добавить ещё leveldata но не удалять Unlocked property.

    public void Migrate(List<UpgradeCard> newUpgradeCards)
    {
        foreach (var cardItem in PersistenceProvider.Instance.GameData.UpgradeCards)
        {
            if (newUpgradeCards.Any(x => x.CardId == cardItem.CardId))
            {
                // Обновленная карта. ID старой совпадает с ID новой.
                UpgradeCard newCardItem = newUpgradeCards.First(x => x.CardId == cardItem.CardId);

                // Attention!
                // Обновляем все возможные данные старой карты из новой.
                // Эта строка может дополняется. При добавлении новых характеристик карте, нужно обновить это поле в миграции!
                cardItem.UpdateCardName(newCardItem.CardName).UpdateCardDescription(newCardItem.CardDescription);

                // ТЕСТ ЭОТГО !!!
                //PersistenceProvider.Instance.GameData.UpgradeCards.Find(x => x.CardId == cardItem.CardId).UpdateCardName();
                
                // Смотрим старые уровни
                foreach(var upgradeLevel in cardItem.VehicleLevels)
                {
                    // Поиск уровня по его значению.
                    if (newCardItem.VehicleLevels.Any(x => x.Level == upgradeLevel.Level))
                    {
                        //var newUpgradeLevel = newCardItem.VehicleLevels.Find

                        //upgradeLevel.VehicleData
                    }
                    // Карточка не найдена.
                    else
                    {
                        // В новых картах, такого уровня нет - удаляем.
                        cardItem.VehicleLevels.Remove(upgradeLevel);
                    }
                }

                // Добавить уровни из новой коллекции, которых нет в старой
                
            }
            else
            {
                // Добавить карты, ID которых ранее не было.
                PersistenceProvider.Instance.GameData.UpgradeCards.AddRange(
                    newUpgradeCards.Where(x => x.CardId != cardItem.CardId).ToList());
            }
        }
    }
}
