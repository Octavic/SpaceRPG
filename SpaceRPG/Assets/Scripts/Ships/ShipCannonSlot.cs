    //  --------------------------------------------------------------------------------------------------------------------
    //  <copyright file="ShipCannonSlot.cs">
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
    using Utility;

    /// <summary>
    /// Describes the cannon of a ship
    /// </summary>
    public class ShipCannonSlot : MonoBehaviour
    {
        /// <summary>
        /// If this cannon slot is customizable
        /// </summary>
        public bool Customizable;

        /// <summary>
        /// How the cannon rotates
        /// </summary>
        public ShipCannonRotateEnum RotationType;

        /// <summary>
        /// The maximum angle that the cannon can be at
        /// </summary>
        public float MaxAngle;

        /// <summary>
        /// How many degrees the cannon slot can rotate per second
        /// </summary>
        public float RotationSpeed;

        /// <summary>
        /// The target to be shot at
        /// </summary>
        public Ship Target;

        /// <summary>
        /// Called once in the beginning
        /// </summary>
        private void Start()
        {

        }

        /// <summary>
        /// Called once per frame
        /// </summary>
        private void Update()
        {
            // If there's a target
            if (Target != null)
            {
                var targetAngle = ((Vector2)(Target.transform.position - this.transform.position)).ToAngleDegree();
                var oldRotation = this.transform.eulerAngles.z;
                var angleDiff = CalcAngleDiff.InDegrees(oldRotation, targetAngle);
                var maxTurning = this.RotationSpeed * Time.deltaTime;
                float angleToTurn = angleDiff;
                if (Math.Abs(angleDiff) > maxTurning)
                {
                    angleToTurn = Mathf.Sign(angleDiff) * maxTurning;
                }

                this.transform.eulerAngles = new Vector3(0, 0, oldRotation + angleToTurn);
            }
        }
    }
}
