//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="UIButtonColor.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.Scripts.Settings
{
	using UnityEngine;

	/// <summary>
	/// Colors for normal/disabled button
	/// </summary>
	public static class UIButtonColor
	{
		/// <summary>
		/// Gets the normal color for a button
		/// </summary>
		public static Color NormalColor
		{
			get
			{
				return UIButtonColor._normalColor;
			}
		}

		/// <summary>
		/// Gets the diabled color for a button
		/// </summary>
		public static Color DisabledColor
		{
			get
			{
				return UIButtonColor._disabledColor;
			}
		}

		/// <summary>
		/// The normal color
		/// </summary>
		private static Color _normalColor = new Color(1, 1, 1);

		/// <summary>
		/// The disabled color
		/// </summary>
		private static Color _disabledColor = new Color(0.7f, 0.7f,0.7f);
	}
}
