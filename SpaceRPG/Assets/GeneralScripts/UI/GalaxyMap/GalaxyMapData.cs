//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="GalaxyMap.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.GeneralScripts.Utility;

namespace Assets.GeneralScripts.UI.GalaxyMap
{
    /// <summary>
    /// Describes the galaxy map
    /// </summary>
    [Serializable]
    public class GalaxyMapData
    {
        /// <summary>
        /// Gets the tiles for this galaxy map
        /// </summary>
        public GalaxyMapTile[,] Tiles { get; private set; }

		/// <summary>
		/// width of the map
		/// </summary>
		public int Width;

		/// <summary>
		/// Height of the map
		/// </summary>
		public int Height;

		/// <summary>
		/// Creates a new instance of the <see cref="GalaxyMapData"/> class
		/// </summary>
		/// <param name="width">width of the map</param>
		/// <param name="height">height of the map</param>
		public GalaxyMapData(int width, int height)
		{
			this.Width = width;
			this.Height = height;
			this.Tiles = new GalaxyMapTile[width, height];

			// For now, just scramble the map
			var tileWeightedList = new WeightedRandomList<GalaxyMapTileEnum>();
			tileWeightedList.AddNewItem(GalaxyMapTileEnum.SpaceStation, 10);
			tileWeightedList.AddNewItem(GalaxyMapTileEnum.Empty, 90);
			tileWeightedList.AddNewItem(GalaxyMapTileEnum.Asteroid, 25);
			for (int y = 0; y < this.Height; y++)
			{
				for (int x = 0; x < this.Width; x++)
				{
					var newTile = new GalaxyMapTile();
					newTile.TileType = tileWeightedList.GetRandomItem();
					newTile.PopulationRating = GlobalRandom.Next(0, 1, 20);
					newTile.CrimeRating = GlobalRandom.Next(0, 1, 20);
					this.Tiles[x, y] = newTile;
				}
			}
		}

		/// <summary>
		/// Checks if a coordinate is within map
		/// </summary>
		/// <param name="c">coordinate</param>
		/// <returns>True if the coordinate is valid</returns>
		public bool IsValidCoordinate(MapCoordinate c)
		{
			return (c.X >= 0 && c.X < this.Width && c.Y >= 0 && c.Y < this.Height);
		}

		/// <summary>
		/// Gets or sets the tile at the target coordinate
		/// </summary>
		/// <param name="coordinate">The target coordinate</param>
		/// <returns>the result tile if set</returns>
		public GalaxyMapTile this[MapCoordinate coordinate]
		{
			get
			{
				if (this.IsValidCoordinate(coordinate))
				{
					return this.Tiles[coordinate.X, coordinate.Y];
				}

				return null;
			}
			set
			{
				if (this.IsValidCoordinate(coordinate))
				{
					this.Tiles[coordinate.X, coordinate.Y] = value;
				}
				else
				{
					throw new IndexOutOfRangeException("Not a valid coordinate!");
				}
			}
		}

		/// <summary>
		/// Gets the sourrounding coordinates
		/// </summary>
		/// <param name="c">center coordinate</param>
		/// <returns>A list of surrounding coordinates</returns>
		public IList<MapCoordinate> GetSurroundingCoordinates(MapCoordinate c)
		{
			var result = new List<MapCoordinate>();
			foreach (var direction in _moveDirections)
			{
				var candidate = c + direction;
				if (this.IsValidCoordinate(candidate))
				{
					result.Add(candidate); 
				}
			}

			return result;
		}

		/// <summary>
		/// A list of possible move directions
		/// </summary>
		private static readonly MapCoordinate[] _moveDirections = {
			new MapCoordinate(0, 1),
			new MapCoordinate(0, -1),
			new MapCoordinate(1, 0),
			new MapCoordinate(-1, 0)
		};
    }
}
