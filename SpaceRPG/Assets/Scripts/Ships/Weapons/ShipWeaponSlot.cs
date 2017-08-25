//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ShipWeaponSlot.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.Scripts.Ships.Weapons
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
	public class ShipWeaponSlot : MonoBehaviour
	{
		/// <summary>
		/// If this cannon slot is customizable
		/// </summary>
		public bool Customizable;

		/// <summary>
		/// How the cannon rotates
		/// </summary>
		public ShipWeaponRotateEnum RotationType;

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
		/// Gets a value indicating whether or not this ship cannon is lined up with the target
		/// </summary>
		public bool IsLinedUp { get; private set; }

		/// <summary>
		/// Gets the current cannon in slot
		/// </summary>
		public ShipWeapon CurrentCannonInSlot;

		/// <summary>
		/// Places a new cannon into the slot
		/// </summary>
		/// <param name="newCannon">The new cannon</param>
		/// <returns>The old cannon, null if the slot was empty before</returns>
		public ShipWeapon PlaceCannon(ShipWeapon newCannon)
		{
			var oldCannon = this.CurrentCannonInSlot;
			this.CurrentCannonInSlot = newCannon;
			return oldCannon;
		}

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
					this.IsLinedUp = false;
					angleToTurn = Mathf.Sign(angleDiff) * maxTurning;
				}
				else
				{
					this.IsLinedUp = true;
				}

                this.transform.eulerAngles = new Vector3(0, 0, oldRotation + angleToTurn);
            }
        }
    }
}
