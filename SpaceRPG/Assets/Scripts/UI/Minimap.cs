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
		/// The player's color
		/// </summary>
		public List<Color> IconColors;

		/// <summary>
		/// The prefab for an icon
		/// </summary>
		public GameObject IconPrefab;

		/// <summary>
		/// The current instance
		/// </summary>
		private static Minimap _currentInstance;

		/// <summary>
		/// Registers a new ship onto the map
		/// </summary>
		/// <param name="newShip">New ship</param>
		/// <param name="iconType">The kind of icon that this ship is</param>
		public static void UpdateMinimap(Ship newShip, ShipIconTypeEnum iconType)
		{
			if (Minimap._currentInstance == null)
			{
				Minimap._currentInstance = GameObjectFinder.FindGameObjectWithTag(Tags.Minimap).GetComponent<Minimap>();
			}

			var newIcon = Instantiate(Minimap._currentInstance.IconPrefab, Minimap._currentInstance.transform, false);
			newIcon.GetComponent<MinimapIcon>().target = newShip;
			newIcon.GetComponent<Image>().color = Minimap._currentInstance.IconColors[(int)iconType];
		}
	}
}
