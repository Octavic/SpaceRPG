//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="InputHelper.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.Scripts.Utility
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;

    /// <summary>
    /// Adds an extention to the input manager from default Unity
    /// </summary>
    public static class InputHelper
    {
        /// <summary>
        /// Gets the axis from the button name enum
        /// </summary>
        /// <param name="buttonName">What button to be used</param>
        /// <returns>Axis value</returns>
        public static float GetAxis(ButtonNames buttonName)
        {
            return Input.GetAxis(buttonName.ToString());
        }

        /// <summary>
        /// Gets the state of the given button. True if the button is down
        /// </summary>
        /// <param name="buttonName">The name of the button</param>
        /// <returns>True if the button is down</returns>
        public static bool GetButton(ButtonNames buttonName)
        {
            return Input.GetButton(buttonName.ToString());
        }

		/// <summary>
		/// Gets the state of the given button down. True if the button was just pressed
		/// </summary>
		/// <param name="buttonName">The name of the button</param>
		/// <returns>True if the button is just pressed</returns>
		public static bool GetButtonDown(ButtonNames buttonName)
		{
			return Input.GetButtonDown(buttonName.ToString());
		}
    }
}
