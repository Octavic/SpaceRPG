//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="PlayerShip.cs">
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

	/// <summary>
	/// Controls the main camera
	/// </summary>
	public class MainCamera : MonoBehaviour
	{
		/// <summary>
		/// What the camera is focused on
		/// </summary>
		public GameObject FocusTarget;

		/// <summary>
		/// The default Z value for the camera
		/// </summary>
		private float _defaultZ;

		/// <summary>
		/// Called when initialzed
		/// </summary>
		protected void Start()
		{
			this._defaultZ = this.transform.position.z;
		}

		/// <summary>
		/// Called once per frame
		/// </summary>
		protected void Update()
		{
			this.transform.position = new Vector3(FocusTarget.transform.position.x, FocusTarget.transform.position.y, this._defaultZ);
		}
	}
}
