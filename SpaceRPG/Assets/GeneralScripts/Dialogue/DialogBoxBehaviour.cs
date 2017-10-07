//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="DialogBoxBehaviour.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.GeneralScripts.Dialogue
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using UnityEngine;
	using UnityEngine.UI;

	/// <summary>
	/// Behaviour for the dialog box
	/// </summary>
	public class DialogBoxBehaviour : MonoBehaviour
	{
		/// <summary>
		/// The text box
		/// </summary>
		public Text OnScreenText;

		/// <summary>
		/// Prefab for the player's option
		/// </summary>
		public Text PlayerOptionPrefab;

		/// <summary>
		/// The item to be rendered
		/// </summary>
		private IDialogItem _item;

		/// <summary>
		/// How fast the text scrolls
		/// </summary>
		private float _scrollSpeed;

		/// <summary>
		/// Renders the dialog
		/// </summary>
		/// <param name="item">the item to be rendered</param>
		/// <param name="scrollSpeed">how fast to scroll in characters per second</param>
		public void Render(IDialogItem item, float scrollSpeed)
		{
			this._item = item;
			this._scrollSpeed = scrollSpeed;
		}

		/// <summary>
		/// Called once per frame
		/// </summary>
		protected void Update()
		{

		}
	}
}
