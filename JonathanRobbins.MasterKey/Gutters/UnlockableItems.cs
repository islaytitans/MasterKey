using System;
using JonathanRobbins.MasterKey.Interfaces;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Globalization;
using Sitecore.Security.AccessControl;
using Sitecore.Security.Accounts;
using Sitecore.Shell.Applications.ContentEditor.Gutters;

namespace JonathanRobbins.MasterKey.Gutters
{
    [Serializable]
    public class UnlockableItems : GutterRenderer
    {
        private IUnlockUtility _unlockUtility = new UnlockUtility();

        protected override GutterIconDescriptor GetIconDescriptor(Item item)
        {
            if (!_unlockUtility.UnlockPermitted(item))
                return (GutterIconDescriptor)null;

            var gutterIconDescriptor = new GutterIconDescriptor
            {
                Icon = "Network/32x32/key1.png",
                Tooltip = Translate.Text("Locked by") + " " + item.Locking.GetOwnerWithoutDomain()
            };

            if (_unlockUtility.UnlockPermitted(item))
                gutterIconDescriptor.Click = "MasterKey:UnlockItem(id=" + (object)item.ID + ")";

            return gutterIconDescriptor;
        }
    }
}
