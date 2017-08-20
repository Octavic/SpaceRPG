//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Ship.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.Scripts
{
    using Assets.Scripts.Settings;
    using Assets.Scripts.Utility;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;

    /// <summary>
    /// Describes the basic functionality of a ship
    /// </summary>
	public abstract class Ship : MonoBehaviour
    {
        /// <summary>
        /// The main paintjob game object
        /// </summary>
        public GameObject MainPaintjob;

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
        /// The current throttle
        /// </summary>
        protected float _currentThrottle;

        /// <summary>
        /// Gets or sets the main color of the ship
        /// </summary>
        public Color MainColor
        {
            get
            {
                return this._mainSpriteRenderer.color;
            }
            set
            {
                this._mainSpriteRenderer.color = value;
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
            protected set
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
        /// Gets the current throttle from 0 to 1. 0 means no throttle, 1 meaning full throttle
        /// </summary>
        public float CurrentThrottle
        {
            get
            {
                return this._currentThrottle;
            }
            protected set
            {
                this._currentThrottle = value.LimitTo(0, 1);
            }
        }

        /// <summary>
        /// The main sprite renderer to control the ship's main color
        /// </summary>
        protected SpriteRenderer _mainSpriteRenderer;

        /// <summary>
        /// The rigid body
        /// </summary>
        protected Rigidbody2D _rgbd;

        ///// <summary>
        ///// Try to orbit the target
        ///// </summary>
        ///// <param name="target">Target that should be orbitted</param>
        ///// <param name="distance">How far the orbit should be</param>
        //protected void TryToOrbit(GameObject target, float distance)
        //{

        //}

        /// <summary>
        /// Try to stop the ship
        /// </summary>
        protected void ApplyBreak()
        {
            this._currentThrottle = 0;
            this._rgbd.velocity -= this._rgbd.velocity * Time.deltaTime / this.TimeToStopFromFullSpeed;

            // limits the velocity to minimum.
            if (this._rgbd.velocity.magnitude < ShipMovementSettings.MinimalSpeed)
            {
                this._rgbd.velocity = new Vector2(0, 0);
            }
        }
        
        /// <summary>
        /// Try to turn the ship with the given offset. Limited by how much can be turned overall
        /// </summary>
        /// <param name="degrees">Target degrees.</param>
        protected void TryTurn(float degrees)
        {
            var maximumPossibleTurn = this.RotationSpeed * Time.deltaTime;

            if (Math.Abs(degrees) > maximumPossibleTurn)
            {
                degrees = Math.Sign(degrees) * maximumPossibleTurn;
            }

            this.Rotation += degrees;
        }

        /// <summary>
        /// Called in the betginning
        /// </summary>
        protected virtual void Start()
        {
            this._mainSpriteRenderer = this.MainPaintjob.GetComponent<SpriteRenderer>();
            this._rgbd = this.GetComponent<Rigidbody2D>();
        }

        /// <summary>
        /// Called once per frame
        /// </summary>
        protected virtual void Update()
        {
            // Apply throttle
            this._rgbd.AddForce(this.ForwardVector * this.MainEngineThrust * Time.deltaTime * this.CurrentThrottle, ForceMode2D.Force);

            // Limits velocity to maximum speed
            if (this._rgbd.velocity.magnitude > this.MaximumSpeed)
            {
                this._rgbd.velocity.Normalize();
                this._rgbd.velocity *= this.MaximumSpeed;
            }
        }
    }
}
