using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data.Items;
using Sitecore.Pipelines;

namespace MasterKey.Pipelines.UnlockItems
{
    public class GetItemArgs : PipelineArgs
    {
        public Item Item { get; set; }
        public IEnumerable<Item> ChildItems { get; set; } 
    }
}
