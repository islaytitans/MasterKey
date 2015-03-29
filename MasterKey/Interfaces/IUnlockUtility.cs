using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasterKey.Entities;
using Sitecore.Data.Items;

namespace MasterKey.Interfaces
{
    public interface IUnlockUtility
    {
        WritableItemsCollection SortWritableItems(IEnumerable<Item> items);
        Item UnlockItem(Item item);
        UnlockItemsResult UnlockChildren(Item parent);
        UnlockItemsResult UnlockItems(IEnumerable<Item> items);
        IEnumerable<Item> GetLockedChildren(Item parent);
        IEnumerable<Item> GetLockedItems(IEnumerable<Item> items);
    }
}
