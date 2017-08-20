//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ShipMovementSettings.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.Scripts.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// A collection of ship movement settings
    /// </summary>
    public static class ShipMovementSettings
    {
        /// <summary>
        /// The minimal speed that the ship can move at. Anything lower will be considered still
        /// </summary>
        public const float MinimalSpeed = 0.05f;
    }
}
