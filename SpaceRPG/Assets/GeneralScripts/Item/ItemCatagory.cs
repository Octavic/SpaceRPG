//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ItemCatagory.cs">
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

    /// <summary>
    /// A list of all items in the game
    /// </summary>
    public static class ItemCatagory
    {
        /// <summary>
        /// Gets the item data with the given item id
        /// </summary>
        /// <param name="itemId">Item id</param>
        /// <returns>result item data</returns>
        public static IItem GetItem(int itemId)
        {
            return ItemCatagory._items[itemId].Clone();
        }

        /// <summary>
        /// The list of items
        /// </summary>
        private static List<IItem> _items = new List<IItem>() {
            new NormalItem(0, "Gold Nugget", new Vector2(1,1), 100, ItemRarityEnum.Normal),
            new NormalItem(1, "Yarn", new Vector2(2,2), 200, ItemRarityEnum.Uncommon)
        };
    }
}
