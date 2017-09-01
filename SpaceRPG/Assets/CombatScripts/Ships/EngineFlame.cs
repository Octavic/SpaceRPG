//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="EngineFlame.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.Scripts.Ships
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using UnityEngine;

	/// <summary>
	/// The engine flame manager
	/// </summary>
	public class EngineFlame : MonoBehaviour
	{
		/// <summary>
		/// The target ship that this flame is representing
		/// </summary>
		public Ship TargetShip;

		/// <summary>
		/// The type of engine this is
		/// </summary>
		public EngineTypeEnum EngineType;

		/// <summary>
		/// The engine length before this update
		/// </summary>
		private EngineFlameLengthEnum _previousLength;

		/// <summary>
		/// Called once per frame
		/// </summary>
		protected void Update()
		{
			var shipThrottle = TargetShip.CurrentThrottle;
			var newLength = EngineFlameLengthEnum.None;
			if (shipThrottle == 0)
			{
				newLength = EngineFlameLengthEnum.None;
			}
			else if (shipThrottle < 0.3f)
			{
				newLength = EngineFlameLengthEnum.Short;
			}
			else if (shipThrottle < 0.6f)
			{
				newLength = EngineFlameLengthEnum.Medium;
			}
			else
			{
				newLength = EngineFlameLengthEnum.Long;
			}

			if (newLength != this._previousLength)
			{
				this._previousLength = newLength;
				for (int i = 0; i < this.transform.childCount; i++)
				{
					Destroy(this.transform.GetChild(i).gameObject);
				}

				var newFlame = EngineFlameManager.GetFlame(this.EngineType, newLength);
				if (newFlame != null)
				{
					Instantiate(newFlame, this.transform, false);
				}
			}
		}
	}
}
