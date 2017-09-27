//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="TileInfoBehavior.cs">
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
	using UnityEngine.UI;
	using Utility;

	/// <summary>
	/// The tile information
	/// </summary>
	public class TileInfoBehavior : MonoBehaviour
	{
		/// <summary>
		/// A list of texture for tile types
		/// </summary>
		public List<Sprite> TileTypeSprites;

		/// <summary>
		/// The images for tile infos
		/// </summary>
		public Image TileTypeImage;

		/// <summary>
		/// The text for ratings
		/// </summary>
		public Text CrimeRatingText;
		public Text PopulationRatingText;

		/// <summary>
		/// Colors for the population
		/// </summary>
		public Color MinPopulationColor;
		public Color MaxPopulationColor;

		/// <summary>
		/// Hides the tile info object
		/// </summary>
		public void Hide()
		{
			this.gameObject.SetActive(false);
		}

		/// <summary>
		/// Renders the given tile
		/// </summary>
		/// <param name="tile">Target tile to be rendered</param>
		public void RenderTile(GalaxyMapTileBehavior tile)
		{
			this.gameObject.SetActive(true);
			this.TileTypeImage.sprite = this.TileTypeSprites[(int)tile.Tile. TileType];
			this.CrimeRatingText.text = tile.Tile.CrimeRating.ToString();
			this.CrimeRatingText.color = tile.Color;
			this.PopulationRatingText.text = tile.Tile.PopulationRating.ToString();
			this.PopulationRatingText.color = this.MinPopulationColor.Lerp(this.MaxPopulationColor, tile.Tile.PopulationRating);
		}
	}
}
