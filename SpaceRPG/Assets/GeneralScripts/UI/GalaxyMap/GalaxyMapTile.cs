//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="GalaxyMapTile.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.GeneralScripts.UI.GalaxyMap;

namespace Assets.GeneralScripts.UI.GalaxyMap
{
	/// <summary>
	/// A map tile 
	/// </summary>
	public class GalaxyMapTile
	{
		/// <summary>
		/// The type of tile that this is
		/// </summary>
		public GalaxyMapTileEnum TileType;

		/// <summary>
		/// A scale of population from 0 to 1. 1 being extremely populated and 0 being completely empty
		/// </summary>
		public float PopulationRating;

		/// <summary>
		/// A scale of government security from 0 to 1. 1 being extremely secure and 0 being lawless land
		/// </summary>
		public float CrimeRating;
	}
}
