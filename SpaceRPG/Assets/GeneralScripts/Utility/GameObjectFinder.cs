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
		/// Finds a gameobject with the given tag enum
		/// </summary>
		/// <param name="tag">Target tag</param>
		/// <returns>The found game object with the given tag</returns>
		public static GameObject FindGameObjectWithTag(Tags tag)
		{
			return GameObject.FindGameObjectWithTag(tag.ToString());
		}
	}
}
