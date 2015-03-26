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
    }
}
