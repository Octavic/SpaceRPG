//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="PlayerShip.cs">
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
    using Utility;
    using Settings;
	using Weapons;
	using UI;

	/// <summary>
	/// A ship that's controlled by a player
	/// </summary>
	public class PlayerShip : Ship
    {
		/// <summary>
		/// The current instance of the player's ship
		/// </summary>
		public static PlayerShip CurrentInstance { get; private set; }

		/// <summary>
		/// How fast the throttle adjusts
		/// </summary>
		public float ThrottleAdjustSpeed;

		/// <summary>
		/// The currently selected weapon system
		/// </summary>
		public int CurrentSelectedSystemIndex { get; private set; }

		/// <summary>
		/// The main camera
		/// </summary>
		private Camera _mainCamera;

		/// <summary>
		/// The button used to select weapons
		/// </summary>
		private IList<ButtonNames> SelectWeaponButtons;

		/// <summary>
		/// Selects a weapon system
		/// </summary>
		/// <param name="index">Index of the weapon system</param>
		public void SelectWeaponSystem(int index)
		{
			if (index >= this.WeaponSystems.Count)
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
				var currentSystem = this.WeaponSystems[this.CurrentSelectedSystemIndex];
				currentSystem.CurrentTarget = target;
				Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
				this.CurrentSelectedSystemIndex = -1;
			}
		}

		/// <summary>
		/// Called when a new instance of the class is created
		/// </summary>
		protected override void Start()
		{
			PlayerShip.CurrentInstance = this;
			this.CurrentSelectedSystemIndex = -1;
			this._mainCamera = GameObjectFinder.FindGameObjectWithTag(Tags.MainCamera).GetComponent<Camera>();
			this.SelectWeaponButtons = new List<ButtonNames>()
			{
				ButtonNames.Weapon1,
				ButtonNames.Weapon2,
				ButtonNames.Weapon3,
				ButtonNames.Weapon4
			};

			base.Start();
		}

		/// <summary>
		/// Called once per frame
		/// </summary>
		protected override void Update()
        {
            // Turn the ship
            var rotateDirection = InputHelper.GetAxis(ButtonNames.Turn);
            this.TryTurn(rotateDirection * RotationSpeed * Time.deltaTime);

            // Adjust throttle
            var throttleAdjustInput = InputHelper.GetAxis(ButtonNames.AdjustThrottle);
            this.CurrentThrottle = this.CurrentThrottle + throttleAdjustInput * this.ThrottleAdjustSpeed * Time.deltaTime;

            // Set throttle
            var throttleSetInput = InputHelper.GetAxis(ButtonNames.SetThrottle);
            if (throttleSetInput > 0)
            {
                this._currentThrottle = 1;
            }
            else if (throttleSetInput < 0)
            {
                this._currentThrottle = 0;
            }

            // Apply side thrust
            var sideThrust = InputHelper.GetAxis(ButtonNames.SideThrust);
            if (sideThrust != 0)
            {
				this.ApplySideThrust(sideThrust < 0);
            }

            // Apply break
            if (InputHelper.GetButton(ButtonNames.Break))
            {
                this.ApplyBreak();
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
					this.SetTargetForCurrentWeaponSystem(hit.collider.gameObject.GetComponent<NPCShip>());
				}
				else
				{
					this.SetTargetForCurrentWeaponSystem(null);
				}
			}

			base.Update();
        }
    }
}
