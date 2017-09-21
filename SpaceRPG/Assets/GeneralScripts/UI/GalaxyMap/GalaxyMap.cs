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
    public class GalaxyMap
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
		/// Creates a new instance of the <see cref="GalaxyMap"/> class
		/// </summary>
		/// <param name="width">width of the map</param>
		/// <param name="height">height of the map</param>
		public GalaxyMap(int width, int height)
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
					newTile.PopulationRating = GlobalRandom.Next(0, 1, 10);
					newTile.CrimeRating = GlobalRandom.Next(0, 1, 10);
					this.Tiles[x, y] = newTile;
				}
			}
		}
    }
}
