//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Config.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.GeneralScripts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;

    /// <summary>
    /// Some static configuration
    /// </summary>
    public static class Config
    {
        /// <summary>
        /// Since there is only one save file, this the path to that save file
        /// </summary>
        public const string SaveDataPath = "save.dat";

        /// <summary>
        /// How far the mouse will have to be dragged to be considered a drag instead of a tap/click
        /// </summary>
        public const float DragSensitivity = 0.5f;

        /// <summary>
        /// The grid size for items
        /// </summary>
        public const float ItemGridSize = 50;

        /// <summary>
        /// The boarder size for inventories
        /// </summary>
        public const float InventoryBoarder = 10;

        /// <summary>
        /// the size of the black outline around the inventory background
        /// </summary>
        public const float InventoryBoarderOutline = 5;

        /// <summary>
        /// The size  of the header for the inventory containing the close button
        /// </summary>
        public const float InventoryHeaderHeight = 25;

        /// <summary>
        /// Size of the close inventory button
        /// </summary>
        public const float CloseInventoryButtonSize = 25;

        /// <summary>
        /// The range player has to be in to open containers
        /// </summary>
        public const float OpenContainerRange = 10;

        /// <summary>
        /// Gets the default position for a container UI
        /// </summary>
        public static Vector2 DefaultContainerUIPosition
        {
            get
            {
                return new Vector2(-200, 30);
            }
        }
    }
}
