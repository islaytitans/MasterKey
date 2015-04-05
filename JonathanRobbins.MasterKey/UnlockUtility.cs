using System;
using System.Collections.Generic;
using System.Linq;
using JonathanRobbins.MasterKey.Entities;
using JonathanRobbins.MasterKey.Interfaces;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace JonathanRobbins.MasterKey
{
    public class UnlockUtility : IUnlockUtility
    {
        public const string ModalTitle = "Unlock Items";

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

        public UnlockItemsResult UnlockChildren(Item parent)
        {
            return UnlockItems(parent.Children);
        }

        public UnlockItemsResult UnlockItems(IEnumerable<Item> items)
        {
            var result = new UnlockItemsResult();

            var lockedChildren = GetLockedItems(items).ToList();
            if (!lockedChildren.Any())
            {
                result.HasLockedItems = false;
                return result;
            }

            var writeableItemsCollection = SortWritableItems(lockedChildren);

            var unlockedItems = writeableItemsCollection.WritableItems.Select(UnlockItem).ToList();

            return new UnlockItemsResult()
            {
                UnlockedItems = unlockedItems,
                FailedUnlockedItems = writeableItemsCollection.WritableItems.Where(i => !unlockedItems.Contains(i)),
                UnwritableItems = writeableItemsCollection.UnwriteableItems,
            };
        }

        public WritableItemsCollection SortWritableItems(IEnumerable<Item> items)
        {
            var writeableItems = new WritableItemsCollection();

            var itemList = items as Item[] ?? items.ToArray();
            if (!itemList.Any())
                return writeableItems;

            writeableItems.WritableItems = itemList.Where(UnlockPermitted).ToList();
            writeableItems.UnwriteableItems = itemList.Where(j => !writeableItems.WritableItems.Contains(j)).ToList();

            return writeableItems;
        }

        public IEnumerable<Item> GetLockedChildren(Item parent)
        {
            return GetLockedItems(parent.Children);
        }

        public IEnumerable<Item> GetLockedItems(IEnumerable<Item> items)
        {
            return items.Where(i => i.Locking.IsLocked()).ToList();
        }

        public bool UnlockPermitted(Item i)
        {
            if (i.Appearance.ReadOnly || !i.Access.CanWrite() || (!i.Locking.HasLock() || !i.Access.CanWriteLanguage()))
                return false;
            return true;
        }
    }
}
