//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="AIController.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.CombatScripts.Ships
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using UnityEngine;

	/// <summary>
	/// An AI controlled ship
	/// </summary>
	public class AIController : ShipController
    {
		/// <summary>
		/// Called in the beginning for initialization
		/// </summary>
		protected void Start()
		{
			var playerShip = PlayerController.CurrentInstance.ShipComponent;

			for (int i = 0; i < this.WeaponSystems.Count; i++)
			{
				var curSystem = this.WeaponSystems[i];
				curSystem.CurrentTarget = playerShip;
			}
		}
	}
}
