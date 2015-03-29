using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data.Items;

namespace MasterKey.Entities
{
    [Serializable]
    public class UnlockItemsResult
    {
        public bool? HasLockedItems { get; set; }
        public IEnumerable<Item> UnlockedItems { get; set; }
        public IEnumerable<Item> FailedUnlockedItems { get; set; }
        public IEnumerable<Item> UnwritableItems { get; set; }

        public string UnlockedAlertMessage
        {
            get
            {
                return string.Format("Successfully unlocked {0} item{1}.", UnlockedItems.Count(),
                    UnlockedItems.Count() > 1 ? "s" : string.Empty);
            }
        }
        public string FailedUnlockAlertMessage
        {
            get
            {
                return string.Format("Failed to unlock {0} item{1}.", FailedUnlockedItems.Count(),
                    UnlockedItems.Count() > 1 ? "s" : string.Empty);
            }
        }
        public string UnwritableAlertMessage
        {
            get
            {
                return string.Format("You don't have permission to unlock the following item{0}:",
                    UnwritableItems.Count() > 1 ? "s" : string.Empty);
            }
        }

        public string AlertMessageHtml
        {
            get
            {
                var alert = new StringBuilder(string.Empty);

                if (UnlockedItems.Any())
                {
                    alert.Append("<p>" + UnlockedAlertMessage + "</p>");
                }
                if (FailedUnlockedItems.Any())
                {
                    alert.Append("<p>" + FailedUnlockAlertMessage + "</p>");
                }
                if (UnwritableItems.Any())
                {
                    alert.Append(string.Format("<p>" + UnwritableAlertMessage + "</p><div style=\"padding-left: 2%;\"><ul>{0}</ul></div>", 
                        string.Join(string.Empty, UnlockedItems.Select(i => "<li>" + i.Name + "</li>").ToArray())));
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
