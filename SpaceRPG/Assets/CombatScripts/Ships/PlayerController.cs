//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="PlayerShip.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.CombatScripts.Ships
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;
    using Utility;
    using Settings;
	using Weapons;
	using UI;

	/// <summary>
	/// A ship that's controlled by a player
	/// </summary>
	public class PlayerController : MonoBehaviour
	{
		/// <summary>
		/// The current instance of the player's ship
		/// </summary>
		public static PlayerController CurrentInstance { get; private set; }

		/// <summary>
		/// How fast the throttle adjusts
		/// </summary>
		public float ThrottleAdjustSpeed;

		/// <summary>
		/// If the throttle is controlled with adjustment, or just tap/release
		/// </summary>
		public bool AdvancedThrottleControl;

		/// <summary>
		/// The currently selected weapon system
		/// </summary>
		public int CurrentSelectedSystemIndex { get; private set; }

		/// <summary>
		/// Gets or sets the current throttle
		/// </summary>
		public float CurrentThrottle
		{
			get
			{
				return this._shipComponent.CurrentThrottle;
			}
			set
			{
				this._shipComponent.CurrentThrottle = value;
			}
		}

		/// <summary>
		/// Gets the ship's weapon systems
		/// </summary>
		public List<ShipWeaponSystem> WeaponSystems
		{
			get
			{
				if (this._shipComponent == null)
				{
					this._shipComponent = this.GetComponent<Ship>();
				}

				return this._shipComponent.WeaponSystems;
			}
		}

		/// <summary>
		/// Gets the related ship component
		/// </summary>
		public Ship ShipComponent
		{
			get
			{
				return this._shipComponent;
			}
		}

		/// <summary>
		/// The main camera
		/// </summary>
		private Camera _mainCamera;

		/// <summary>
		/// The button used to select weapons
		/// </summary>
		private IList<ButtonNames> SelectWeaponButtons;

		/// <summary>
		/// The ship component
		/// </summary>
		private Ship _shipComponent;

		/// <summary>
		/// Selects a weapon system
		/// </summary>
		/// <param name="index">Index of the weapon system</param>
		public void SelectWeaponSystem(int index)
		{
			if (index >= this._shipComponent.WeaponSystems.Count)
			{
				return;
			}

			Cursor.SetCursor(CursorManager.CurrentInstance.LockonCursors[index], new Vector2(9,9), CursorMode.Auto);
			this.CurrentSelectedSystemIndex = index;
		}

		/// <summary>
		/// Sets the target for the current selected weapon system
		/// </summary>
		/// <param name="target">Target ship</param>
		public void SetTargetForCurrentWeaponSystem(Ship target)
		{
			if (this.CurrentSelectedSystemIndex != -1)
			{
				var currentSystem = this._shipComponent.WeaponSystems[this.CurrentSelectedSystemIndex];
				currentSystem.CurrentTarget = target;
				Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
				this.CurrentSelectedSystemIndex = -1;
			}
		}

		/// <summary>
		/// Returns true if the target ship is targeted by one of the player's weapon system
		/// </summary>
		/// <param name="ship">Ship target</param>
		/// <returns>True if the ship was targeted by one of the player's weapon system</returns>
		public bool IsShipTargeted(Ship ship)
		{
			return this.WeaponSystems.Any(system => system.CurrentTarget == ship);
		}

		/// <summary>
		/// Called when a new instance of the class is created
		/// </summary>
		protected void Start()
		{
			this._shipComponent = this.GetComponent<Ship>();
			PlayerController.CurrentInstance = this;
			this.CurrentSelectedSystemIndex = -1;
			this._mainCamera = GameObjectFinder.FindGameObjectWithTag(Tags.MainCamera).GetComponent<Camera>();
			this.SelectWeaponButtons = new List<ButtonNames>()
			{
				ButtonNames.Weapon1,
				ButtonNames.Weapon2,
				ButtonNames.Weapon3,
				ButtonNames.Weapon4
			};
		}

		/// <summary>
		/// Called once per frame
		/// </summary>
		protected void Update()
        {
            // Turn the ship
            var rotateDirection = InputHelper.GetAxis(ButtonNames.Turn);
            this._shipComponent.TryTurn(rotateDirection * this._shipComponent.RotationSpeed * Time.deltaTime);

            // Adjust throttle
            var throttleAdjustInput = InputHelper.GetAxis(ButtonNames.AdjustThrottle);
            this._shipComponent.CurrentThrottle = this._shipComponent.CurrentThrottle + throttleAdjustInput * this.ThrottleAdjustSpeed * Time.deltaTime;
			if (!this.AdvancedThrottleControl && throttleAdjustInput == 0)
			{
				this.CurrentThrottle = 0;
			}

            // Set throttle
            var throttleSetInput = InputHelper.GetAxis(ButtonNames.SetThrottle);
            if (throttleSetInput > 0)
            {
                this._shipComponent.CurrentThrottle = 1;
            }
            else if (throttleSetInput < 0)
            {
                this._shipComponent.CurrentThrottle = 0;
            }

            // Apply side thrust
            var sideThrust = InputHelper.GetAxis(ButtonNames.SideThrust);
            if (sideThrust != 0)
            {
				this._shipComponent.ApplySideThrust(sideThrust < 0);
            }

            // Apply break
            if (InputHelper.GetButton(ButtonNames.Break))
            {
                this._shipComponent.ApplyBreak();
            }

			// Apply select weapon system
			for (int i = 0; i < this.SelectWeaponButtons.Count; i++)
			{
				if (InputHelper.GetButtonDown(this.SelectWeaponButtons[i]))
				{
					this.SelectWeaponSystem(i);
					break;
				}
			}

			// Apply target select
			if (Input.GetMouseButtonDown(1))
			{
				RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
				if (hit.collider != null && hit.collider.gameObject.tag == Tags.Ship.ToString())
				{
					this.SetTargetForCurrentWeaponSystem(hit.collider.gameObject.GetComponent<Ship>());
				}
				else
				{
					this.SetTargetForCurrentWeaponSystem(null);
				}
			}
        }
    }
}
