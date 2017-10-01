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
		/// The size  of the camera when right clicking on a tile
		/// </summary>
		public float FocusSize;

		/// <summary>
		/// THe speed at which the camera pans
		/// </summary>
		public float PanScale;

		/// <summary>
		/// The tile info
		/// </summary>
		public TileInfoBehavior TileInfo;

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
		private GalaxyMapTileBehavior _focusTile;

		/// <summary>
		/// The default depth of the camera
		/// </summary>
		private float _defaultZ;

		/// <summary>
		/// Used for tile info ui
		/// </summary>
		public void GenerateSafestPath()
		{
			var testX = (int)(this.transform.position.x + 0.95f / 0.96f);
			var testY = (int)(this.transform.position.y + 0.95f / 0.96f);
			GalaxyMapBehavior.CurrentInstance.GeneratePath(GalaxyMapPathPriorityEnum.LeastCrimeRating,
				new MapCoordinate(testX, testY));
		}

		/// <summary>
		/// Generates the fastest path
		/// </summary>
		public void GenerateFastestPath()
		{
			GalaxyMapBehavior.CurrentInstance.GeneratePath(GalaxyMapPathPriorityEnum.MostFuelEfficient,
				this._focusTile.Coordinate);
		}

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
				this._focusTile = null;
			}

			// If left click is down, pan the map
			if (Input.GetMouseButton(0))
			{
				var mouseX = Input.GetAxis("MouseX");
				var mouseY = Input.GetAxis("MouseY");
				if (Math.Abs(mouseX) > 0.5f || Math.Abs(mouseY) > 0.5f)
				{
					this._focusTile = null;
					this.TileInfo.gameObject.SetActive(false);
				}

				this.transform.position -= new Vector3(mouseX, mouseY) * this.PanScale * this.CurCameraSize;
			}
			// On right click, pan to the tile clicked and show info
			else if (Input.GetMouseButtonDown(1))
			{
				RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
				if (hit.collider != null)
				{
					var hitTile = hit.collider.gameObject.GetComponent<GalaxyMapTileBehavior>();
					if (hitTile != null)
					{
						this._focusTile = hitTile;
						this.TileInfo.RenderTile(hitTile);
					}
				}
			}

			if (this._focusTile != null)
			{
				this.transform.position = Vector3.Lerp(
					this.transform.position, 
					new Vector3(this._focusTile.transform.position.x, this._focusTile.transform.position.y, _defaultZ), 
					0.15f);
				this.CurCameraSize= this.CurCameraSize.Lerp(this.FocusSize, 0.15f);
			}
		}
	}
}
