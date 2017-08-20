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
    using Settings;

    /// <summary>
    /// A ship that's controlled by a player
    /// </summary>
    public class PlayerShip : Ship
    {
        /// <summary>
        /// How fast the throttle adjusts
        /// </summary>
        public float ThrottleAdjustSpeed;

        /// <summary>
        /// Called once per frame
        /// </summary>
        protected override void Update()
        {
            // Turn the ship
            var rotateDirection = InputHelper.GetAxis(ButtonNames.Turn);
            this.TryTurn(rotateDirection * RotationSpeed * Time.deltaTime);

            // Adjust throttle
            var throttleAdjustInput = InputHelper.GetAxis(ButtonNames.AdjustThrottle);
            this.CurrentThrottle = this.CurrentThrottle + throttleAdjustInput * this.ThrottleAdjustSpeed * Time.deltaTime;

            // Set throttle
            var throttleSetInput = InputHelper.GetAxis(ButtonNames.SetThrottle);
            if (throttleSetInput > 0)
            {
                this._currentThrottle = 1;
            }
            else if (throttleSetInput < 0)
            {
                this._currentThrottle = 0;
            }

            // Apply side thrust
            var sideThrust = InputHelper.GetAxis(ButtonNames.SideThrust);
            Vector2 sideVector = new Vector2(this.ForwardVector.y, -this.ForwardVector.x);
            if (sideThrust < 0)
            {
                sideVector *= -1;
            }

            if (sideThrust != 0)
            {
                this._rgbd.AddForce(sideVector * this.SideEngineThrust * Time.deltaTime, ForceMode2D.Force);
            }

            // Apply break
            if (InputHelper.GetButton(ButtonNames.Break))
            {
                this.ApplyBreak();
            }
            
            base.Update();
        }
    }
}
