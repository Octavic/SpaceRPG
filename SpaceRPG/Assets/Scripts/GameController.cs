//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="GameController.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.Scripts
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using UnityEngine;
	using Utility;
	using Ships;
	using Assets.Scripts.UI;

	/// <summary>
	/// The overall game controller
	/// </summary>
	public class GameController : MonoBehaviour
	{
		/// <summary>
		/// The current instance of the game controller
		/// </summary>
		public static GameController CurrentInstance
		{
			get
			{
				if (GameController._currentInstance == null)
				{
					GameController._currentInstance = GameObjectFinder.FindGameObjectWithTag(Tags.GameController).GetComponent<GameController>();
				}

				return GameController._currentInstance;
			}
		}

		/// <summary>
		/// A list of ships and their respective icon types
		/// </summary>
		public IList<KeyValuePair<Ship, ShipIconTypeEnum>> ShipRelationships { get; private set; }

		/// <summary>
		/// The current instance of the game controller
		/// </summary>
		private static GameController _currentInstance;

		/// <summary>
		/// Registers a new ship onto the map
		/// </summary>
		/// <param name="newShip">New ship</param>
		/// <param name="iconType">The kind of icon that this ship is</param>
		public void RegisterShip(Ship newShip, ShipIconTypeEnum iconType)
		{
			this.ShipRelationships.Add(new KeyValuePair<Ship, ShipIconTypeEnum>(newShip, iconType));
			Minimap.UpdateMinimap(newShip, iconType);
		}

		/// <summary>
		/// Called once in the beginning
		/// </summary>
		protected void Start()
		{
			if (GameController._currentInstance == null)
			{
				GameController._currentInstance = this;
			}

			this.ShipRelationships = new List<KeyValuePair<Ship, ShipIconTypeEnum>>();
		}
	}
}
