
namespace Assets.GeneralScripts.Item
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;

    /// <summary>
    /// Describes the behavior of a container
    /// </summary>
    public class ContainerBehavior : MonoBehaviour
    {
        /// <summary>
        /// The respective inventory UI
        /// </summary>
        public InventoryBehavior InventoryUI;

        /// <summary>
        /// Prefab for an inventory UI
        /// </summary>
        public GameObject InventoryUIPrefab;

        /// <summary>
        /// Assigns the new inventory
        /// </summary>
        /// <param name="inventory">The new inventory</param>
        public void AssignInventory(Inventory inventory)
        {
            var newUI = Instantiate(this.InventoryUIPrefab).GetComponent<InventoryBehavior>();
            newUI.transform.position = Config.DefaultContainerUIPositioin;
            newUI.InventoryData = inventory;
            newUI.RenderInventory();
            this.InventoryUI = newUI;
        }
    }
}
