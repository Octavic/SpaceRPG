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
        /// Sets the sprite of the item
        /// </summary>
        /// <param name="sprite">The sprite</param>
        public void SetSprite(Sprite sprite)
        {
            this.GetComponent<Image>().sprite = sprite;
        }
    }
}
