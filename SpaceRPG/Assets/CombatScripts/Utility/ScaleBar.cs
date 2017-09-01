//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ScaleBar.cs">
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
	/// Represents a bar that scales based on the given value
	/// </summary>
	public class ScaleBar : MonoBehaviour
	{
		/// <summary>
		/// The ratio 
		/// </summary>
		public float ratio;

		/// <summary>
		/// The total length
		/// </summary>
		private const float TotalLength = 0.1f;

		/// <summary>
		/// Called once per frame
		/// </summary>
		protected void Update()
		{
			this.transform.localScale = new Vector3(this.ratio, 1, 1);
			this.transform.localPosition = new Vector3((this.ratio - 1)* TotalLength /2, 0, 0);
		}
	}
}
