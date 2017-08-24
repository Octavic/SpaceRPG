//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ShipCannon.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.Scripts.Ships
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;

    /// <summary>
    /// Describse a base ship cannon
    /// </summary>
    public abstract class ShipCannon : MonoBehaviour    
    {
        /// <summary>
        /// How much fuel drain per shot
        /// </summary>
        public float CDMDrain;

        /// <summary>
        /// How much energy drain per shot
        /// </summary>
        public float EnergyDrain;

        /// <summary>
        /// The possible time gap between shots
        /// </summary>
        public float TimeBetweenShots;

        /// <summary>
        /// Try to fire the weapon
        /// </summary>
        public abstract bool TryFire();

        /// <summary>
        /// Used in the beginning for initialization
        /// </summary>
        protected void Start()
        {
        }

        /// <summary>
        /// Called once per frame
        /// </summary>
        protected void Update()
        {
        }
    }
}
