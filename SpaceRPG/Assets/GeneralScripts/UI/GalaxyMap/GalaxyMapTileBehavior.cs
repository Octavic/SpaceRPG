//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="GalaxyMapTileBehavior.cs">
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
	/// The monobehavior describing a  galaxy map tile
	/// </summary>
	public class GalaxyMapTileBehavior : MonoBehaviour
	{
		/// <summary>
		/// Gets the tile that this piece is representing
		/// </summary>
		public GalaxyMapTile Tile { get; set; }

		/// <summary>
		/// Gets or sets the map coordinate
		/// </summary>
		public MapCoordinate Coordinate { get; set; }

		/// <summary>
		/// gets or sets the color of the tile
		/// </summary>
		public Color Color
		{
			get
			{
				return this._spriteRenderer.color;
			}
			set
			{
				if (this._spriteRenderer == null)
				{
					this._spriteRenderer = this.GetComponent<SpriteRenderer>();
				}

				this._spriteRenderer.color = value;
			}
		}

		/// <summary>
		/// Gets the image component;
		/// </summary>
		private SpriteRenderer _spriteRenderer;

		/// <summary>
		/// Used for initialization
		/// </summary>
		protected void Start()
		{
			this._spriteRenderer = this.GetComponent<SpriteRenderer>();
		}
	}
}
