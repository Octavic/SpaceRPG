//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="GalaxyMap.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.GeneralScripts.UI.GalaxyMap
{
    /// <summary>
    /// Describes the galaxy map
    /// </summary>
    [Serializable]
    public class GalaxyMap
    {
        /// <summary>
        /// Gets the tiles for this galaxy map
        /// </summary>
        public GalaxyMapTile[,] Tiles { get; private set; }
    }
}
