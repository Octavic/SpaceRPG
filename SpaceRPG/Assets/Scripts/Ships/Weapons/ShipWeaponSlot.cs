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
	/// Describes the weapon of a ship
	/// </summary>
	public class ShipWeaponSlot : MonoBehaviour
	{
		/// <summary>
		/// If this weapon slot is customizable
		/// </summary>
		public bool Customizable;

		/// <summary>
		/// If the weapon is on top
		/// </summary>
		public bool IsOnTop;

		/// <summary>
		/// How the weapon rotates
		/// </summary>
		public ShipWeaponRotateEnum RotationType;

		/// <summary>
		/// The maximum angle that the weapon can be at
		/// </summary>
		public float MaxAngle;

		/// <summary>
		/// How many degrees the weapon slot can rotate per second
		/// </summary>
		public float RotationSpeed;

		/// <summary>
		/// The target to be shot at
		/// </summary>
		public Ship Target;

		/// <summary>
		/// Gets a value indicating whether or not this ship weapon is lined up with the target
		/// </summary>
		public bool IsLinedUp { get; private set; }

		/// <summary>
		/// Gets the current cannon in slot
		/// </summary>
		public ShipWeapon CurrentWeaponInSlot;

		/// <summary>
		/// The original Z rotation when the weapon is at neutral
		/// </summary>
		private float _originalRotation;

		/// <summary>
		/// Places a new cannon into the slot
		/// </summary>
		/// <param name="newWeapon">The new cannon</param>
		/// <returns>The old cannon, null if the slot was empty before</returns>
		public ShipWeapon PlaceWeapon(ShipWeapon newWeapon)
		{
			var oldWeapon = this.CurrentWeaponInSlot;
			this.CurrentWeaponInSlot = newWeapon;

			// Sets the new weapon to the right place with the right rotation
			var newWeaponTransform = newWeapon.transform;
			newWeaponTransform.localPosition = Vector3.zero;
			newWeaponTransform.localEulerAngles = Vector3.zero;
			newWeaponTransform.parent = this.transform;
			this.UpdateWeaponRenderLayer();

			return oldWeapon;
		}

		/// <summary>
		/// Updates the weapon's rendering layer base on the "IsOnTop" boolean
		/// </summary>
		private void UpdateWeaponRenderLayer()
		{
			this.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = this.IsOnTop ? 2 : -2;
		}

        /// <summary>
        /// Called once in the beginning
        /// </summary>
        private void Start()
        {
			this._originalRotation = this.transform.eulerAngles.z;
			this.UpdateWeaponRenderLayer();
        }

        /// <summary>
        /// Called once per frame
        /// </summary>
        private void Update()
        {
			this.IsLinedUp = true;

			// If there's a target
			if (Target != null)
			{
				// The angle that the target is at
				var targetAngle = ((Vector2)(Target.transform.position - this.transform.position)).ToAngleDegree();

				var oldRotation = this.transform.eulerAngles.z;
				var angleDiff = CalcAngleDiff.InDegrees(oldRotation, targetAngle);
				var maxTurning = this.RotationSpeed * Time.deltaTime;
				float angleToTurn = angleDiff;

				// Check if the weapon is lined up. If the turning required is bigger than how much can be turned this frame, then weapon is no longer lined up
				if (Math.Abs(angleDiff) > maxTurning)
				{
					this.IsLinedUp = false;
					angleToTurn = Mathf.Sign(angleDiff) * maxTurning;
				}

				this.transform.eulerAngles = new Vector3(0, 0, oldRotation + angleToTurn);

				// Check if the weapon is outside allowed max angle
				if (this.RotationType == ShipWeaponRotateEnum.Limited)
				{
					var diffToCenter = CalcAngleDiff.InDegrees(this._originalRotation, this.transform.localEulerAngles.z);
					if (Math.Abs(diffToCenter) > this.MaxAngle)
					{
						this.IsLinedUp = false;
						this.transform.localEulerAngles = new Vector3(0, 0, this._originalRotation + Math.Sign(diffToCenter) * this.MaxAngle);
					}
				}
			}
			else
			{
				this.IsLinedUp = false;
			}
        }
    }
}
