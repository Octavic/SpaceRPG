//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="GameObjectFinder.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.GeneralScripts.Utility
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using UnityEngine;

	/// <summary>
	/// Extends the game object class
	/// </summary>
	public static class GameObjectFinder
	{
        /// <summary>
        /// A hash of found items
        /// </summary>
        private static IDictionary<Tags, GameObject>_foundItemHash = new Dictionary<Tags, GameObject>();

		/// <summary>
		/// Finds a gameobject with the given tag enum
		/// </summary>
		/// <param name="tag">Target tag</param>
		/// <returns>The found game object with the given tag</returns>
		public static GameObject FindGameObjectWithTag(Tags tag)
		{
            GameObject result;
            
            // See if the object is found in the hash. If so and it's not null, use it
            if (!_foundItemHash.TryGetValue(tag, out result) || result == null)
            {
                // Hash not found, find it and add it to hash
                result = GameObject.FindGameObjectWithTag(tag.ToString());
                _foundItemHash[tag] = result;
            }

            return result;
		}
	}
}
