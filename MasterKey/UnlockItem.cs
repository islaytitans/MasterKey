using System;
using System.Linq;
using MasterKey.Interfaces;
using MasterKey.Pipelines.UnlockItems;
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

            Sitecore.Context.ClientPage.Start("UnlockItemsChildren", new UnlockItemArgs() {Item = selectedItem });
        }
    }
}