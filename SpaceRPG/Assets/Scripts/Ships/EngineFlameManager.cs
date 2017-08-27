//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="EngineFlameManager.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.Scripts.Ships
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using UnityEngine;

	/// <summary>
	/// Manages all engine flame prefabs
	/// </summary>
	public class EngineFlameManager : MonoBehaviour
	{
		/// <summary>
		/// The engine flames for small engines from short to long
		/// </summary>
		public List<GameObject> SmallEngineFlames;

		/// <summary>
		/// The current singleton instance
		/// </summary>
		private static EngineFlameManager _currentInstance;

		/// <summary>
		/// Called in the very beginning
		/// </summary>
		protected void Start()
		{
			EngineFlameManager._currentInstance = this;
		}

		/// <summary>
		/// Gets the flame prefab for the given engine type and length
		/// </summary>
		/// <param name="engineType">The type of engine</param>
		/// <param name="length">The length of the flame</param>
		/// <returns>Prefab for the engine flame, null if no flame</returns>
		public static GameObject GetFlame(EngineTypeEnum engineType, EngineFlameLengthEnum length)
		{
			if (length == EngineFlameLengthEnum.None)
			{
				return null;
			}

			switch (engineType)
			{
				case EngineTypeEnum.Small:
					return EngineFlameManager._currentInstance.SmallEngineFlames[(int)length - 1];
				default:
					return null;
			}
		}
	}
}
