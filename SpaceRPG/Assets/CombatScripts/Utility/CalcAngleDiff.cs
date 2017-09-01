//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="CalcAngleDiff.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.CombatScripts.Utility
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;

    /// <summary>
    /// Calculates the minimal amount of turn required
    /// </summary>
    public static class CalcAngleDiff
    {
        /// <summary>
        /// Calculates the angle different from source to target (Both in degrees)
        /// </summary>
        /// <param name="source">Source angle</param>
        /// <param name="target">Target angle</param>
        /// <returns>Angle difference in degrees</returns>
        public static float InDegrees(float source, float target)
        {
            var diff = target - source;
            diff %= 360;
            if (diff > 180)
            {
                return diff - 360;
            }

            if (diff < -180)
            {
                return diff + 360;
            }

            return diff;
        }

        /// <summary>
        /// Calculates the angle different from source to target (Both in radiant)
        /// </summary>
        /// <param name="source">Source angle</param>
        /// <param name="target">Target angle</param>
        /// <returns>Angle difference in radiant</returns>
        public static float InRadiant(float source, float target)
        {
            return CalcAngleDiff.InDegrees(source * Mathf.Rad2Deg, target * Mathf.Rad2Deg) * Mathf.Deg2Rad;
        }
    }
}
