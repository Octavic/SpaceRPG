//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Inventory.cs">
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
    /// Defines an inventory
    /// </summary>
    public class Inventory
    {
        /// <summary>
        /// Dimensions of the inventory
        /// </summary>
        public int DimentionX;
        public int DimentionY;

        /// <summary>
        /// A collection of coordinate => contents
        /// </summary>
        public Dictionary<Vector2, IItem> Occupied;

        /// <summary>
        /// A collection of contents => base coordinate
        /// </summary>
        public Dictionary<IItem, Vector2> Contents;

        /// <summary>
        /// Try to remove an item
        /// </summary>
        /// <param name="clickedSpot">the slot that was clicked on</param>
        /// <returns>the item that was removed, null if not applicable</returns>
        public IItem TryRemoveItem(Vector2 clickedSpot)
        {
            IItem result = null;
            if (!this.Occupied.TryGetValue(clickedSpot, out result))
            {
                return null;
            }

            this.Contents.Remove(result);
            return result;
        }

        /// <summary>
        /// Try to add a new item to the inventory
        /// </summary>
        /// <param name="newItem">new item to be added</param>
        /// <returns>True if the new item can be added</returns>
        public bool TryAddItem(IItem newItem, Vector2 newItemCoordinate)
        {
            var newItemOccupied = new List<Vector2>();
            for (int x = 0; x < newItem.Dimensions.x; x++)
            {
                for (int y = 0; y < newItem.Dimensions.y; y++)
                {
                    newItemOccupied.Add(newItemCoordinate + new Vector2(x, y));
                }
            }

            // If there's collision, can't place
            foreach (var occupied in newItemOccupied)
            {
                if (this.Occupied.ContainsKey(occupied))
                {
                    return false;
                }
            }

            // No collision, apply
            this.Contents[newItem] = newItemCoordinate;
            foreach (var occupied in newItemOccupied)
            {
                this.Occupied[occupied] = newItem;
            }

            return true;
        }
    }
}
