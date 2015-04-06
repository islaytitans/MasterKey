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
    public class UnlockChildItems : Command
    {
        protected IUnlockUtility UnlockUtility = new UnlockUtility();

        public override void Execute([NotNull] CommandContext context)
        {
            var selectedItem = context.Items.FirstOrDefault();
            if (selectedItem == null)
                return;

            Sitecore.Context.ClientPage.Start("uiUnlockChildren", new UnlockItemArgs() { Item = selectedItem, UnlockChildren = true});
        }

        public override CommandState QueryState(CommandContext context)
        {
            Assert.ArgumentNotNull((object)context, "context");
            if (context.Items.Length != 1)
                return CommandState.Disabled;
            Item item = context.Items[0];
            if (!item.Children.Any())
                return CommandState.Disabled;

            if (Context.IsAdministrator)
                return !item.Children.Any(c => c.Locking.IsLocked()) ? CommandState.Disabled : CommandState.Enabled;

            bool permittedUnlock = item.Children.Any(c => UnlockUtility.UnlockPermitted(c));
            if (!permittedUnlock)
                return CommandState.Disabled;
            return base.QueryState(context);
        }
    }
}