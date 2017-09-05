//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="DelayedSelfDestruct.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.CombatScripts.Utility
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using UnityEngine;

	/// <summary>
	/// Destroys the game object after the given delay
	/// </summary>
	public class DelayedSelfDestruct : MonoBehaviour
	{
		/// <summary>
		/// How many seconds until self destruct
		/// </summary>
		public float DeathDelayInSeconds;

		/// <summary>
		/// Called once per frame
		/// </summary>
		protected virtual void Update()
		{
			this.DeathDelayInSeconds -= Time.deltaTime;
			if (this.DeathDelayInSeconds <= 0)
			{
				Destroy(this.gameObject);
			}
		}
	}
}
