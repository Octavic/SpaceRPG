﻿//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="RandomNumberGenerator.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.GeneralScripts.Utility
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Generates a new random number
    /// </summary>
    public static class GlobalRandom
    {
        private static Random _random = new Random();

        /// <summary>
        /// Gets a random integer
        /// </summary>
        /// <returns>A new random integer</returns>
        public static int Next()
        {
            return GlobalRandom._random.Next();
        }

        /// <summary>
        /// Gets a random integer
        /// </summary>
        /// <param name="maxValue">Maximum value</param>
        /// <returns>Gets a random integer between 0 and the given max</returns>
        public static int Next(int maxValue)
        {
            return GlobalRandom._random.Next(maxValue);
        }

        /// <summary>
        /// Gets a random integer
        /// </summary>
        /// <param name="minValue">Minimal value</param>
        /// <param name="maxValue">Maximal value</param>
        /// <returns>Gets a random integer between the given min and max</returns>
        public static int Next(int minValue, int maxValue)
        {
            return GlobalRandom._random.Next(minValue, maxValue);
        }

        /// <summary>
        /// Gets a random number
        /// </summary>
        /// <param name="minValue">Minimal value</param>
        /// <param name="maxValue">Maximal value</param>
        /// <param name="intervals">How many possible results in between</param>
        /// <returns>A random number with the given quiteria</returns>
        public static float Next(float minValue, float maxValue, int intervals = 100)
        {
            float r = GlobalRandom._random.Next(intervals);
            return minValue + (r / intervals * (maxValue - minValue));
        }
    }
}
