﻿//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="GameController.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.CombatScripts
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using UnityEngine;
	using Utility;
	using Ships;
	using Assets.CombatScripts.UI;
	using Assets.GeneralScripts.Utility;

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
		/// The current instance of the game controller
		/// </summary>
		private static GameController _currentInstance;

		/// <summary>
		/// A ship to map icon dictionary
		/// </summary>
		private IDictionary<Ship, MinimapIcon> _shipMapIconDictionary;

		/// <summary>
		/// The currently highlighted ship
		/// </summary>
		private Ship _currentlyHighlightedShip;

		/// <summary>
		/// Registers a new ship onto the map
		/// </summary>
		/// <param name="newShip">New ship</param>
		public void RegisterShip(Ship newShip)
		{
			if (this._shipMapIconDictionary == null)
			{
				this._shipMapIconDictionary = new Dictionary<Ship, MinimapIcon>();
			}

			this._shipMapIconDictionary[newShip] = Minimap.CurrentInstance.CreateShipIcon(newShip);
			ShipListUI.CurrentInstance.RegisterShip(newShip);
		}

		/// <summary>
		/// Destroy the ship
		/// </summary>
		/// <param name="deadShip">The ship that's been blown up</param>
		public void DestroyShip(Ship deadShip)
		{
			ShipListUI.CurrentInstance.DestroyShip(deadShip);
			Destroy(this._shipMapIconDictionary[deadShip].gameObject);
			this._shipMapIconDictionary.Remove(deadShip);
		}

		/// <summary>
		/// Sets the highlight status of a ship
		/// </summary>
		/// <param name="targetShip">The target ship</param>
		/// <param name="isHighlighted">If it's being highlighted or unhighlighted</param>
		public void SetHighlightStatus(Ship targetShip, bool isHighlighted)
		{
			MinimapIcon result;
			if (this._shipMapIconDictionary.TryGetValue(targetShip, out result))
			{
				var color = Minimap.CurrentInstance.GetColor(targetShip.ShipAttitude, isHighlighted);
				result.IconColor = color;
			}
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

			if (this._shipMapIconDictionary == null)
			{
				this._shipMapIconDictionary = new Dictionary<Ship, MinimapIcon>();
			}
		}
	}
}
