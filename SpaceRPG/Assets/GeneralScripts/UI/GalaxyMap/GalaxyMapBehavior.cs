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
	using Utility;

	/// <summary>
	/// Defines the galaxy map UI element
	/// </summary>
	public class GalaxyMapBehavior : MonoBehaviour
	{
		/// <summary>
		/// Prefab for an empty tile
		/// </summary>
		public GameObject EmptyTilePrefab;

		/// <summary>
		/// Prefab for a space station
		/// </summary>
		public GameObject SpacesStationPrefab;

		/// <summary>
		/// Prefab for a asteroid field
		/// </summary>
		public GameObject AsteroidPrefab;

		/// <summary>
		/// Size of each tile
		/// </summary>
		public float TileSize;

		/// <summary>
		/// Color of the lowest security tile
		/// </summary>
		public Color LowestSecurityColor;

		/// <summary>
		/// Color of the highest security tile
		/// </summary>
		public Color HighestSecurityColor;

		/// <summary>
		/// Gets the current instance
		/// </summary>
		public static GalaxyMapBehavior CurrentInstance
		{
			get
			{
				return GalaxyMapBehavior._currentInstance;
			}
		}

		/// <summary>0
		/// Gets the current map object containing all of the data
		/// </summary>
		public GalaxyMap Map { get; private set; }

		/// <summary>
		/// The current instance
		/// </summary>
		protected static GalaxyMapBehavior _currentInstance;

		/// <summary>
		/// Gets the color of a tile based on its security rating
		/// </summary>
		/// <param name="securityRating">The security rating</param>
		/// <returns></returns>
		public Color GetColor(float securityRating)
		{
			return LowestSecurityColor.Lerp(this.HighestSecurityColor, securityRating);
		}

		/// <summary>
		/// Update the map
		/// </summary>
		public void UpdateMap()
		{

		}

		/// <summary>
		/// Called once in the beginning for initialization
		/// </summary>
		protected void Start()
		{
			this.Map = new GalaxyMap();
			GalaxyMapBehavior._currentInstance = this;

			// Generating map is expensive, so don't destroy this UI item
			DontDestroyOnLoad(this.gameObject);

			// Loop through the map and generate the tiles
			for (int i = 0; i < this.Map.Height; i++)
			{
				for (int j = 0; j < this.Map.Width; j++)
				{
					var curTile = this.Map.Tiles[j, i];
					GameObject prefab = null;
					switch (curTile.TileType)
					{
						case GalaxyMapTileEnum.SpaceStation:
						{
							prefab = this.SpacesStationPrefab;
							break;
						}
					}
				}
			}
		}
    }
}