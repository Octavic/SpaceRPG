//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="GalaxyMapBehavior.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.GeneralScripts.UI.GalaxyMap
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using UnityEngine;

	/// <summary>
	/// Defines the galaxy map UI element
	/// </summary>
	public class GalaxyMapBehavior : MonoBehaviour
	{
		/// <summary>0
		/// Gets the current map object containing all of the data
		/// </summary>
		public GalaxyMap Map { get; private set; }

        /// <summary>
        /// Called once in the beginning for initialization
        /// </summary>
        protected void Start()
        {
            this.Map = new GalaxyMap();
        }
    }
}
