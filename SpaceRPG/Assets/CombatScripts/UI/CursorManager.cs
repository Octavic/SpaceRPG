//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="CursorManager.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.Scripts.UI
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using UnityEngine;

	/// <summary>
	/// A cursor manager
	/// </summary>
	public class CursorManager : MonoBehaviour
	{
		/// <summary>
		/// Gets the current instance of the cursor manager class
		/// </summary>
		public static CursorManager CurrentInstance{get; private set;}

		/// <summary>
		/// The default cursor
		/// </summary>
		public Texture2D DefaultCursor; 

		/// <summary>
		/// A list of lock on cursors for weapons
		/// </summary>
		public List<Texture2D> LockonCursors;

		/// <summary>
		/// Called in the beginning
		/// </summary>
		protected void Start()
		{
			CursorManager.CurrentInstance = this;
		}
	}
}
