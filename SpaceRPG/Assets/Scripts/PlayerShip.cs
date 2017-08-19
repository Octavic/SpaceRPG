//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="PlayerShip.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.Scripts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;
    using Utility;

    /// <summary>
    /// A ship that's controlled by a player
    /// </summary>
    public class PlayerShip : Ship
    {
        /// <summary>
        /// Gets the current throttle from 0 to 1. 0 means no throttle, 1 meaning full throttle
        /// </summary>
        public float CurrentThrottle
        { 
            get
            {
                return this._currentThrottle;
            }
            private set
            {
                this._currentThrottle = Math.Min(value, 1);     
            }
        }

        /// <summary>
        /// The current throttle
        /// </summary>
        private float _currentThrottle;

        /// <summary>
        /// Called once per frame
        /// </summary>
        protected override void Update()
        {
            // Turn the ship
            var rotateDirection = Input.GetAxis(ButtonNames.Turn.ToString());
            this.Rotation += this.RotationSpeed * Time.deltaTime * rotateDirection;


            base.Update();
        }
    }
}
