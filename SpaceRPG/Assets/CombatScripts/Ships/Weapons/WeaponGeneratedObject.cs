﻿//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="WeaponGeneratedObject.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.CombatScripts.Ships.Weapons
{
	using Assets.CombatScripts.Utility;
	using Assets.CombatScripts.Ships;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	/// <summary>
	/// Describes an object generated when a weapon is fired
	/// </summary>
	public abstract class WeaponGeneratedObject : DelayedSelfDestruct
	{
		/// <summary>
		/// The faction who fired this object
		/// </summary>
		public int FactionId;

		/// <summary>
		/// The amount of damage done
		/// </summary>
		public float Damage;
	}
}
