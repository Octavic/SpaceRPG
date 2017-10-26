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
    using Settings;
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
        public Vector2? MouseHoverCell(Vector2 mousePosition)
        {
            var relativePosition = mousePosition - new Vector2
                (
                    this.transform.position.x - GeneralSettings.ItemGridSize / 2,
                    this.transform.position.y - GeneralSettings.ItemGridSize / 2
                );
            var xCoor = relativePosition.x / GeneralSettings.ItemGridSize;
            var yCoor = relativePosition.y / GeneralSettings.ItemGridSize;
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
            for (int xPos = 0; xPos < this.InventoryData.DimentionX; xPos++)
            {
                for (int yPos = 0; yPos < this.InventoryData.DimentionY; yPos++)
                {
                    var pos = new Vector2(xPos, yPos);
                    var newGrid = Instantiate(this.GridCellPrefab, this._gridTransform);
                    newGrid.transform.localPosition = pos * GeneralSettings.ItemGridSize;
                    newGrid.GetComponent<Image>().color = this.RarityColors[0];
                }
            }
        }

        /// <summary>
        /// Try  to add an item
        /// </summary>
        /// <param name="newItem">new item to be added</param>
        /// <param name="indexCoordinate">The coordinate for the item to be added</param>
        /// <returns>True if the operation succeed</returns>
        public bool TryAddItem(IItem newItem, Vector2 indexCoordinate)
        {
            if (this.InventoryData.TryAddItem(newItem, indexCoordinate))
            {
                var newItemObject = this.CreateNewItem(newItem);
                newItemObject.transform.localPosition = GeneralSettings.ItemGridSize * (indexCoordinate+newItem.Dimensions / 2 + this._itemOffset);
                this.Occupied[newItem] = newItemObject.GetComponent<ItemBehavior>();
                return true;
            }

            return false;
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
            this._behaviors = new Dictionary<IItem, ItemBehavior>();
            var grid = new GameObject("Grid");
            grid.gameObject.transform.parent = this.transform;
            grid.gameObject.transform.localPosition = Vector3.zero;
            this._gridTransform = grid.transform;

            var items = new GameObject("Items");
            items.gameObject.transform.parent = this.transform;
            items.gameObject.transform.localPosition = Vector3.zero;
            this._itemsTransform = items.transform;

            this.InventoryData = new Inventory(3,3);

            var newItem = new NormalItem(0, "Gold Nugget", new Vector2(1, 1), 10, ItemRarityEnum.Normal);
            this.TryAddItem(newItem, new Vector2(0,0));

            var yarn = new NormalItem(1, "Yarn", new Vector2(2, 2), 20, ItemRarityEnum.Uncommon);
            this.TryAddItem(yarn, new Vector2(1, 1));

            InventoryManager.CurrentInstance.OpenInventory(this);
            this.RenderInventory();
        }
    }
}
