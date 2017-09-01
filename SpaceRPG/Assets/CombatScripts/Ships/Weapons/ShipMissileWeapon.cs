//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ShipMissileWeapon.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.CombatScripts.Ships.Weapons
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using UnityEngine;

	/// <summary>
	/// A ship weapon that fires missiles
	/// </summary>
	public class ShipMissileWeapon : ShipWeapon
	{
		/// <summary>
		/// Gets the speed of the bullet
		/// </summary>
		public float ProjectileSpeed
		{
			get
			{
				return this._missileComponent.Velocity;
			}
		}

		/// <summary>
		/// The weapon projectile's component
		/// </summary>
		private WeaponMissile _missileComponent;

		/// <summary>
		/// Called in the beginning
		/// </summary>
		protected void Start()
		{
			this._missileComponent = this.ProjectilePrefab.GetComponent<WeaponMissile>();
		}

		/// <summary>
		/// Try to fire the weapon
		/// </summary>
		/// <param name="target">The target ship</param>
		/// <returns>True if the weapon fired succcessfully</returns>
		public override bool TryFire(Ship target)
		{
			// If you can't fire right now, do nothing
			if (!base.TryFire(target))
			{
				return false;
			}

			// If there's no target, then missile can't fire
			if (target == null)
			{
				return false;
			}

			// Create new missle
			var projectile = Instantiate(this.ProjectilePrefab, this.transform, false);
			projectile.transform.parent = null;
			projectile.GetComponent<WeaponMissile>().Target = target;
			this.FireCooldown = this.TimeBetweenShots;
			return true;
		}
	}
}
