using System;
using System.Collections.Generic;
using Sitecore.Data.Items;
using Sitecore.Web.UI.Sheer;

namespace JonathanRobbins.MasterKey.Pipelines.UnlockItems
{
    [Serializable]
    public class UnlockItemArgs : ClientPipelineArgs
    {
        public Item Item { get; set; }
        public IEnumerable<Item> ChildItems { get; set; }
        public bool UnlockChildren { get; set; }
    }
}
