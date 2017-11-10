//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Ship.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.CombatScripts.Ships
{
    using Assets.CombatScripts.Settings;
	using Assets.CombatScripts.UI;
	using Assets.CombatScripts.Utility;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;
	using Weapons;
	using Assets.GeneralScripts.Utility;

	/// <summary>
	/// Describes the basic functionality of a ship
	/// </summary>
	public class Ship : MonoBehaviour
    {
        /// <summary>
        /// The main paintjob game object
        /// </summary>
        public GameObject MainPaintjob;

		/// <summary>
		/// Name of the ship
		/// </summary>
		public string ShipName;

		/// <summary>
		/// The faction Id if the ship
		/// </summary>
		public int FactionId;

		/// <summary>
		/// The max speed at which this vessel can travel
		/// </summary>
		public float MaximumSpeed;

        /// <summary>
        /// The amount of forward thrust that the engine provides
        /// </summary>
        public float MainEngineThrust;

        /// <summary>
        /// The amount of sidewards thrust that the engine provides
        /// </summary>
        public float SideEngineThrust;
        
        /// <summary>
        /// How fast the ship rotates in degrees per second
        /// </summary>
        public float RotationSpeed;

        /// <summary>
        /// The amount of velocity that can be canceled per second
        /// </summary>
        public float StoppingForce;

		/// <summary>
		/// The amount of shield available
		/// </summary>
		public float MaxShield;

		/// <summary>
		/// The amount of hull available
		/// </summary>
		public float MaxHull;

		/// <summary>
		/// How much delay between the shield can start regenerating again
		/// </summary>
		public float ShieldRegenDelay;

		/// <summary>
		/// How much shield the ship can regenerate per second
		/// </summary>
		public float ShieldRegenSpeed;

		/// <summary>
		/// A list of ship weapon systems
		/// </summary>
		public List<ShipWeaponSystem> WeaponSystems;

		/// <summary>
		/// Gets or sets the current shield health
		/// </summary>
		public float CurrentShield
		{
			get
			{
				return this._currentShield;
			}
			set
			{
				// Make sure that shield does not exceed max shield
				this._currentShield = Math.Min(value, this.MaxShield);

				// Make sure that shield cannot fall below zero. If so, the remaining damage goes onto hull
				if (value < 0)
				{
					this._currentShield = 0;
					this.CurrentHull += value;
				}
			}
		}

		/// <summary>
		/// Gets or sets the current hull health
		/// </summary>
		public float CurrentHull
		{
			get
			{
				return this._currentHull;
			}
			set
			{
				this._currentHull = value;
				if (this._currentHull <= 0)
				{
					this.OnShipDestroy();
				}
			}
		}

        /// <summary>
        /// Gets or sets the main color of the ship
        /// </summary>
        public Color MainColor
        {
            get
            {
                return this.MainSpriteRenderer.color;
            }
            set
            {
                this.MainSpriteRenderer.color = value;
            }
        }

        /// <summary>
        /// Gets or sets the rotation of the ship in degrees. 0 means the ship is facing direct east (right)
        /// </summary>
        public float Rotation
        {
            get
            {
                return this.transform.eulerAngles.z;
            }
            set
            {
                this.transform.eulerAngles = new Vector3(0, 0, value);
            }
        }

        /// <summary>
        /// Gets the forward vector towards which the ship is traveling at
        /// </summary>
        public Vector2 ForwardVector
        {
            get
            {
                return AngleToVector.FromDegree(this.Rotation);
            }
        }

		/// <summary>
		/// Gets the ship attitude
		/// </summary>
		public ShipAttitudeEnum ShipAttitude
		{
			get
			{
				return this._shipAttitude;
			}
		}

		/// <summary>
		/// Gets the current throttle from 0 to 1. 0 means no throttle, 1 meaning full throttle
		/// </summary>
		public virtual float CurrentThrottle
		{
			get
			{
				return this._currentThrottle;
			}
			set
			{
				this._currentThrottle = value.LimitTo(0, 1);
			}
		}

		/// <summary>
		/// The current throttle
		/// </summary>
		private float _currentThrottle;

		/// <summary>
		/// The current shield
		/// </summary>
		private float _currentShield;

		/// <summary>
		/// The current hull
		/// </summary>
		private float _currentHull;

		/// <summary>
		/// How much time passed since the last time the shield was attacked
		/// </summary>
		private float _timeSinceShieldDamage;

        /// <summary>
        /// The main sprite renderer to control the ship's main color
        /// </summary>
        public SpriteRenderer MainSpriteRenderer { get; private set; }

        /// <summary>
        /// The rigid body
        /// </summary>
        public Rigidbody2D RGBD { get; private set; }

		/// <summary>
		/// Gets the ship's attitude
		/// </summary>
		private ShipAttitudeEnum _shipAttitude;

        /// <summary>
        /// Try to stop the ship
        /// </summary>
        public void ApplyBreak()
        {
            this._currentThrottle = 0;

			var velocityReductionThisFrame = this.StoppingForce * Time.deltaTime;
			var curVelocity = this.RGBD.velocity.magnitude;
			if (curVelocity <= velocityReductionThisFrame)
			{
				this.RGBD.velocity = Vector2.zero;
			}
			else
			{
				this.RGBD.velocity = this.RGBD.velocity.normalized;
				this.RGBD.velocity *= (curVelocity - velocityReductionThisFrame);
			}
        }
        
        /// <summary>
        /// Try to turn the ship with the given offset. Limited by how much can be turned overall
        /// </summary>
        /// <param name="degrees">Target degrees.</param>
        public void TryTurn(float degrees)
        {
            var maximumPossibleTurn = this.RotationSpeed * Time.deltaTime;
			this.RGBD.angularVelocity = 0.0f;
            if (Math.Abs(degrees) > maximumPossibleTurn)
            {
                degrees = Math.Sign(degrees) * maximumPossibleTurn;
            }

            this.Rotation += degrees;
        }

		/// <summary>
		/// Applies a side thrust
		/// </summary>
		/// <param name="toLeft">True if the thrust is to the left, false if it's to the right</param>
		public void ApplySideThrust(bool toLeft)
		{
			Vector2 sideVector = new Vector2(this.ForwardVector.y, -this.ForwardVector.x);
			if (toLeft)
			{
				sideVector *= -1;
			}

			this.RGBD.AddForce(sideVector * this.SideEngineThrust, ForceMode2D.Force);
		}

        /// <summary>
        /// Called in the betginning
        /// </summary>
        public virtual void Start()
        {
            this.MainSpriteRenderer = this.MainPaintjob.GetComponent<SpriteRenderer>();
            this.RGBD = this.GetComponent<Rigidbody2D>();

			// Decide the ship's attitude
			this._shipAttitude = this.gameObject.tag == Tags.Player.ToString() ? ShipAttitudeEnum.Self : ShipAttitudeEnum.Hostile;

			// Register ship on minimap
			GameController.CurrentInstance.RegisterShip(this);

			// Apply shield and hull
			this._currentShield = this.MaxShield;
			this._currentHull = this.MaxHull;
        }

        /// <summary>
        /// Called once per frame
        /// </summary>
        protected void FixedUpdate()
        {
            // Apply throttle
            this.RGBD.AddForce(this.ForwardVector * this.MainEngineThrust * Time.deltaTime * this._currentThrottle, ForceMode2D.Force);

            // Limits velocity to maximum speed
            if (this.RGBD.velocity.magnitude > this.MaximumSpeed)
            {
				var unitVelocity = this.RGBD.velocity.normalized;
				this.RGBD.velocity = unitVelocity * this.MaximumSpeed;
            }

			// Auto fire weapon systems
			foreach (var weaponSystem in this.WeaponSystems)
			{
				weaponSystem.TryAutoFireAllWeapons();
			}

			// Apply shield regen if applicable
			if (this._timeSinceShieldDamage >= this.ShieldRegenDelay)
			{
				this.CurrentShield += this.ShieldRegenSpeed * Time.deltaTime;
			}
			else
			{
				this._timeSinceShieldDamage += Time.deltaTime;
			}
        }

		/// <summary>
		/// When the ship collides with a trigger
		/// </summary>
		/// <param name="collision">The collision that occured</param>
		protected void OnTriggerEnter2D(Collider2D collision)
		{
			var projectileClass = collision.gameObject.GetComponent<WeaponGeneratedObject>();

			// if the collision is between this ship and an enemy projectile
			if (projectileClass != null && projectileClass.FactionId != this.FactionId)
			{
				projectileClass.OnHitEnemyShip();
				this._timeSinceShieldDamage = 0;
				this.CurrentShield -= projectileClass.Damage;
				//Destroy(projectileClass.gameObject);
			}
		}

		/// <summary>
		/// Called when the ship is destroyed
		/// </summary>
		protected void OnShipDestroy()
		{
			GameController.CurrentInstance.DestroyShip(this);
            var dropLoot = this.GetComponent<DropsLoot>();
            if (dropLoot != null)
            {
                dropLoot.GenerateContainer();
            }

			Destroy(this.gameObject);
		}
	}
}
