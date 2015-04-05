using System.Collections.Generic;
using Sitecore.Data.Items;
using Sitecore.Pipelines;

namespace JonathanRobbins.MasterKey.Pipelines.UnlockItems
{
    public class GetItemArgs : PipelineArgs
    {
        public Item Item { get; set; }
        public IEnumerable<Item> ChildItems { get; set; } 
    }
}
