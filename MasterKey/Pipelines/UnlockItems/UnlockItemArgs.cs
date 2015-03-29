using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data.Items;
using Sitecore.Web.UI.Sheer;

namespace MasterKey.Pipelines.UnlockItems
{
    public class UnlockItemArgs : ClientPipelineArgs
    {
        public Item Item { get; set; }
        public IEnumerable<Item> ChildItems { get; set; }
        public bool UnlockChildren { get; set; }
    }
}
