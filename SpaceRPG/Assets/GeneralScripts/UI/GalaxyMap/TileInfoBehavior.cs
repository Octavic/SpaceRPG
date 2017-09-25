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
		/// Renders the given tile
		/// </summary>
		/// <param name="tile">Target tile to be rendered</param>
		public void RenderTile(GalaxyMapTile tile)
		{
			this.gameObject.SetActive(true);
			this.TileTypeImage.sprite = this.TileTypeSprites[(int)tile.TileType];
			this.CrimeRatingText.text = tile.CrimeRating.ToString();
			this.PopulationRatingText.text = tile.PopulationRating.ToString();
		}
	}
}
