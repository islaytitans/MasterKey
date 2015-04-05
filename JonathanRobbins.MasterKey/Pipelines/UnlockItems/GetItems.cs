using System.Linq;
using Sitecore.Diagnostics;

namespace JonathanRobbins.MasterKey.Pipelines.UnlockItems
{
    public class GetItems
    {
        public void AssertItem(GetItemArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            if (args.Item == null)
            {
                args.AbortPipeline();
            }
        }

        public void GetChildItems(GetItemArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            Assert.ArgumentNotNull(args.Item, "args.Item");
            args.ChildItems = args.Item.Children.ToList();
        }
    }
}
