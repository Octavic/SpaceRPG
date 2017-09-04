//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IShipWeaponFireMode.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.CombatScripts.Ships.Weapons.WeaponFireMode
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	/// <summary>
	/// Fire the ship weapon
	/// </summary>
	public interface IShipWeaponFireMode
	{
		/// <summary>
		/// Try to fire the weapon
		/// </summary>
		/// <returns>True if the weapon fired</returns>
		bool TryFire();
	}
}
