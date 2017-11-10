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
        /// Creates the only isntance of the <see cref="ItemCatagory"/> class
        /// </summary>
        static ItemCatagory()
        {
            _itemData.Add(new NormalItem(0, "Gold Nugget", new Vector2(1, 1), 100, ItemRarityEnum.Normal));
            _itemData.Add(new NormalItem(1, "Yarn", new Vector2(2, 2), 200, ItemRarityEnum.Normal));

            foreach (var item in _itemData)
            {
                _items[item.ItemId] = item;
            }
        }

        /// <summary>
        /// Gets the item data with the given item id, null if invalid
        /// </summary>
        /// <param name="itemId">Item id</param>
        /// <returns>result item data</returns>
        public static IItem GetItem(int itemId)
        {
            IItem result;
            if (!_items.TryGetValue(itemId, out result))
            {
                return null;
            }

            return result.Clone();
        }

        /// <summary>
        /// The list of items
        /// </summary>
        private static Dictionary<int, IItem> _items = new Dictionary<int, IItem>() ;

        /// <summary>
        /// The collection of items
        /// TODO: Move this to a centralized file
        /// </summary>
        private static IList<IItem> _itemData = new List<IItem>();
    }
}
