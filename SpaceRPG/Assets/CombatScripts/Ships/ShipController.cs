//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ShipController.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.CombatScripts.Ships
{
	using Assets.CombatScripts.Ships.Weapons;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using UnityEngine;

	/// <summary>
	/// Controls a ship
	/// </summary>
	public abstract class ShipController : MonoBehaviour
	{
		/// <summary>
		/// Gets the ship's weapon systems
		/// </summary>
		public List<ShipWeaponSystem> WeaponSystems
		{
			get
			{
				return this.ShipComponent.WeaponSystems;
			}
		}

		/// <summary>
		/// Gets the related ship component
		/// </summary>
		public Ship ShipComponent
		{
			get
			{
				if (this._shipComponent == null)
				{
					this._shipComponent = this.GetComponent<Ship>();
				}

				return this._shipComponent;
			}
		}

		/// <summary>
		/// The ship component
		/// </summary>
		protected Ship _shipComponent;
	}
}
