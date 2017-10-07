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
		/// Index for the scene
		/// </summary>
		private int _curSceneIndex;

		/// <summary>
		/// Which dialog are we on inside the scene
		/// </summary>
		private int _curDialogIndex;

		/// <summary>
		/// Move onto the next dialog
		/// </summary>
		public void ProgressDialog()
		{
			this._curDialogIndex++;
			if (this._curDialogIndex < this._conversationData.Scenes[this._curSceneIndex].Dialogues.Count)
			{
				DialogBoxBehaviour.CurrentInstance.RenderDialog(
					this._conversationData.Scenes[this._curSceneIndex].Dialogues[this._curDialogIndex]);
			}
		}

		/// <summary>
		/// Used for initialization
		/// </summary>
		protected void Start()
		{
			var lines = Script.text.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
			this._conversationData = new Conversation(lines);

			this._curSceneIndex = 0;
			this._curDialogIndex = 0;

			DialogBoxBehaviour.CurrentInstance.RenderDialog(this._conversationData.Scenes[0].Dialogues[0], this);
		}
	}
}
