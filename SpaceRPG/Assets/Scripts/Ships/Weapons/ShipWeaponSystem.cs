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
		public List<ShipWeapon> WeaponsInSystem
		{
			get
			{
				return this.ControlledWeaponSlots.Where(slot => slot.CurrentWeaponInSlot != null).Select(slot=>slot.CurrentWeaponInSlot).ToList();
			}
		}

		/// <summary>
		/// The sum of CPU usage of all the cannons
		/// </summary>
		public float CpuUsage
		{
			get
			{
				return this.WeaponsInSystem.Sum(cannon => cannon.CpuDragin);
			}
		}

		/// <summary>
		/// Returns true if every single weapon in the system is lined up
		/// </summary>
		public bool IsLinedUp
		{
			get
			{
				return !(this.ControlledWeaponSlots.Any(slot => !slot.IsLinedUp));
			}
		}

		/// <summary>
		/// Get roughly how ready the weapons are
		/// </summary>
		public float ReloadPercentage
		{
			get
			{
				if (this.WeaponsInSystem.Count == 0)
				{
					return 0;
				}

				return this.WeaponsInSystem.Average(weapon => weapon.ReloadPercentage);
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

				// Auto fire set to only when weapons are lined up and fully loaded
				case WeaponSystemAutoFireModeEnum.Precise:
					if (this.ReloadPercentage == 1
					&& !this.ControlledWeaponSlots.Any(slot=>slot.CurrentWeaponInSlot !=null && !slot.IsLinedUp))
					{
						this.TryFireAllWeapons();
						return true;
					}

					return false;
				// Default case, just try to fire all and return result

				default:
					return this.TryFireAllWeapons();
			}
		}

		/// <summary>
		/// Try to manually fire all of the weapons
		/// </summary>
		/// <returns>True if any of the weapons fired</returns>
		public bool TryFireAllWeapons()
		{
			var result = false;
			foreach (var weapon in this.WeaponsInSystem)
			{
				if (weapon.TryFire())
				{
					result = true;
				}
			}

			return result;
		}
	}
}
