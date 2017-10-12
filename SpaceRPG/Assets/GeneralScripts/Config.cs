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
    }
}
