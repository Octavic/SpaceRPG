//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ToggleButton.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.CombatScripts.UI
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using UnityEngine;
	using UnityEngine.UI;
	using Settings;

	/// <summary>
	/// The toggle button that changes
	/// </summary>
	public class ToggleButton : Image
	{
		/// <summary>
		/// The current state of the button
		/// </summary>
		private bool _isNormal = true;

		/// <summary>
		/// Toggles the button's state
		/// </summary>
		public void Toggle()
		{
			this.SetState(!this._isNormal);
		}

		/// <summary>
		/// Sets the button's state
		/// </summary>
		/// <param name="setToNormal">The new state</param>
		public void SetState(bool setToNormal)
		{
			var newColor = setToNormal ? UIButtonColor.NormalColor : UIButtonColor.DisabledColor;
			this.color = newColor;
			this._isNormal = setToNormal;
		}
	}
}
