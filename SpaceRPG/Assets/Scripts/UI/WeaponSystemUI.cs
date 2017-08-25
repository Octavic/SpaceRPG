//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="WeaponSystemUI.cs">
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
	using Ships.Weapons;

	/// <summary>
	/// Shows all iniformation related to a weapon system
	/// </summary>
	public class WeaponSystemUI : MonoBehaviour
	{
		/// <summary>
		/// The current player
		/// </summary>
		public PlayerShip CurrentPlayer;

		/// <summary>
		/// The index for the weapon's UI
		/// </summary>
		public int weaponSystemIndex;

		/// <summary>
		/// The icon that means the weapon system is locked onc
		/// </summary>
		public GameObject AimIcon;

		/// <summary>
		/// The bar of reload
		/// </summary>
		public Image ReloadBar;

		/// <summary>
		/// The full length of the reload bar
		/// </summary>
		public float ReloadBarFullLength;

		/// <summary>
		/// The weapon system that this UI is representing
		/// </summary>
		private ShipWeaponSystem _targetWeaponSystem;

		/// <summary>
		/// The default loading weapon bar color
		/// </summary>
		private Color _loadingWeaponBarColor;

		/// <summary>
		/// Called in the beginning
		/// </summary>
		private void Start()
		{
			if (weaponSystemIndex >= CurrentPlayer.WeaponSystems.Count)
			{
				this.gameObject.SetActive(false);
				return;
			}

			this._targetWeaponSystem = CurrentPlayer.WeaponSystems[this.weaponSystemIndex];
			this._loadingWeaponBarColor = this.ReloadBar.color;
		}

		/// <summary>
		/// Called once per frame
		/// </summary>
		private void Update()
		{
			// Set aim lock icon
			this.AimIcon.SetActive(this._targetWeaponSystem.IsLinedUp);

			// Set reload bar
			var reloadPercentage = this._targetWeaponSystem.ReloadPercentage;
			this.ReloadBar.transform.localScale = new Vector3(reloadPercentage, 1);
			this.ReloadBar.transform.localPosition = new Vector3((reloadPercentage * this.ReloadBarFullLength /2) - this.ReloadBarFullLength, 0);
			if (reloadPercentage == 1)
			{
				this.ReloadBar.color = new Color(1, 1, 1);
			}
			else
			{
				this.ReloadBar.color = this._loadingWeaponBarColor;
			}
		}
	}
}
