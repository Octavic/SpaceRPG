//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="SaveData.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.GeneralScripts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UI.GalaxyMap;

    /// <summary>
    /// All save data related to the current player and flags/global events
    /// </summary>
    [Serializable]
    public struct SaveData
    {
        /// <summary>
        /// The player's current coordinate. Since player can only save inside a space station
        /// </summary>
        public MapCoordinate CurrentCoordiante;

        /// <summary>
        /// The currency
        /// </summary>
        public long Currency;
    }
}
