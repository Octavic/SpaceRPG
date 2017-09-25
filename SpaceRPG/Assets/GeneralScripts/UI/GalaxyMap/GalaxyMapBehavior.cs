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
		/// Width of the map
		/// </summary>
		public int MapWidth;

		/// <summary>
		/// Height of the map
		/// </summary>
		public int MapHeight;

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
				if (GalaxyMapBehavior._currentInstance == null)
				{
					GalaxyMapBehavior._currentInstance = 
						GameObjectFinder.FindGameObjectWithTag(Tags.GalaxyMap).GetComponent<GalaxyMapBehavior>();
				}

				return GalaxyMapBehavior._currentInstance;
			}
		}

		/// <summary>0
		/// Gets the current map object containing all of the data
		/// </summary>
		public GalaxyMapData Map { get; private set; }

		/// <summary>
		/// The current instance
		/// </summary>
		protected static GalaxyMapBehavior _currentInstance;

		/// <summary>
		/// Calculates a new path
		/// </summary>
		public void CalculatePath()
		{
		}

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
		/// Gets the tile at the given coordinate
		/// </summary>
		/// <param name="coordinate">Target coordinate</param>
		/// <returns>tile at that coordinate, null if out of range</returns>
		public GalaxyMapTile this[MapCoordinate coordinate]
		{
			get
			{
				return this.Map[coordinate];
			}
			set
			{
				this.Map[coordinate] = value;
			}
		}

		/// <summary>
		/// Redraw the map
		/// </summary>
		public void RedrawMap()
		{

		}

		/// <summary>
		/// Called once in the beginning for initialization
		/// </summary>
		protected void Start()
		{
			this.Map = new GalaxyMapData(this.MapWidth, this.MapHeight);
			GalaxyMapBehavior._currentInstance = this;

			// Generating map is expensive, so don't destroy this UI item
			DontDestroyOnLoad(this.gameObject);

			// Loop through the map and generate the tiles
			for (int y = 0; y < this.Map.Height; y++)
			{
				for (int x = 0; x < this.Map.Width; x++)
				{
					var curTile = this.Map.Tiles[x, y];
					GameObject prefab = null;
					switch (curTile.TileType)
					{
						case GalaxyMapTileEnum.SpaceStation:
							{
								prefab = this.SpacesStationPrefab;
								break;
							}
						case GalaxyMapTileEnum.Asteroid:
							{
								prefab = this.AsteroidPrefab;
								break;
							}
						default:
							{
								prefab = this.EmptyTilePrefab;
								break;
							}
					}

					// Instantiate a new tile
					var newTileObject = Instantiate(prefab, this.transform);
					var newTileBehavior = newTileObject.GetComponent<GalaxyMapTileBehavior>();

					// Assign it to the current map and set color and position
					newTileBehavior.Tile = curTile;
					newTileBehavior.Coordinate = new MapCoordinate(x, y);
					newTileBehavior.Color = this.GetColor(curTile.CrimeRating);
					newTileObject.transform.localPosition = new Vector3(x * this.TileSize, y * this.TileSize, 0);
				}
			}
		}
    }
}