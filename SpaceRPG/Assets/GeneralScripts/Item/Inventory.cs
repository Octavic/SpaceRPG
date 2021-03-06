﻿//  --------------------------------------------------------------------------------------------------------------------
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
        public int DimentionX { get; private set; }
        public int DimentionY { get; private set; }

        /// <summary>
        /// A collection of coordinate => contents
        /// </summary>
        public Dictionary<Vector2, IItem> Occupied;

        /// <summary>
        /// A collection of contents => base coordinate
        /// </summary>
        public Dictionary<IItem, Vector2> Contents;

        /// <summary>
        /// Creates a new instance of the <see cref="Inventory"/> class
        /// </summary>
        /// <param name="dimentionX">The width</param>
        /// <param name="dimentionY">The height</param>
        public Inventory(int dimentionX, int dimentionY)
        {
            this.DimentionX = dimentionX;
            this.DimentionY = dimentionY;
            this.Occupied = new Dictionary<Vector2, IItem>();
            this.Contents = new Dictionary<IItem, Vector2>();
        }

        /// <summary>
        /// Try to remove an item
        /// </summary>
        /// <param name="item">the item to be removed</param>
        /// <returns>True if successful</returns>
        public bool TryRemoveItem(IItem item)
        {
            Vector2 indexCell;
            if (!this.Contents.TryGetValue(item, out indexCell))
            {
                return false;
            }
            var occupied = this.GetItemOccupied(item, indexCell);
            foreach (var cell in occupied)
            {
                this.Occupied.Remove(cell);
            }

            this.Contents.Remove(item);
            return true;
        }

        /// <summary>
        /// Try to quick add an item
        /// </summary>
        /// <param name="newitem">The new item to be added</param>
        /// <returns>The index coordinate if successful, null if the new item cannot fits</returns>
        public Vector2? CanQuickAddItem(IItem newitem)
        {
            // Search from left to right, then top to down
            for (int y = this.DimentionY - (int)newitem.Dimensions.y; y >= 0; y--)
            {
                for (int x = 0; x <= this.DimentionX - newitem.Dimensions.x; x++)
                {
                    var checkCoor = new Vector2(x, y);
                    if (this.CanAddItem(newitem, checkCoor))
                    {
                        return checkCoor;
                    }
                }
            }

            // Can't fit anywhere
            return null;
        }

        /// <summary>
        /// Try to add a new item to the inventory
        /// </summary>
        /// <param name="newItem">new item to be added</param>
        /// <returns>True if the new item can be added</returns>
        public bool CanAddItem(IItem newItem, Vector2 newItemCoordinate)
        {
            // If the new item coordinate is out of bounds
            if (newItemCoordinate.x < 0 || newItemCoordinate.y < 0)
            {
                return false;
            }

            var cornerCoordiante = newItemCoordinate + newItem.Dimensions;
            if (cornerCoordiante.x > this.DimentionX || cornerCoordiante.y > this.DimentionY)
            {
                return false;
            }

            // Iterate through new item's cells for conflicts
            for (int x = 0; x < newItem.Dimensions.x; x++)
            {
                for (int y = 0; y < newItem.Dimensions.y; y++)
                {
                    var curCheckCoor = newItemCoordinate + new Vector2(x, y);
                    IItem collided;

                    // If there's collision, check collision
                    if (this.Occupied.TryGetValue(curCheckCoor, out collided))
                    {
                        // If it's the same item just being moved, it won't collide with itself
                        if (collided != newItem)
                        {
                            return false;
                        }
                    }
                }
            }

            // Loop finished with no collision, add
            return true;
        }

        /// <summary>
        /// Adds a new item to the inventory, assuming that collision has been resolved with CanAddItem(..., ...)
        /// </summary>
        /// <param name="newItem">The new item to be added</param>
        /// <param name="indexCoordinaate">The index coordinate for the new item</param>
        public void AddItem(IItem newItem, Vector2 indexCoordinaate)
        {
            this.Contents[newItem] = indexCoordinaate;
            for (int x = 0; x < newItem.Dimensions.x; x++)
            {
                for (int y = 0; y < newItem.Dimensions.y; y++)
                {
                    this.Occupied[indexCoordinaate + new Vector2(x, y)] = newItem;
                }
            }
        }

        /// <summary>
        /// Calculates all of the occupied cells
        /// </summary>
        /// <param name="item">Item data</param>
        /// <param name="indexCoordinate">The index coordinate</param>
        /// <returns>A list of all cells occupied by the item</returns>
        private IList<Vector2> GetItemOccupied(IItem item, Vector2 indexCoordinate)
        {
            var result = new List<Vector2>();

            for (int x = 0; x < item.Dimensions.x; x++)
            {
                for (int y = 0; y < item.Dimensions.y; y++)
                {
                    result.Add(new Vector2(indexCoordinate.x + x, indexCoordinate.y + y));
                }
            }

            return result;
        }
    }
}
