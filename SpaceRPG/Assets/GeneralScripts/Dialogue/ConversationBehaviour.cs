//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="GalaxyMapPath.cs">
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

	/// <summary>
	/// The conversation behavior for unity
	/// </summary>
	public class ConversationBehaviour : MonoBehaviour
	{
		/// <summary>
		/// Script for the conversation
		/// </summary>
		public TextAsset Script;

		/// <summary>
		/// The conversation data
		/// </summary>
		private Conversation _conversationData;

		/// <summary>
		/// Used for initialization
		/// </summary>
		protected void Start()
		{
			this._conversationData = new Conversation(new Queue<string>());
		}
	}
}
