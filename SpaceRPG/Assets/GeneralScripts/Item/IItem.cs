//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IItem.cs">
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

    public interface IItem
    {
        /// <summary>
        /// The item Id
        /// </summary>
        int ItemId { get; }

        /// <summary>
        /// Gets the name of the item
        /// </summary>
        string ItemName { get; }

        /// <summary>
        /// Gets the dimensions of the item
        /// </summary>
        Vector2 Dimensions { get; }

        /// <summary>
        /// Base value for the item
        /// </summary>
        float BaseSellValue { get; }
        
        /// <summary>
        /// Gets the rarity of an item
        /// </summary>
        ItemRarityEnum Rarity { get; }

        /// <summary>
        /// Clones the item and return an exact copy
        /// </summary>
        /// <returns>The cloned item</returns>
        IItem Clone();
    }
}
