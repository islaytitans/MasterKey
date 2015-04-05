using Sitecore.Data.Items;
using Sitecore.Globalization;
using Sitecore.Shell.Applications.ContentEditor.Gutters;

namespace JonathanRobbins.MasterKey.Gutters
{
    public class UnlockableItems : GutterRenderer
    {
        protected override GutterIconDescriptor GetIconDescriptor(Item item)
        {
            if (!item.Locking.IsLocked() || !item.Access.CanWrite() ||  !item.Access.CanWriteLanguage())
                return (GutterIconDescriptor)null;

            var gutterIconDescriptor = new GutterIconDescriptor
            {
                Icon = "Network/32x32/key1.png",
                Tooltip = Translate.Text("Locked by") + " " + item.Locking.GetOwnerWithoutDomain()
            };

            if (item.Locking.CanUnlock() && item.Access.CanWrite() && item.Access.CanWriteLanguage() &&
                !item.Appearance.ReadOnly)
                gutterIconDescriptor.Click = "MasterKey:UnlockItem(id=" + (object)item.ID + ")";
            
            return gutterIconDescriptor;
        }
    }
}
