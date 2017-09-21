using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.GeneralScripts.Utility
{
	/// <summary>
	/// Adds functionality to the default unity color class
	/// </summary>
	public static class ColorExtensions
	{
		/// <summary>
		/// Lerps the color towards another color
		/// </summary>
		/// <param name="c">color</param>
		/// <param name="target">target color</param>
		/// <param name="scale">how fast to lerp</param>
		/// <returns>Lerped color</returns>
		public static Color Lerp(this Color c, Color target, float scale)
		{
			var newR = c.r.Lerp(target.r, scale);
			var newG = c.g.Lerp(target.g, scale);
			var newB = c.b.Lerp(target.b, scale);
			var newA = c.a.Lerp(target.a, scale);
			return new Color(newR, newG, newB, newA);
		}
	}
}
