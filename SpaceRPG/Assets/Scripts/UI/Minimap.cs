//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Minimap.cs">
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
	using Ships;
	using UnityEngine.UI;
	using Utility;

	/// <summary>
	/// The starmap 
	/// </summary>
	public class Minimap : MonoBehaviour
	{
		/// <summary>
		/// A list of normal icon colors
		/// </summary>
		public List<Color> NormalIconColors;

		/// <summary>
		/// A list of icon colors when selected 
		/// </summary>
		public List<Color> HighlightedIconColors;

		/// <summary>
		/// The prefab for an icon
		/// </summary>
		public GameObject IconPrefab;

		/// <summary>
		/// The current instance
		/// </summary>
		private static Minimap _currentInstance;

		/// <summary>
		/// Gets the current instance of the minimap
		/// </summary>
		public static Minimap CurrentInstance
		{
			get
			{
				if (Minimap._currentInstance == null)
				{
					Minimap._currentInstance = GameObjectFinder.FindGameObjectWithTag(Tags.Minimap).GetComponent<Minimap>();
				}

				return Minimap._currentInstance;
			}
		}

		/// <summary>
		/// Creates a new ship icon
		/// </summary>
		/// <param name="newShip">New ship to be targeted</param>
		/// <returns>The newly created icon</returns>
		public MinimapIcon CreateShipIcon(Ship newShip)
		{
			var newIcon = Instantiate(Minimap._currentInstance.IconPrefab, Minimap._currentInstance.transform, false);
			var newIconComponent = newIcon.GetComponent<MinimapIcon>();
			newIconComponent.target = newShip;
			newIcon.GetComponent<Image>().color = this.GetColor(newShip.ShipAttitude, false);
			return newIconComponent;
		}

		/// <summary>
		/// Gets the icon color based on the attitude enum
		/// </summary>
		/// <param name="shipAttitude">Target attitude</param>
		/// <param name="isHighlighted">If the color is normal or highlighted</param>
		/// <returns>Icon color</returns>
		public Color GetColor(ShipAttitudeEnum shipAttitude, bool isHighlighted)
		{
			if (!isHighlighted)
			{
				return this.NormalIconColors[(int)shipAttitude];
			}
			else
			{
				return this.HighlightedIconColors[(int)shipAttitude];
			}
		}
	}
}
