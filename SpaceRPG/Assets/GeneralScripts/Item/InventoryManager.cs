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
            var mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            // Move the existing item and attach it to mouse if possible
            if (this._draggedItem != null)
            {
                this._draggedItem.transform.position = mousePos + this._draggedItemOffset;
                if (Input.GetMouseButtonUp(0))
                {
                    // Use has released the item, check each opened inventories from top to bottom
                    var heldItemIndexPos = this._draggedItem.GetIndexCellCoordinate();
                    Vector2? hoverIndex = null;
                    InventoryBehavior hoverInventory = null;

                    for (int i = 0; i < this._openedInventories.Count; i++)
                    {
                        var curResult = this._openedInventories[i].MouseHoverCell(mousePos);
                        if (curResult != null)
                        {
                            hoverIndex = curResult;
                            hoverInventory = this._openedInventories[i];
                            break;
                        }
                    }

                    // Not releasing it over any opened inventory, destroy
                    if (hoverIndex == null)
                    {
                        this._draggedItemSource.InventoryData.TryRemoveItem(this._draggedItem.ItemData);
                    }
                    // Releasing over an opened inventory, try move the item
                    else
                    {
                        // If item does not fit, remove dragged item
                        if (!hoverInventory.TryAddItem(this._draggedItem.ItemData, hoverIndex.Value))
                        {
                            Destroy(this._draggedItem.gameObject);
                        }
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
                        var curResult = this._openedInventories[i].MouseHoverCell(mousePos);
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

        /// <summary>
        /// Finds the first inventory that the mouse/item is hovering over
        /// </summary>
        /// <param name="mousePos">Mouse/item position</param>
        /// <returns>Possible hovering index, null if not found</returns>
        private Vector2? FindFirstHitInventory(Vector2 mousePos)
        {
            

            return null;
        }
    }
}
