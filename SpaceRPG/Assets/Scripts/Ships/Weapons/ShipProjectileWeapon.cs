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
		/// Try to fire the weapon
		/// </summary>
		/// <returns>True if the weapon fired successfully</returns>
		public override bool TryFire()
		{
			if (!base.TryFire())
			{
				return false;
			}

			var projectile = Instantiate(this.ProjectilePrefab);
			projectile.transform.position = this.transform.position;
			projectile.transform.eulerAngles = this.transform.eulerAngles;
			this.FireCooldown = this.TimeBetweenShots;
			return true;
		}
	}
}
