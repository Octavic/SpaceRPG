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
		/// Gets a list of cannons in the system
		/// </summary>
		public IEnumerable<ShipWeapon> WeaponsInSystem
		{
			get
			{
				return this.ControlledWeaponSlots.Where(slot => slot.CurrentCannonInSlot != null).Select(slot=>slot.CurrentCannonInSlot);
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
				return this.WeaponsInSystem.Average(weapon => weapon.ReloadPercentage);
			}
		}

		/// <summary>
		/// Try to fire all of the weapons
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
