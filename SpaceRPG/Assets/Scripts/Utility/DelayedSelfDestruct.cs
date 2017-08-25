//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="DelayedSelfDestruct.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.Scripts.Utility
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
		public float DelayInSeconds;

		/// <summary>
		/// Called once per frame
		/// </summary>
		protected virtual void Update()
		{
			this.DelayInSeconds -= Time.deltaTime;
			if (this.DelayInSeconds < 0)
			{
				Destroy(this.gameObject);
			}
		}
	}
}
