//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ShipBallasticWeapon.cs">
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
	using WeaponFireMode;

	/// <summary>
	/// Defines a ship ballastic weapon that fires one or multiple projectiles
	/// </summary>
	public class ShipBallasticWeapon : ShipWeapon
	{
		/// <summary>
		/// How this weapon is fired
		/// </summary>
		public WeaponFireModeEnum FireMode;

		/// <summary>
		/// How many to be fired
		/// </summary>
		public int FireCount;
	
		/// <summary>
		/// How much delay between each burst
		/// </summary>
		public float BurstDelay;

		/// <summary>
		/// The maximum angle for the spread
		/// </summary>
		public float SpreadAngle;

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
		/// The fire mode for this weapon
		/// </summary>
		private IShipWeaponFireMode _fireMode;

		/// <summary>
		/// Called in the beginning
		/// </summary>
		protected override void Start()
		{
			base.Start();

			

			switch (this.FireMode)
			{
				case WeaponFireModeEnum.Burst:
					this._fireMode = new BurstFire(this, this.FireCount, this.BurstDelay);
					break;
				case WeaponFireModeEnum.Spread:
					this._fireMode = new SpreadFire(this, this.FireCount, this.SpreadAngle);
					break;
				default:
					this._fireMode = new SingleFire(this);
					break;
			}
		}

		/// <summary>
		/// Try to fire the weapon
		/// </summary>
		/// <returns>True if the weapon fired</returns>
		public override bool TryFire()
		{
			if (!base.TryFire())
			{
				return false;
			}

			this._fireMode.TryFire();
			this.FireCooldown = this.TimeBetweenShots;
			return true;
		}
	}
}
