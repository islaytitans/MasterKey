using System;
using System.Linq;
using ChildrenUnlocker.Entities;
using ChildrenUnlocker.Interfaces;
using Sitecore;
using Sitecore.Diagnostics;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Web.UI.Sheer;

namespace ChildrenUnlocker
{
    [Serializable]
    public class UnlockChildItems : Command
    {
        public const string ModalTitle = "Unlock Items";
        protected IUnlockUtility UnlockUtility = new UnlockUtility();

        public override void Execute([NotNull] CommandContext context)
        {
            var selectedItem = context.Items.FirstOrDefault();
            if (selectedItem == null)
                return;

            var lockedChildren = selectedItem.Children.Where(i => i.Locking.IsLocked()).ToList();
            if (!lockedChildren.Any())
            {
                SheerResponse.Alert("Item '" + selectedItem.Name + "' does not have any child items that are locked",
                    false,
                    ModalTitle);
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

            SheerResponse.Alert(String.Format("<div>{0}<div>", result.AlertMessageHtml), 
                false,
                ModalTitle);
        }
    }
}