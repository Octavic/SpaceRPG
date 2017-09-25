//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="GalaxyMapCamera.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.GeneralScripts.UI.GalaxyMap
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using UnityEngine;
	using Utility;

	/// <summary>
	/// The main camera for the galaxy map
	/// </summary>
	public class GalaxyMapCamera : MonoBehaviour
	{
		/// <summary>
		/// The minimal size for the camera
		/// </summary>
		public float MinSize;

		/// <summary>
		/// The maximum size for the camera
		/// </summary>
		public float MaxSize;

		/// <summary>
		/// Gets or sets the current camera size
		/// </summary>
		private float CurCameraSize
		{
			get
			{
				return this._cameraComponent.orthographicSize;
			}
			set
			{
				if (value > this.MaxSize)
				{
					value = this.MaxSize;
				}
				else if (value < this.MinSize)
				{
					value = this.MinSize;
				}

				this._cameraComponent.orthographicSize = value;
			}
		}

		/// <summary>
		/// Gets the current instance
		/// </summary>
		public static GalaxyMapCamera CurrentInstance
		{
			get
			{
				if (GalaxyMapCamera._currentInstance == null)
				{
					GalaxyMapCamera._currentInstance = GameObjectFinder.FindGameObjectWithTag(Tags.MainCamera).GetComponent<GalaxyMapCamera>();
				}

				return GalaxyMapCamera._currentInstance;
			}
		}

		/// <summary>
		/// The current instance of the galaxy map
		/// </summary>
		private static GalaxyMapCamera _currentInstance;

		/// <summary>
		/// The camera component
		/// </summary>
		private Camera _cameraComponent;

		/// <summary>
		/// Called once in the begining
		/// </summary>
		protected void Start()
		{
			this._cameraComponent = this.GetComponent<Camera>();	
		}

		/// <summary>
		/// Called once per frame
		/// </summary>
		protected void Update()
		{
			this.CurCameraSize -= Input.GetAxis("MouseScroll");
		}
	}
}
