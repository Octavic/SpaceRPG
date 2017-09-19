using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.GeneralScripts.Utility
{
	/// <summary>
	/// Extends the float class
	/// </summary>
	public static class FloatExtensions
	{
		/// <summary>
		/// Lerps the float towards the goal
		/// </summary>
		/// <param name="goal">End goal</param>
		/// <param name="scale">How much to move</param>
		/// <returns>Lerped float value</returns>
		public static float Lerp(this float f, float goal, float scale)
		{
			return ((goal) - f) * scale + f;
		}
	}
}
