//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="InventoryManager.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.GeneralScripts.Item
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using UnityEngine;
    using Utility;

    /// <summary>
    /// Manages inventories as well as how to transfer items in between
    /// </summary>
    public class InventoryManager : MonoBehaviour
    {
        /// <summary>
        /// Gets the current singleton instance of the <see cref="InventoryManager"/> class 
        /// </summary>
        public static InventoryManager CurrentInstance {
            get
            {
                if (InventoryManager._currentInstance == null)
                {
                    InventoryManager._currentInstance = GameObjectFinder.FindGameObjectWithTag(Tags.InventoryManager).GetComponent<InventoryManager>();
                }

                return InventoryManager._currentInstance;
            }
        }
        
        /// <summary>
        /// The current instance
        /// </summary>
        private static InventoryManager _currentInstance;

        /// <summary>
        /// A list of inventory items
        /// </summary>
        private List<InventoryBehavior> _openedInventories;

        /// <summary>
        /// If the player is currently clicking and dragging an item around
        /// </summary>
        private ItemBehavior _draggedItem;

        /// <summary>
        /// The offset of the dragged item's coordinate to mouse offset
        /// </summary>
        private Vector2 _draggedItemOffset;

        /// <summary>
        /// Opens up a new inventory
        /// </summary>
        /// <param name="newInventory">new inventory to be opened</param>
        public void OpenInventory(InventoryBehavior newInventory)
        {
            this._openedInventories.Add(newInventory);
        }

        /// <summary>
        /// Closes the given inventory
        /// </summary>
        /// <param name="targetInventory">Target inventory to be closed</param>
        public void CloseInventory(InventoryBehavior targetInventory)
        {
            this._openedInventories.Remove(targetInventory);
        }

        /// <summary>
        /// Used for initialization
        /// </summary>
        protected void Start()
        {
            this._openedInventories = new List<InventoryBehavior>();
        }

        /// <summary>
        /// Called once per frame
        /// </summary>
        protected void FixedUpdate()
        {
            foreach (var inventory in this._openedInventories)
            {
                var hoverCoor = inventory.MouseHoverCell(Input.mousePosition);
                if (hoverCoor!= null)
                {
                }
            }
        }
    }
}
