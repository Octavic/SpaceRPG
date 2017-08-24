//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Vector2Extension.cs">
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

    public static class Vector2Extension
    {
        /// <summary>
        /// Gets an angle from the given vector
        /// </summary>
        /// <param name="v">Vector</param>
        /// <returns>The angle </returns>
        public static float ToAngleDegree(this Vector2 v)
        {
            return v.ToAngleRadiant() * Mathf.Rad2Deg;
        }

        /// <summary>
        /// Gets an angle from the given vector
        /// </summary>
        /// <param name="v">Vector</param>
        /// <returns>The angle </returns>
        public static float ToAngleRadiant(this Vector2 v)
        {
            return Mathf.Atan2(v.y, v.x);
        }
    }
}
