//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="WeaponGeneratedObject.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.CombatScripts.Ships.Weapons
{
	using Assets.CombatScripts.Utility;
	using Assets.CombatScripts.Ships;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	/// <summary>
	/// Describes an object generated when a weapon is fired
	/// </summary>
	public abstract class WeaponGeneratedObject : DelayedSelfDestruct
	{
		/// <summary>
		/// Speed of the projectile
		/// </summary>
		public float Velocity;

		/// <summary>
		/// The faction who fired this object
		/// </summary>
		public int FactionId;

		/// <summary>
		/// The amount of damage done
		/// </summary>
		public float Damage;

		/// <summary>
		/// Called when the projectile hits an enemy ship
		/// </summary>
		public abstract void OnHitEnemyShip();
	}
}
