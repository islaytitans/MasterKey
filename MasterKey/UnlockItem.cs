using System;
using System.Linq;
using MasterKey.Interfaces;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Web.UI.Sheer;

namespace MasterKey
{
    [Serializable]
    public class UnlockItem : Command
    {
        protected IUnlockUtility UnlockUtility = new UnlockUtility();

        public override void Execute([NotNull] CommandContext context)
        {
            var selectedItem = context.Items.FirstOrDefault();
            if (selectedItem == null)
                return;

            if (selectedItem.Locking.IsLocked())
            {
                UnlockUtility.UnlockItem(selectedItem);
            }

            var lockedChildren = selectedItem.Children.Where(i => i.Locking.IsLocked()).ToList();
            if (!lockedChildren.Any())
                return;

            var writeableItemsCollection = UnlockUtility.SortWritableItems(lockedChildren);
            if (!writeableItemsCollection.WritableItems.Any())
            {
                return;
            }

            SheerResponse.YesNoCancel("Unlock children?", "200", "200");
        }
    }
}