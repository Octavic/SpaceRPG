//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="AngleToVector.cs">
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

    public static class AngleToVector
    {
        /// <summary>
        /// Gets the directional vector with the given angle in degrees
        /// </summary>
        /// <param name="angle">Target angle in degrees</param>
        /// <returns>Direction unit vector</returns>
        public static Vector2 FromDegree(float angle)
        {
            return AngleToVector.FromRadiant(angle * Mathf.Deg2Rad);
        }

        /// <summary>
        /// Gets the directional vector with the given angle in radiant
        /// </summary>
        /// <param name="angle">Target angle in radiant</param>
        /// <returns>Direction unit vector</returns>
        public static Vector2 FromRadiant(float angle)
        {
            return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        }
    }
}
