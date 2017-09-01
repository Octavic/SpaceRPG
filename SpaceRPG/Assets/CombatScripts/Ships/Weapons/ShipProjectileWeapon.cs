//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ShipWeapon.cs">
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
	/// A ship weapon that fires a non-seeking projectile
	/// </summary>
	public class ShipProjectileWeapon : ShipWeapon
	{
		/// <summary>
		/// Gets the speed of the bullet
		/// </summary>
		public float ProjectileSpeed
		{
			get
			{
				return this._projectileComponent.Velocity;
			}
		}

		/// <summary>
		/// The weapon projectile's component
		/// </summary>
		private WeaponProjectile _projectileComponent;

		/// <summary>
		/// Called in the beginning
		/// </summary>
		protected void Start()
		{
			this._projectileComponent = this.ProjectilePrefab.GetComponent<WeaponProjectile>();
		}

		/// <summary>
		/// Try to fire the weapon
		/// </summary>
		/// <param name="target">The target ship</param>
		/// <returns>True if the weapon fired succcessfully</returns>
		public override bool TryFire(Ship target)
		{
			// If you can't fire right now
			if (!base.TryFire(target))
			{
				return false;
			}

			// Create new projectile
			var projectile = Instantiate(this.ProjectilePrefab, this.transform, false);
			projectile.transform.parent = null;
			this.FireCooldown = this.TimeBetweenShots;
			return true;
		}
	}
}
