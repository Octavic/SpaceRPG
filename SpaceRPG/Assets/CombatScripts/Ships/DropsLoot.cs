//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="DropsLoot.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.CombatScripts.Ships
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;
    using GeneralScripts.Utility;
    using GeneralScripts.Item;

    /// <summary>
    /// Defines an item in the loot table
    /// </summary>
    [Serializable]
    public struct DropTableItem
    {
        /// <summary>
        /// The item Id
        /// </summary>
        public int ItemId;

        /// <summary>
        /// The weight/chance of it dropping
        /// </summary>
        public int Weight;
    }

    /// <summary>
    /// Defines an item that creates a loot container when destroyed
    /// </summary>
    public class DropsLoot : MonoBehaviour
    {
        /// <summary>
        /// The drop table
        /// </summary>
        public List<DropTableItem> DropTable;

        /// <summary>
        /// The dimensions of the result item
        /// </summary>
        /// <returns></returns>
        public Vector2 ResultContainerDimensions;

        /// <summary>
        /// Prefab for the container
        /// </summary>
        public GameObject ContainerPrefab;

        /// <summary>
        /// How many items to generate. 0 or below meaning as much as can fit
        /// </summary>
        public int GenerateCount;

        /// <summary>
        /// Generates the loot container
        /// </summary>
        public void GenerateContainer()
        {
            var weightedList = new WeightedRandomList<int>();
            foreach (var item in this.DropTable)
            {
                weightedList.AddNewItem(item.ItemId, item.Weight);
            }

            // Try to fill up the inventory until we fail 5 times in total
            int failedCount = 0;
            int succeedCount = 0;
            var newInventory = new Inventory((int)this.ResultContainerDimensions.x, (int)this.ResultContainerDimensions.y);
            while (failedCount < 5 && GenerateCount>0 && succeedCount < GenerateCount)
            {
                var newItem = ItemCatagory.GetItem(weightedList.GetRandomItem());
                if (newItem == null)
                {
                    succeedCount ++;
                    failedCount++;
                }

                var indexCoor = newInventory.CanQuickAddItem(newItem);
                if (!indexCoor.HasValue)
                {
                    failedCount++;
                    continue;
                }
                else
                {
                    succeedCount++;
                    newInventory.AddItem(newItem, indexCoor.Value);
                }
            }

            for (int i = 0; i < 10; i++)
            {
                var newContainer = Instantiate(this.ContainerPrefab).GetComponent<ContainerBehavior>();
                newContainer.transform.position = this.transform.position;
                newContainer.AssignInventory(newInventory);
            }
        }
    }
}
