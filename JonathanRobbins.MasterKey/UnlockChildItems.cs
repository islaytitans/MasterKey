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
    }
}