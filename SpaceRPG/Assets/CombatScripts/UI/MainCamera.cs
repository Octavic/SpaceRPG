//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="PlayerShip.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.CombatScripts.UI
{
	using Assets.CombatScripts.Ships;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using UnityEngine;
	using Utility;

	/// <summary>
	/// Controls the main camera
	/// </summary>
	public class MainCamera : MonoBehaviour
	{
		/// <summary>
		/// Gets the current instance
		/// </summary>
		public static MainCamera CurrentInstance
		{
			get
			{
				if (MainCamera._currentInstance == null)
				{
					MainCamera._currentInstance = GameObjectFinder.FindGameObjectWithTag(Tags.MainCamera).GetComponent<MainCamera> ();
				}

				return MainCamera._currentInstance;
			}

		}

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
		/// The current instance
		/// </summary>
		private static MainCamera _currentInstance;

		/// <summary>
		/// Called once per frame
		/// </summary>
		protected void FixedUpdate()
		{
			if (FocusTarget == null)
			{
				FocusTarget = PlayerController.CurrentInstance.gameObject;
			}

			var newPosition = new Vector3(FocusTarget.transform.position.x, FocusTarget.transform.position.y, this._defaultZ);
			this.transform.position = Vector3.Lerp(this.transform.position, newPosition, 0.2f);
		}
	}
}
