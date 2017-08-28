//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ShipWeaponSystem.cs">
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

	/// <summary>
	/// A collection of cannons controlled by this system
	/// </summary>
	[Serializable]
	public struct ShipWeaponSystem
	{
		/// <summary>
		/// A collection of cannons controlled by this system
		/// </summary>
		public List<ShipWeaponSlot> ControlledWeaponSlots;

		/// <summary>
		/// The total amount of available CPU power
		/// </summary>
		public float CpuCapacity;

		/// <summary>
		/// The current fire mode for this system
		/// </summary>
		public WeaponSystemAutoFireModeEnum FireMode;

		/// <summary>
		/// Gets a list of cannons in the system
		/// </summary>
		public List<ShipWeaponSlot> FilledWeaponSlots
		{
			get
			{
				return this.ControlledWeaponSlots.Where(slot => slot.CurrentWeaponInSlot != null).ToList();
			}
		}

		/// <summary>
		/// The sum of CPU usage of all the cannons
		/// </summary>
		public float CpuUsage
		{
			get
			{
				return this.FilledWeaponSlots.Sum(slot => slot.CurrentWeaponInSlot.CpuDragin);
			}
		}

		/// <summary>
		/// Returns true if every single weapon in the system is lined up
		/// </summary>
		public bool IsLinedUp
		{
			get
			{
				return !(this.FilledWeaponSlots.Any(slot => !slot.IsLinedUp));
			}
		}

		/// <summary>
		/// Get roughly how ready the weapons are
		/// </summary>
		public float ReloadPercentage
		{
			get
			{
				return this.FilledWeaponSlots.Average(slot=>slot.CurrentWeaponInSlot.ReloadPercentage);
			}
		}

		/// <summary>
		/// Gets the current system's target
		/// </summary>
		public Ship CurrentTarget
		{
			get
			{
				return this.ControlledWeaponSlots.Count == 0 ? null : this.ControlledWeaponSlots[0].Target;
			}
			set
			{
				foreach (var slot in this.ControlledWeaponSlots)
				{
					slot.Target = value;
				}
			}
		}

		/// <summary>
		/// Try to auto fire the weapon. This is called once per frame
		/// </summary>
		/// <returns>True if any of the weapons fired</returns>
		public bool TryAutoFireAllWeapons()
		{
			switch (this.FireMode)
			{
				// Auto fire off, don't fire anything
				case WeaponSystemAutoFireModeEnum.Off:
					return false;

				// Auto fire set to only fire weapons that's lined up
				case WeaponSystemAutoFireModeEnum.Precise:

					// Nothing to fire
					var result = false;
					foreach (var slot in this.FilledWeaponSlots)
					{
						if (slot.IsLinedUp && slot.TryFire())
						{
							result = true;
						}
					}

					return result;

				// Default case, just try to fire all and return result
				default:
					result = false;
					foreach (var slot in this.FilledWeaponSlots)
					{
						if (slot.TryFire())
						{
							result = true;
						}
					}

					return result;
			}
		}
	}
}
