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
		/// Prefab for a section of the path
		/// </summary>
		public GameObject PathSectionPrefab;

		/// <summary>
		/// Prefab for the path's node
		/// </summary>
		public GameObject PathNodePrefab;

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
		/// Transform for the path's parent
		/// </summary>
		public Transform PathParentTransform;

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
		/// Gets the player's current coordinate
		/// </summary>
		public MapCoordinate PlayerCurrentCoordiante { get; private set; }

		/// <summary>
		/// An array of tile behaviors
		/// </summary>
		private GalaxyMapTileBehavior[,] _tileBehaviors;

		/// <summary>
		/// The current instance
		/// </summary>
		private static GalaxyMapBehavior _currentInstance;

		/// <summary>
		/// Calculates a new path
		/// </summary>
		public void CalculatePath(MapCoordinate destination, GalaxyMapPathPriorityEnum priority)
		{
			var newPath = new GalaxyMapPath(this.Map, this.PlayerCurrentCoordiante, destination, priority);
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
		/// Generates a path with the given priority
		/// </summary>
		/// <param name="priority">target priority</param>
		public void GeneratePath(GalaxyMapPathPriorityEnum priority, MapCoordinate destination)
		{
			var path = new GalaxyMapPath(this.Map, new MapCoordinate(0, 0), destination, priority);
			this.RenderPath(path);
		}

		/// <summary>
		/// Called once in the beginning for initialization
		/// </summary>
		protected void Start()
		{
			this.Map = new GalaxyMapData(this.MapWidth, this.MapHeight);
			this._tileBehaviors = new GalaxyMapTileBehavior[this.MapWidth, this.MapHeight];
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
					this._tileBehaviors[x, y] = newTileBehavior;
				}
			}
		}

		/// <summary>
		/// generates a single node
		/// </summary>
		/// <param name="node">node</param>
		private void GeneratePathNode(MapCoordinate node)
		{
			var newNode = Instantiate(this.PathNodePrefab, this.PathParentTransform);
			newNode.transform.localPosition = new Vector3(node.X, node.Y) * this.TileSize;
		}

		/// <summary>
		/// Renders the map onto the map
		/// </summary>
		/// <param name="path">The target path to be rendered</param>
		private void RenderPath(GalaxyMapPath path)
		{
			// Delete old path if applicable
			this.PathParentTransform.gameObject.DestroyAllChildren();

			// Generate the first node
			this.GeneratePathNode(path.Nodes[0]);

			// Generate the new  path
			for (int i = 1; i < path.Nodes.Count; i++)
			{
				var prev = path.Nodes[i - 1];
				var next = path.Nodes[i];
				this.GeneratePathNode(next);

				var step = GalaxyMapPath.CalcStep(prev, next);
				var cur = prev;

				// Generate sections
				while (cur != next)
				{
					var newPath = Instantiate(this.PathSectionPrefab, this.PathParentTransform);
					newPath.transform.localPosition = new Vector3(
						(0.5f * step.X + cur.X) * this.TileSize, 
						(0.5f * step.Y + cur.Y) * this.TileSize);
					newPath.transform.localEulerAngles = new Vector3(0, 0, Mathf.Atan2(step.Y, step.X) * Mathf.Rad2Deg);
					cur = cur + step;
				}
			}
		}
    }
}