//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="GameObjectExtension.cs">
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
	/// Extends the GameObject class from Unity
	/// </summary>
	public static class GameObjectExtension
	{
		public static void DestroyAllChildren(this GameObject obj)
		{
			for (int i = obj.transform.childCount - 1; i >= 0; i--)
			{
				GameObject.Destroy(obj.transform.GetChild(i).gameObject);
			}
		}

	}
}
