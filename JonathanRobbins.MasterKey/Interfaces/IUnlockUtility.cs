using System.Collections.Generic;
using JonathanRobbins.MasterKey.Entities;
using Sitecore.Data.Items;

namespace JonathanRobbins.MasterKey.Interfaces
{
    public interface IUnlockUtility
    {
        WritableItemsCollection SortWritableItems(IEnumerable<Item> items);
        Item UnlockItem(Item item);
        UnlockItemsResult UnlockChildren(Item parent);
        UnlockItemsResult UnlockItems(IEnumerable<Item> items);
        IEnumerable<Item> GetLockedChildren(Item parent);
        IEnumerable<Item> GetLockedItems(IEnumerable<Item> items);
        bool UnlockPermitted(Item item);
    }
}
