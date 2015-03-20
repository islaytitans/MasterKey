using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore;

namespace MassUnlocker
{
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Sitecore.Shell.Framework.Commands;

    // TODO: \App_Config\include\UnlockChildItems.config created automatically when creating UnlockChildItems class. In this config include file, specify command name attribute value



    [Serializable]
    public class UnlockChildItems : Command
    {
        public override void Execute([NotNull] CommandContext context)
        {
            var selectedItem = context.Items.FirstOrDefault();
            if (selectedItem == null)
                return;

            var lockedChildren = selectedItem.Children.Where(i => i.Locking.IsLocked()).ToList();
            if (!lockedChildren.Any())
            {
                Log.Info("Item '" + selectedItem.Name + "' does not have child items that are locked.", this);
                return;
            }

            lockedChildren.Select(UnlockItem);

            bool canWrite = UserHasWriteAccess(selectedItem);
        }

        private bool UnlockItem(Item item)
        {
            bool success = false;

            try
            {
                item.Editing.BeginEdit();
                item.Locking.Unlock();
                item.Editing.EndEdit();

                success = true;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex, this);
                success = false;
            }

            return success;
        }

        private bool UserHasWriteAccess(Item item)
        {
            return item.Access.CanWriteLanguage();
        }
    }
}