//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Ship.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.Scripts.Ships
{
    using Assets.Scripts.Settings;
	using Assets.Scripts.UI;
	using Assets.Scripts.Utility;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;
	using Weapons;

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
        /// How long it takes to stop the vessel from full speed
        /// </summary>
        public float TimeToStopFromFullSpeed;

		/// <summary>
		/// A list of ship weapon systems
		/// </summary>
		public List<ShipWeaponSystem> WeaponSystems;

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
            this.RGBD.velocity -= this.RGBD.velocity * Time.deltaTime / this.TimeToStopFromFullSpeed;

            // limits the velocity to minimum.
            if (this.RGBD.velocity.magnitude < ShipMovementSettings.MinimalSpeed)
            {
                this.RGBD.velocity = new Vector2(0, 0);
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

			this.RGBD.AddForce(sideVector * this.SideEngineThrust * Time.deltaTime, ForceMode2D.Force);
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
        }

        /// <summary>
        /// Called once per frame
        /// </summary>
        protected void Update()
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
        }
    }
}
