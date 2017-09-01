//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="FloatExtension.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.CombatScripts.Utility
{
    /// <summary>
    /// Extends the float class
    /// </summary>
    public static class FloatExtension
    {
        /// <summary>
        /// Returns a new float that's between the given range. If it's small than min, return min. 
        /// Else if it's larger than max, then return max. Else, return original value.
        /// </summary>
        /// <param name="f">Original float</param>
        /// <param name="min">Minimal value</param>
        /// <param name="max">Maximum value</param>
        /// <returns>A float within range</returns>
        public static float LimitTo(this float f, float min, float max)
        {
            if (f < min)
            {
                return min;
            }

            if (f > max)
            {
                return max;
            }

            return f;
        }
    }
}
