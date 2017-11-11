//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ContainerBehavior.cs">
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
    using Utility;

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
        /// The player
        /// </summary>
        private GameObject _player;

        /// <summary>
        /// Assigns the new inventory
        /// </summary>
        /// <param name="inventory">The new inventory</param>
        public void AssignInventory(Inventory inventory)
        {
            var newUI = InventoryManager.CurrentInstance.CreateNewUI(inventory);
            newUI.transform.localPosition = Config.DefaultContainerUIPosition;
            this.InventoryUI = newUI;
        }

        // Called once in the beginning for initialization
        protected void Start()
        {
            this._player = GameObjectFinder.FindGameObjectWithTag(Tags.Player);
        }

        /// <summary>
        /// Called once per frame
        /// </summary>
        protected void Update()
        {
            // Opens if the player is close enough, closes if not
            var isOpened = this.InventoryUI.gameObject.activeSelf;
            var isInRange = (this._player.transform.position - this.transform.position).magnitude <= Config.OpenContainerRange;
            if (isInRange && !isOpened)
            {
                this.InventoryUI.Open();
            }
            else if(!isInRange && isOpened)
            {
                this.InventoryUI.Close();
            }
        }
    }
}
