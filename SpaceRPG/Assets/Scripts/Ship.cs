//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Ship.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.Scripts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;
    using Utility;

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
        /// The amount of thrust that can stop the ship from moving
        /// </summary>
        public float KillMomentumThrust;
        
        /// <summary>
        /// How fast the ship rotates in degrees per second
        /// </summary>
        public float RotationSpeed;

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
        /// The main sprite renderer to control the ship's main color
        /// </summary>
        private SpriteRenderer _mainSpriteRenderer;

        /// <summary>
        /// The rigid body
        /// </summary>
        private Rigidbody2D _rgbd;

        /// <summary>
        /// Try to orbit the target
        /// </summary>
        /// <param name="target">Target that should be orbitted</param>
        /// <param name="distance">How far the orbit should be</param>
        protected void TryToOrbit(GameObject target, float distance)
        {

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
            // Limits velocity to maximum speed
            if (this._rgbd.velocity.magnitude > this.MaximumSpeed)
            {
                this._rgbd.velocity.Normalize();
                this._rgbd.velocity *= this.MaximumSpeed;
            }

            var v = AngleToVector.FromDegree(this.Rotation);
            this._rgbd.AddForce(v * this.MainEngineThrust * Time.deltaTime, ForceMode2D.Force);
        }
    }
}
