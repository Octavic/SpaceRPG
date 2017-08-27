//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="SpeedMeterMarker.cs">
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
	using UnityEngine.UI;
	using Ships;

	/// <summary>
	/// Moves the speed meter based on the player ship's velocity
	/// </summary>
	public class SpeedMeterMarker : MonoBehaviour
	{
		/// <summary>
		/// The x off set
		/// </summary>
		private float _xOffset;

		/// <summary>
		/// The max y offset
		/// </summary>
		private float _totalHeight;

		/// <summary>
		/// Called in the very beginning
		/// </summary>
		protected void Start()
		{
			this._xOffset = this.transform.localPosition.x;
			this._totalHeight = Math.Abs(this.transform.localPosition.y) * 2;
		}

		/// <summary>
		/// Called once per frame
		/// </summary>
		protected void Update()
		{
			var shipComponent = PlayerController.CurrentInstance.ShipComponent;
			var ratio = (shipComponent.RGBD.velocity.magnitude) / shipComponent.MaximumSpeed;
			this.transform.localPosition = new Vector2(this._xOffset, _totalHeight * (ratio - 0.5f));
		}
	}
}
