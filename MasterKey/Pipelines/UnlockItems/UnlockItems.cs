﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasterKey.Entities;
using MasterKey.Interfaces;
using Sitecore.Diagnostics;
using Sitecore.StringExtensions;
using Sitecore.Web.UI.Sheer;

namespace MasterKey.Pipelines.UnlockItems
{
    public class UnlockItems
    {
        private string _unlockChildrenMessage;
        private string UnlockChildrenMessage
        {
            get
            {
                if (_unlockChildrenMessage.IsNullOrEmpty())
                {
                    _unlockChildrenMessage = "Unlock child items?";
                }
                return _unlockChildrenMessage;
            }
            set { _unlockChildrenMessage = value; }
        }

        private string _unlockChildrenMessageWidth;
        private string UnlockChildrenMessageWidth
        {
            get
            {
                if (_unlockChildrenMessageWidth.IsNullOrEmpty())
                {
                    _unlockChildrenMessageWidth = "200";
                }
                return _unlockChildrenMessageWidth;
            }
            set { _unlockChildrenMessageWidth = value; }
        }

        private string _unlockChildrenMessageHeight;
        private string UnlockChildrenMessageHeight
        {
            get
            {
                if (_unlockChildrenMessageHeight.IsNullOrEmpty())
                {
                    _unlockChildrenMessageHeight = "200";
                }
                return _unlockChildrenMessageHeight;
            }
            set { _unlockChildrenMessageHeight = value; }
        }

        private IUnlockUtility _unlockUtility = new UnlockUtility();

        public void DetermineIfItemHasUnlockableChildren(UnlockItemArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            Assert.ArgumentNotNull(args.Item, "args.Item");

            var lockedChildren = _unlockUtility.GetLockedChildren(args.Item).ToList();
            if (!lockedChildren.Any())
            {
                args.AbortPipeline();
                return;
            }

            var writeableItemsCollection = _unlockUtility.SortWritableItems(lockedChildren);
            if (!writeableItemsCollection.WritableItems.Any())
            {
                args.AbortPipeline();
                return;
            }
        }

        public void ConfirmUnlockChildren(UnlockItemArgs args)
        {
            Assert.ArgumentNotNull(args, "args");

            if (!args.IsPostBack)
            {
                SheerResponse.YesNoCancel(UnlockChildrenMessage, UnlockChildrenMessageWidth, UnlockChildrenMessageHeight);
                args.WaitForPostBack();
            }
            else if (args.HasResult)
            {
                args.UnlockChildren = string.Equals(args.Result, "yes", StringComparison.CurrentCultureIgnoreCase);
                args.IsPostBack = false;
            }
        }

        public void GetChildrenToUnlock(UnlockItemArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            if (!args.UnlockChildren)
            {
                args.AbortPipeline();
                return;
            }

            args.ChildItems = args.Item.Children.ToList();
        }

        public void UnlockChildItems(UnlockItemArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            Assert.ArgumentNotNull(args.ChildItems, "args.ChildItems");
            var result = _unlockUtility.UnlockItems(args.ChildItems);

            if (result.HasLockedItems.HasValue
                && result.HasLockedItems.Value)
            {
                SheerResponse.Alert("No locked child items were found",
                    false, MasterKey.UnlockUtility.ModalTitle);
                return;
            }
            else
            {
                SheerResponse.Alert(String.Format("<div>{0}<div>", result.AlertMessageHtml),
                    false, MasterKey.UnlockUtility.ModalTitle);
            }
        }
    }
}
