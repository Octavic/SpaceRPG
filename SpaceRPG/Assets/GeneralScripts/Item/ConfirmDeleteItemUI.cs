//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ConfirmDeleteItemUI.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.GeneralScripts.Item
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// The UI for confirm jettison items
    /// </summary>
    public class ConfirmDeleteItemUI : MonoBehaviour
    {
        /// <summary>
        /// Name for the item
        /// </summary>
        public Text ItemText;

        /// <summary>
        /// Item to be destroyed
        /// </summary>
        public IItem TargetItem { get; set; }

        /// <summary>
        /// Where the item comes from
        /// </summary>
        public InventoryBehavior SourceInventory { get; set; }

        /// <summary>
        /// Triggered when the user clicks confirm
        /// </summary>
        public void Confirm()
        {
            this.SourceInventory.TryRemoveItem(this.TargetItem);
            Destroy(this.gameObject);
        }

        /// <summary>
        /// Cancels the action
        /// </summary>
        public void Cancel()
        {
            Destroy(this.gameObject);
        }

        /// <summary>
        /// Renders the UI
        /// </summary>
        public void RenderUI()
        {
            this.ItemText.text = this.TargetItem.ItemName;
            this.ItemText.color = this.SourceInventory.RarityColors[(int)this.TargetItem.Rarity];
        }
    }
}
