//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="BurstFire.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.CombatScripts.Ships.Weapons.WeaponFireMode
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using UnityEngine;

	/// <summary>
	/// Burst fires
	/// </summary>
	public class BurstFire : IShipWeaponFireMode
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
		private float _delay;

		/// <summary>
		/// Creates a new instance of the <see cref="BurstFire"/> class
		/// </summary>
		/// <param name="weapon">The weapon that's being fired</param>
		/// <param name="count">How many times to burst fire</param>
		/// <param name="delay">The delay between each shot</param>
		public BurstFire(ShipWeapon weapon, int count, float delay)
		{
			this._weapon = weapon;
			this._count = count;
			this._delay = delay;
		}

		/// <summary>
		/// Try to fire the weapon
		/// </summary>
		/// <returns>True if the weapon fired</returns>
		public bool TryFire()
		{
			this._weapon.StartCoroutine(this.TryBurstFire());
			return true;
		}

		/// <summary>
		/// Burst fires the weapon
		/// </summary>
		/// <returns>Coroutine action</returns>
		private IEnumerator TryBurstFire()
		{
			for (int i = 0; i < this._count; i++)
			{
				this._weapon.GenerateProjectile();
				yield return new WaitForSeconds(this._delay);
			}

			yield break;
		}
	}
}
