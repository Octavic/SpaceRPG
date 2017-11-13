//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="InventoryBehavior.cs">
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
    using GeneralScripts.Utility;
    using UnityEngine.UI;

    /// <summary>
    /// The behavior script for an inventory item
    /// </summary>
    public class InventoryBehavior : MonoBehaviour
    {
        /// <summary>
        /// Prefab for the item behaviour
        /// </summary>
        public GameObject ItemPrefab;

        /// <summary>
        /// Prefab for the grid cell
        /// </summary>
        public GameObject GridCellPrefab;

        /// <summary>
        /// Outline for the background
        /// </summary>
        public GameObject BackgroundOutline;

        /// <summary>
        /// The background of the UI
        /// </summary>
        public GameObject BackgroundObject;

        /// <summary>
        /// The button for closing this inventory
        /// </summary>
        public GameObject CloseButton;

        /// <summary>
        /// A collection of rarity colors from empty, to least rare to most rare
        /// </summary>
        public List<Color> RarityColors;

        /// <summary>
        /// The data for the inventory
        /// </summary>
        public Inventory InventoryData { get; set; }

        /// <summary>
        /// A collection of Item => Item behaviors
        /// </summary>
        public Dictionary<IItem, ItemBehavior> Occupied { get; private set; }

        /// <summary>
        /// Transform for the grid
        /// </summary>
        private Transform _gridTransform;

        /// <summary>
        /// Transform for the 'items' game object
        /// </summary>
        private Transform _itemsTransform;

        /// <summary>
        /// The offset for items
        /// </summary>
        private Vector2 _itemOffset = new Vector2(-0.5f, -0.5f);

        /// <summary>
        /// Gets the index of the square that the  mouse is hovering over
        /// </summary>
        /// <param name="mousePosition">Mouse position</param>
        /// <returns>The index of the square that the mouse is hovering over, null if the mouse is not inside this inventory</returns>
        public Vector2? HoverCellCoordidnate(Vector2 mousePosition)
        {
            var relativePosition = mousePosition - new Vector2
                (
                    this.transform.position.x - Config.ItemGridSize / 2,
                    this.transform.position.y - Config.ItemGridSize / 2
                );
            var xCoor = relativePosition.x / Config.ItemGridSize;
            var yCoor = relativePosition.y / Config.ItemGridSize;
            if (xCoor < 0 || yCoor < 0 || xCoor >= this.InventoryData.DimentionX || yCoor >= this.InventoryData.DimentionY)
            {
                return null;
            }

            var result = new Vector2((int)xCoor, (int)yCoor);
            return result;
        }

        /// <summary>
        /// Renders the given inventory
        /// </summary>
        public void RenderInventory()
        {
            // Set the background and boarder for the background
            var width = this.InventoryData.DimentionX * Config.ItemGridSize + Config.InventoryBoarder * 2;
            var height = this.InventoryData.DimentionY * Config.ItemGridSize + Config.InventoryBoarder * 2 + Config.InventoryHeaderHeight;
            var posX = (this.InventoryData.DimentionX - 1) * Config.ItemGridSize / 2;
            var posY = (this.InventoryData.DimentionY - 1) * Config.ItemGridSize / 2 + Config.InventoryHeaderHeight /2  ;
            var backgroundRect = this.BackgroundObject.GetComponent<RectTransform>() ;
            backgroundRect.sizeDelta = new Vector2(width, height);
            backgroundRect.transform.localPosition = new Vector2(posX, posY);
            var outlineRect = this.BackgroundOutline.GetComponent<RectTransform>();
            outlineRect.sizeDelta = new Vector2(width + Config.InventoryBoarderOutline * 2, height + Config.InventoryBoarderOutline * 2);
            outlineRect.transform.localPosition = new Vector3(posX, posY);

            // Sets the position of the close button to top right
            var buttonPosX = this.InventoryData.DimentionX * Config.ItemGridSize - Config.ItemGridSize / 2 - Config.CloseInventoryButtonSize / 2 ;
            var buttonPosY = this.InventoryData.DimentionY * Config.ItemGridSize - Config.ItemGridSize / 2 - Config.CloseInventoryButtonSize / 2 + Config.InventoryHeaderHeight;
            this.CloseButton.transform.localPosition = new Vector2(buttonPosX, buttonPosY);
            
            // Remove old
            this._itemsTransform.gameObject.DestroyAllChildren();
            this._gridTransform.gameObject.DestroyAllChildren();
            this.Occupied = new Dictionary<IItem, ItemBehavior>();

            // Draw the grid
            for (int xPos = 0; xPos < this.InventoryData.DimentionX; xPos++)
            {
                for (int yPos = 0; yPos < this.InventoryData.DimentionY; yPos++)
                {
                    var pos = new Vector2(xPos, yPos);
                    var newGrid = Instantiate(this.GridCellPrefab, this._gridTransform);
                    newGrid.transform.localPosition = pos * Config.ItemGridSize;
                    newGrid.GetComponent<Image>().color = this.RarityColors[0];
                }
            }

            // Draw the items and set them in the occupied dictionary
            foreach (var item in this.InventoryData.Contents)
            {
                var newItemObject = this.CreateNewItem(item.Key);
                this.Occupied[item.Key] = newItemObject.GetComponent<ItemBehavior>();
                newItemObject.transform.localPosition = Config.ItemGridSize * (item.Value + item.Key.Dimensions / 2 + this._itemOffset);
            }
        }

        /// <summary>
        /// Assigns an inventory for this behavior to represent
        /// </summary>
        /// <param name="inventoryData">inventory data</param>
        public void AssignInventory(Inventory inventoryData)
        {
            this.InventoryData = inventoryData;
        }

        /// <summary>
        /// Try to add an item
        /// </summary>
        /// <param name="newItem">new item to be added</param>
        /// <param name="indexCoordinate">The coordinate for the item to be added</param>
        /// <returns>True if the operation succeed</returns>
        public bool CanAddItem(IItem newItem, Vector2 indexCoordinate)
        {
            return this.InventoryData.CanAddItem(newItem, indexCoordinate);
        }

        /// <summary>
        /// Adds a new item to the inventory
        /// </summary>
        /// <param name="newItem">New item  to be added</param>
        /// <param name="indexCoordinate">The index coordinate for the new item</param>
        public void AddItem(IItem newItem, Vector2 indexCoordinate)
        {
            this.InventoryData.AddItem(newItem, indexCoordinate);
            this.RenderInventory();
        }

        /// <summary>
        /// Try  to remove an item from inventory
        /// </summary>
        /// <param name="targetItem">Item targeted for removal</param>
        /// <returns>True if successful</returns>
        public bool TryRemoveItem(IItem targetItem)
        {
            if (!this.InventoryData.TryRemoveItem(targetItem))
            {
                return false;
            }

            this.RenderInventory();
            return true;
        }

        /// <summary>
        /// Opens the inventory
        /// </summary>
        public void Open()
        {
            InventoryManager.CurrentInstance.OpenInventory(this);
            this.gameObject.SetActive(true);
        }

        /// <summary>
        /// Closes the inventory
        /// </summary>
        public void Close()
        {
            InventoryManager.CurrentInstance.CloseInventory(this);
            this.gameObject.SetActive(false);
        }

        /// <summary>
        /// Creates a new item prefab
        /// </summary>
        /// <param name="item">new item</param>
        private GameObject CreateNewItem(IItem item)
        {
            var newItem = Instantiate(this.ItemPrefab, this._itemsTransform);
            newItem.transform.localScale = item.Dimensions;
            var newItemBehavior = newItem.GetComponent<ItemBehavior>();
            newItemBehavior.ItemData = item;
            newItemBehavior.SetSprite(SpriteManager.CurrentInstance.GetItemSprite(item.ItemId));
            return newItem;
        }

        /// <summary>
        /// Used for initialization
        /// </summary>
        protected void Start()
        {
            this.Occupied = new Dictionary<IItem, ItemBehavior>();
            var grid = new GameObject("Grid");
            grid.gameObject.transform.parent = this.transform;
            grid.gameObject.transform.localPosition = Vector3.zero;
            this._gridTransform = grid.transform;

            var items = new GameObject("Items");
            items.gameObject.transform.parent = this.transform;
            items.gameObject.transform.localPosition = Vector3.zero;
            this._itemsTransform = items.transform;

            if (this.InventoryData != null)
            {
                this.RenderInventory();
            }
        }
    }
}
