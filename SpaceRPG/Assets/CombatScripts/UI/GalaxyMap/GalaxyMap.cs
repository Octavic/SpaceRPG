//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="GalaxyMap.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.CombatScripts.UI.GalaxyMap
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using UnityEngine;

	/// <summary>
	/// Defines the galaxy map UI element
	/// </summary>
	public class GalaxyMap : MonoBehaviour
	{
		/// <summary>0
		/// A 2D array representing the map tiles
		/// </summary>
		public GalaxyMapTile[,] MapTiles;
	}
}
