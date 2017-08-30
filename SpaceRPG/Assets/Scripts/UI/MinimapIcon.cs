//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MinimapIcon.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.Scripts.UI
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using UnityEngine;
	using UnityEngine.UI;
	using Ships;
	using Settings;

	/// <summary>
	/// An icon on the map representing the ship
	/// </summary>
	public class MinimapIcon : MonoBehaviour
	{
		/// <summary>
		/// The target that this icon is representing
		/// </summary>
		public Ship target;

		/// <summary>
		/// Gets or sets the icon's color
		/// </summary>
		public Color IconColor
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
		/// The image component
		/// </summary>
		private Image _imageComponent;

		/// <summary>
		/// Used for initializtion
		/// </summary>
		protected void Start()
		{
			this._imageComponent = this.GetComponent<Image>();
		}

		/// <summary>
		/// Called once per frame
		/// </summary>
		protected void Update()
		{
			this.transform.localPosition = target.transform.position * MinimapSettings.Scale;
		}
	}
}
