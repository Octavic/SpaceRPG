
namespace Assets.GeneralScripts.Item
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;

    /// <summary>
    /// The behavior script for an inventory item
    /// </summary>
    public class InventoryBehavior : MonoBehaviour
    {
        /// <summary>
        /// The data for the inventory
        /// </summary>
        public Inventory InventoryData;

        /// <summary>
        /// A collection of Item => Item behaviors
        /// </summary>
        public Dictionary<IItem, ItemBehavior> Behaviors;

        /// <summary>
        /// Prefab for the item behaviour
        /// </summary>
        public GameObject ItemPrefab;

        /// <summary>
        /// Size of the item's grid
        /// </summary>
        public float ItemGridSize;

        /// <summary>
        /// Creates a new item prefab
        /// </summary>
        /// <param name="item">new item</param>
        private void CreateNewItem(IItem item)
        {
            var newItem = Instantiate(this.ItemPrefab, this.transform);
            var newItemBehavior = newItem.GetComponent<ItemBehavior>();
            newItemBehavior.ItemData = item;
            newItemBehavior.MoveIndexSquareToLocalCoordinate(this.InventoryData.Contents[item]);
        }
    }
}
