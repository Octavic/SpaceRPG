//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="BackgroundItem.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.Scripts
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using UnityEngine;
	using UI;

	/// <summary>
	/// Describes the background item
	/// </summary>
	public class BackgroundItem : MonoBehaviour
	{
		/// <summary>
		/// The scale of the item
		/// </summary>
		public float Scale;

		/// <summary>
		/// The main camera
		/// </summary>
		private GameObject _mainCamera;

		/// <summary>
		/// Used as initialization
		/// </summary>
		protected void Start()
		{
			this._mainCamera = MainCamera.CurrentInstance.gameObject;
		}

		/// <summary>
		/// Called once per frame
		/// </summary>
		protected void Update()
		{
			this.transform.position = this._mainCamera.transform.position * this.Scale;
		}
	}
}
