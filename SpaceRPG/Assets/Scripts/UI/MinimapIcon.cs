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
		/// Called once per frame
		/// </summary>
		protected void Update()
		{
			this.transform.localPosition = target.transform.position * MinimapSettings.Scale;
		}
	}
}
