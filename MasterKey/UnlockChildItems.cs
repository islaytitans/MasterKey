using System;
using System.Linq;
using MasterKey.Entities;
using MasterKey.Interfaces;
using MasterKey.Pipelines.UnlockItems;
using Sitecore;
using Sitecore.Diagnostics;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Web.UI.Sheer;

namespace MasterKey
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

            Sitecore.Context.ClientPage.Start("UnlockChildren", new UnlockItemArgs() { Item = selectedItem, UnlockChildren = true});
        }
    }
}