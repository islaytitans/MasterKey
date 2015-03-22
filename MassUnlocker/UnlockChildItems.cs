﻿using System;
using System.Linq;
using ChildrenUnlocker.Entities;
using ChildrenUnlocker.Interfaces;
using Sitecore;
using Sitecore.Diagnostics;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Web.UI.Sheer;

namespace ChildrenUnlocker
{
    // TODO: \App_Config\include\UnlockChildItems.config created automatically when creating UnlockChildItems class. In this config include file, specify command name attribute value

    [Serializable]
    public class UnlockChildItems : Command
    {
        protected IUnlockUtility UnlockUtility = new UnlockUtility();

        public override void Execute([NotNull] CommandContext context)
        {
            var selectedItem = context.Items.FirstOrDefault();
            if (selectedItem == null)
                return;

            var lockedChildren = selectedItem.Children.Where(i => i.Locking.IsLocked()).ToList();
            if (!lockedChildren.Any())
            {
                //Report instead
                Log.Info("Item '" + selectedItem.Name + "' does not have child items that are locked.", this);
                return;
            }

            var writeableItemsCollection = UnlockUtility.SortWritableItems(lockedChildren);

            var unlockedItems = writeableItemsCollection.WritableItems.Select(UnlockUtility.UnlockItem).ToList();

            var result = new UnlockItemsResult()
            {
                UnlockedItems = unlockedItems,
                FailedUnlockedItems = writeableItemsCollection.WritableItems.Where(i => !unlockedItems.Contains(i)),
                UnwritableItems = writeableItemsCollection.UnwriteableItems,
            };

            SheerResponse.Alert(string.Format("<p>{0}</p>", result.AlertMessage), 
                false,
                "Unlock Items");
        }
    }
}