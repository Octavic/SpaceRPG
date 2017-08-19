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
        /// Gets the main color of the ship
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
        /// The main sprite
        /// </summary>
        private SpriteRenderer _mainSpriteRenderer;
        
        /// <summary>
        /// Called in the betginning
        /// </summary>
        private void Start()
        {
            this._mainSpriteRenderer = this.MainPaintjob.GetComponent<SpriteRenderer>();
        }

        /// <summary>
        /// Called once per frame
        /// </summary>
        private void Update()
        {
        }
    }
}
