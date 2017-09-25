﻿//  --------------------------------------------------------------------------------------------------------------------
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
		/// THe speed at which the camera pans
		/// </summary>
		public float PanScale;

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
		/// The object that this camera is currently focusing on
		/// </summary>
		private Transform _focusTransform;

		/// <summary>
		/// The default depth of the camera
		/// </summary>
		private float _defaultZ;

		/// <summary>
		/// Called once in the beginning
		/// </summary>
		protected void Start()
		{
			this._cameraComponent = this.GetComponent<Camera>();
			this._defaultZ = this.transform.position.z;
		}

		/// <summary>
		/// Called once per frame
		/// </summary>
		protected void Update()
		{
			var scroll = Input.GetAxis("MouseScroll");
			if (scroll != 0)
			{
				this.CurCameraSize -= scroll;
				this._focusTransform = null;
			}

			// If left click is down, pan the map
			if (Input.GetMouseButton(0))
			{
				this._focusTransform = null;
				var mouseX = Input.GetAxis("MouseX");
				var mouseY = Input.GetAxis("MouseY");
				this.transform.position -= new Vector3(mouseX, mouseY) * this.PanScale * this.CurCameraSize;
			}
			// On right click, pan to the tile clicked and show info
			else if (Input.GetMouseButtonDown(1))
			{
				RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
				if (hit.collider != null)
				{
					this._focusTransform = hit.collider.gameObject.transform;
				}
			}

			if (this._focusTransform != null)
			{
				this.transform.position = Vector3.Lerp(
					this.transform.position, 
					new Vector3(this._focusTransform.position.x, this._focusTransform.position.y, _defaultZ), 
					0.15f);
				this.CurCameraSize= this.CurCameraSize.Lerp(3, 0.15f);
			}
		}
	}
}
