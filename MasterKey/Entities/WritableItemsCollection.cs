using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data.Items;

namespace MasterKey.Entities
{
    [Serializable]
    public class WritableItemsCollection
    {
        public IEnumerable<Item> WritableItems { get; set; }
        public IEnumerable<Item> UnwriteableItems { get; set; }


        public WritableItemsCollection()
        {
            WritableItems = new List<Item>();
            UnwriteableItems = new List<Item>();
        }
    }
}
