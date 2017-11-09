//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ItemBehavior.cs">
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
    /// The behavior script for an item
    /// </summary>
    public class ItemBehavior : MonoBehaviour
    {
        /// <summary>
        /// The item data
        /// </summary>
        public IItem ItemData;

        /// <summary>
        /// If this behavior is an actual item in inventory or if it's a phantom
        /// </summary>
        public ItemBehavior RealCopy;

        /// <summary>
        /// Sets the sprite of the item
        /// </summary>
        /// <param name="sprite">The sprite</param>
        public void SetSprite(Sprite sprite)
        {
            this.GetComponent<Image>().sprite = sprite;
        }

        /// <summary>
        /// Gets the coordinate of the index square based on the item's current location
        /// </summary>
        /// <returns>Global position of the index square</returns>
        public Vector2 GetIndexCellWorldPosition()
        {
            var halfGrid = Config.ItemGridSize / 2;
            var result = (Vector2)this.transform.position - this.ItemData.Dimensions* halfGrid + new Vector2(halfGrid, halfGrid);
            return result;
        }

        /// <summary>
        /// Generates the phantom when dragging
        /// </summary>
        /// <returns>The generated phantom</returns>
        public ItemBehavior GeneratePhantom()
        {
            var newItem = Instantiate(this.gameObject, this.transform.parent).GetComponent<ItemBehavior>();
            newItem.RealCopy = this;
            newItem.ItemData = this.ItemData;
            this.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            newItem.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            return newItem;
        }

        /// <summary>
        /// Called when the item is destroyed
        /// </summary>
        protected void OnDestroy()
        {
            if (this.RealCopy != null)
            {
                this.RealCopy.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
        }
    }
}
