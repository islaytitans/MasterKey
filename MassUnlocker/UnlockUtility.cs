using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChildrenUnlocker.Entities;
using ChildrenUnlocker.Interfaces;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace ChildrenUnlocker
{
    public class UnlockUtility : IUnlockUtility
    {
        public WritableItemsCollection SortWritableItems(IEnumerable<Item> items)
        {
            var writeableItems = new WritableItemsCollection();

            var itemList = items as Item[] ?? items.ToArray();
            if (!itemList.Any())
                return writeableItems;

            writeableItems.WritableItems = itemList.Where(i => i.Access.CanWriteLanguage()).ToList();
            writeableItems.UnwriteableItems = itemList.Where(j => !writeableItems.WritableItems.Contains(j)).ToList();

            return writeableItems;
        }

        public Item UnlockItem(Item item)
        {
            try
            {
                item.Editing.BeginEdit();
                item.Locking.Unlock();
                item.Editing.EndEdit();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex, this);
                return null;
            }

            return item;
        }
    }
}
