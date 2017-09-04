//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="BurstFire.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.CombatScripts.Ships.Weapons.WeaponFireMode
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using UnityEngine;
	using Utility;

	/// <summary>
	/// Fires the weapon in a spread
	/// </summary>
	public class SpreadFire : IShipWeaponFireMode
	{
		/// <summary>
		/// The ship weapon
		/// </summary>
		private ShipWeapon _weapon;

		/// <summary>
		/// How many times to burst fire
		/// </summary>
		private int _count;

		/// <summary>
		/// The delay between each shot
		/// </summary>
		private float _spread;

		/// <summary>
		/// Creates a new instance of the <see cref="SpreadFire"/> class
		/// </summary>
		/// <param name="weapon">The ship weapon being fired</param>
		/// <param name="count">How many pellets there are</param>
		/// <param name="spread">Maximum angle</param>
		public SpreadFire(ShipWeapon weapon, int count, float spread)
		{
			this._weapon = weapon;
			this._count = count;
			this._spread = spread;
		}

		/// <summary>
		/// Try to fire the weapon
		/// </summary>
		/// <returns>True if the weapon fired</returns>
		public bool TryFire()
		{
			for (int i = 0; i < this._count; i++)
			{
				var newWeapon = this._weapon.GenerateProjectile();
				var oldEulerZ = newWeapon.transform.eulerAngles.z;
				var offset = RandomNumberGenerator.Next(-this._spread, this._spread);
				newWeapon.transform.eulerAngles = new Vector3(0, 0, oldEulerZ + offset);
			}

			return true;
		}
	}
}
