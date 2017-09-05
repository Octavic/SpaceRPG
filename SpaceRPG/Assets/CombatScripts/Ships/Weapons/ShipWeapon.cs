//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ShipWeapon.cs">
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
    /// Describse a base ship weapon
    /// </summary>
    public abstract class ShipWeapon : MonoBehaviour    
    {
		/// <summary>
		/// Id for the faction
		/// </summary>
		public int FactoinId;

		/// <summary>
		/// The target
		/// </summary>
		public Ship Target;

        /// <summary>
        /// How much fuel drain per shot
        /// </summary>
        public float CDMDrain;

        /// <summary>
        /// How much energy drain per shot
        /// </summary>
        public float EnergyDrain;

		/// <summary>
		/// How much CPU does the weapon take
		/// </summary>
		public float CpuDragin;

        /// <summary>
        /// The possible time gap between shots
        /// </summary>
        public float TimeBetweenShots;

		/// <summary>
		/// The prefab for fire effect
		/// </summary>
		public GameObject FirePrefab;

		/// <summary>
		/// The prefab for bullet effect
		/// </summary>
		public GameObject ProjectilePrefab;

        /// <summary>
        /// How much time left before next shot can be fired
        /// </summary>
        public float FireCooldown { get; protected set; }

		/// <summary>
		/// How ready is the weapon, 100% being ready to fire
		/// </summary>
		public float ReloadPercentage
		{
			get
			{
				return (this.TimeBetweenShots - this.FireCooldown) / this.TimeBetweenShots;
			}
		}

		/// <summary>
		/// The order in layer
		/// </summary>
		protected int _orderInLayer;

		/// <summary>
		/// The weapon projectile's component
		/// </summary>
		protected WeaponGeneratedObject _projectileComponent;

		/// <summary>
		/// Try to fire the weapon
		/// </summary>
		/// <returns>True if the weapon fired succcessfully</returns>
		public virtual bool TryFire()
		{
			return this.FireCooldown == 0 && this.Target != null;
		}

		/// <summary>
		/// Generates a new projectile
		/// </summary>
		/// <returns>The new projectile</returns>
		public WeaponGeneratedObject GenerateProjectile()
		{
			// Create new instance as child
			var projectile = Instantiate(this.ProjectilePrefab, this.transform, false);

			// Let new projectile run free
			projectile.transform.parent = null;

			// Set variables
			var projectileClass = projectile.GetComponent<WeaponGeneratedObject>();
			projectileClass.FactionId = this.FactoinId;
			if (projectileClass is WeaponMissile)
			{
				((WeaponMissile)projectileClass).Target = this.Target;
			}

			// See display sorting layer
			projectile.GetComponent<SpriteRenderer>().sortingOrder = this._orderInLayer;

			return projectileClass;
		}

		/// <summary>
		/// Called in the beginning for intialization
		/// </summary>
		protected virtual void Start()
		{
			this._orderInLayer = this.GetComponent<SpriteRenderer>().sortingOrder;
			this._projectileComponent = this.ProjectilePrefab.GetComponent<WeaponGeneratedObject>();
		}

        /// <summary>
        /// Called once per frame
        /// </summary>
        protected virtual void Update()
        {
            // Apply cooldown if needed
            if (this.FireCooldown > 0)
            {
                this.FireCooldown -= Time.deltaTime;
                if (this.FireCooldown < 0)
                {
                    this.FireCooldown = 0;
                }
            }
        }
    }
}
