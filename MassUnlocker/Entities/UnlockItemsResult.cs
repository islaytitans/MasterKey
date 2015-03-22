using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data.Items;

namespace ChildrenUnlocker.Entities
{
    [Serializable]
    public class UnlockItemsResult
    {
        public IEnumerable<Item> UnlockedItems { get; set; }
        public IEnumerable<Item> FailedUnlockedItems { get; set; }
        public IEnumerable<Item> UnwritableItems { get; set; }
        public string AlertMessage
        {
            get
            {
                var alert = new StringBuilder(string.Empty);

                if (UnlockedItems.Any())
                {
                    alert.Append(string.Format("Successfully unlocked {0} item{1}.", UnlockedItems.Count(),
                        UnlockedItems.Count() > 1 ? "s" : string.Empty));
                }
                if (FailedUnlockedItems.Any())
                {
                    alert.Append(string.Format("Failed to unlock {0} item{1}.", FailedUnlockedItems.Count(),
                        UnlockedItems.Count() > 1 ? "s" : string.Empty));
                }
                if (UnwritableItems.Any())
                {
                    alert.Append(string.Format("You don't have permission to unlock the following item{0} {1}", 
                        UnwritableItems.Count() > 1 ? "s" : string.Empty,
                        UnlockedItems.Select(i => i.Name))); // TODO
                }

                return alert.ToString();
            }
        }

        public UnlockItemsResult()
        {
            UnlockedItems = new List<Item>();
            FailedUnlockedItems = new List<Item>();
            UnwritableItems = new List<Item>();
        }
    }
}
