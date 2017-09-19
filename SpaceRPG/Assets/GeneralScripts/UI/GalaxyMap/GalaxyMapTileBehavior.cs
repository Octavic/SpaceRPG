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
		/// gets or sets the color of the tile
		/// </summary>
		public Color Color
		{
			get
			{
				return this._imageComponent.color;
			}
			set
			{
				this._imageComponent.color = value;
			}
		}

		/// <summary>
		/// Gets the image component;
		/// </summary>
		private Image _imageComponent;

		/// <summary>
		/// Used for initialization
		/// </summary>
		protected void Start()
		{
			this._imageComponent = this.GetComponent<Image>();
		}
	}
}
