using System;
using System.Linq;
using JonathanRobbins.MasterKey.Interfaces;
using JonathanRobbins.MasterKey.Pipelines.UnlockItems;
using JonathanRobbins.MasterKey;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Shell.Framework.Commands;

namespace JonathanRobbins.MasterKey
{
    [Serializable]
    public class UnlockItem : Command
    {
        private IUnlockUtility _unlockUtility = new UnlockUtility();

        public override void Execute([NotNull] CommandContext context)
        {
            var selectedItem = context.Items.FirstOrDefault();
            if (selectedItem == null)
                return;

            if (selectedItem.Locking.IsLocked() && _unlockUtility.UnlockPermitted(selectedItem))
            {
                _unlockUtility.UnlockItem(selectedItem);
            }

            Sitecore.Context.ClientPage.Start("uiUnlockItemsChildren", new UnlockItemArgs() {Item = selectedItem });
        }

        public override CommandState QueryState(CommandContext context)
        {
            Assert.ArgumentNotNull((object)context, "context");
            if (context.Items.Length != 1)
                return CommandState.Disabled;

            Item item = context.Items[0];
            if (Context.IsAdministrator)
                return !item.Locking.IsLocked() ? CommandState.Disabled : CommandState.Enabled;
            if (!_unlockUtility.UnlockPermitted(item))
                return CommandState.Disabled;
            return base.QueryState(context);
        }
    }
}