using System;
using System.Collections.Generic;
using Sitecore.Data.Items;

namespace JonathanRobbins.MasterKey.Entities
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
