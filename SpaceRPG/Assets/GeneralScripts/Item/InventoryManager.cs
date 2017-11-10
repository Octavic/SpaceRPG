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
        /// The inventory that currently contains the dragged item
        /// </summary>
        private InventoryBehavior _draggedItemSource;

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
            if (this._openedInventories == null)
            {
                this._openedInventories = new List<InventoryBehavior>();
            }

            if (!this._openedInventories.Contains(newInventory))
            {
                this._openedInventories.Add(newInventory);
            }
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
            if (this._openedInventories == null)
            {
                this._openedInventories = new List<InventoryBehavior>();
              }
        }

        /// <summary>
        /// Called once per frame
        /// </summary>
        protected void FixedUpdate()
        {
            var mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            // Move the existing item and attach it to mouse if possible
            if (this._draggedItem != null)
            {
                this._draggedItem.transform.position = mousePos + this._draggedItemOffset;
                if (Input.GetMouseButtonUp(0))
                {
                    // Use has released the item, check each opened inventories from top to bottom
                    var heldItemIndexPos = this._draggedItem.GetIndexCellWorldPosition();
                    Vector2? hoverIndex = null;
                    InventoryBehavior hoverInventory = null;

                    for (int i = 0; i < this._openedInventories.Count; i++)
                    {
                        var curHover = this._openedInventories[i].HoverCellCoordidnate(mousePos);
                        if (curHover != null)
                        {
                            hoverIndex = curHover;
                            hoverInventory = this._openedInventories[i];
                            break;
                        }
                    }

                    // Not releasing it over any opened inventory, destroy
                    if (hoverIndex == null)
                    {
                        this._draggedItemSource.TryRemoveItem(this._draggedItem.ItemData);
                        Destroy(this._draggedItem.gameObject);
                        this._draggedItem = null;
                    }
                    // Releasing over an opened inventory, try move the item
                    else
                    {
                        // If the dragged item does fit
                        if (hoverInventory.CanAddItem(this._draggedItem.ItemData, hoverIndex.Value))
                        {
                            // Remove item
                            this._draggedItemSource.TryRemoveItem(this._draggedItem.ItemData);

                            // Add new item
                            hoverInventory.AddItem(this._draggedItem.ItemData, hoverIndex.Value);
                        }

                        // Regardless of fit or not, release held item
                        Destroy(this._draggedItem.gameObject);
                        this._draggedItem = null;
                    }
                }
            }
            // No currently holding item, listen for mouse down
            else
            {
                if (Input.GetMouseButtonDown(0))
                {

                    Vector2? hoverIndex = null;
                    InventoryBehavior hoverInventory = null;

                    for (int i = 0; i < this._openedInventories.Count; i++)
                    {
                        var curResult = this._openedInventories[i].HoverCellCoordidnate(mousePos);
                        if (curResult != null)
                        {
                            hoverIndex = curResult;
                            hoverInventory = this._openedInventories[i];
                            break;
                        }
                    }

                    //  Mouse is hovering over something
                    if (hoverIndex != null)
                    {
                        // If left shift key is not held, then normal drag and drop
                        if (!Input.GetKey(KeyCode.LeftShift))
                        {
                            this._draggedItemSource = hoverInventory;
                            this._draggedItem = hoverInventory.Occupied[hoverInventory.InventoryData.Occupied[hoverIndex.Value]].GeneratePhantom();
                            this._draggedItemOffset = (Vector2)this._draggedItem.transform.position - mousePos;
                        }
                        // Left shift key is held, do quick add
                        else
                        {
                            // Inventory transfer is only possible with more than 2 opened inventories. 
                            // TODO: Use this as quick delete
                            if (this._openedInventories.Count >= 2)
                            {
                                var clickedItem = hoverInventory.Occupied[hoverInventory.InventoryData.Occupied[hoverIndex.Value]];
                                var destination = this._openedInventories[0] != hoverInventory ? this._openedInventories[0] : this._openedInventories[1];
                                var result = destination.InventoryData.CanQuickAddItem(clickedItem.ItemData);
                                if (result.HasValue)
                                {
                                    hoverInventory.TryRemoveItem(clickedItem.ItemData);
                                    destination.AddItem(clickedItem.ItemData, result.Value);
                                }
                            }
                        }
                    }

                }
            }
        }

        /// <summary>
        /// Moves the given inventory to the top of the opened inventories
        /// </summary>
        /// <param name="inv">Target inventory</param>
        private void MoveInventoryToTop(InventoryBehavior inv)
        {
            this._openedInventories.Remove(inv);
            this._openedInventories.Insert(0, inv);
        }
    }
}
