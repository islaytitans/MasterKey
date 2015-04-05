using System;
using System.Linq;
using JonathanRobbins.MasterKey.Interfaces;
using JonathanRobbins.MasterKey.Pipelines.UnlockItems;
using JonathanRobbins.MasterKey;
using Sitecore;
using Sitecore.Shell.Framework.Commands;

namespace JonathanRobbins.MasterKey
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

            Sitecore.Context.ClientPage.Start("UnlockItemsChildren", new UnlockItemArgs() {Item = selectedItem });
        }
    }
}