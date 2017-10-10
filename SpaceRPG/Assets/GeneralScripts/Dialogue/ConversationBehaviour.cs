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
		private int _curSceneId;

		/// <summary>
		/// Which dialog are we on inside the scene
		/// </summary>
		private int _curDialogIndex;

        /// <summary>
        /// A dictionary of scene Id => scene
        /// </summary>
        private Dictionary<int, DialogScene> _sceneDictionary;

        /// <summary>
        /// Called when a player selects an index
        /// </summary>
        /// <param name="jumpIndex"></param>
        public void OnPlayerSelectOption(int jumpIndex)
        {
            if (jumpIndex == -1)
            {
                return;
            }

            this._curSceneId = jumpIndex;
            DialogBoxBehaviour.CurrentInstance.RenderDialog(this._sceneDictionary[this._curSceneId].Dialogues[0]);
        }

        /// <summary>
        /// Move onto the next dialog
        /// </summary>
        public void ProgressDialog()
		{
			this._curDialogIndex++;

            // If we haven't reached the end of a scene yet, render the next item
            if (this._curDialogIndex < this._sceneDictionary[this._curSceneId].Dialogues.Count)
            {
                DialogBoxBehaviour.CurrentInstance.RenderDialog(
                    this._sceneDictionary[this._curSceneId].Dialogues[this._curDialogIndex]);
            }
            // End of scene reached
            else
            {
                var curScene = this._sceneDictionary[this._curSceneId];

                // Check if the scene is jumping
                DialogScene jumpScene;
                if (this._sceneDictionary.TryGetValue(curScene.JumpSceneId, out jumpScene))
                {
                    this._curSceneId = curScene.JumpSceneId;
                    this._curDialogIndex = 0;
                    DialogBoxBehaviour.CurrentInstance.RenderDialog(jumpScene.Dialogues[0]);
                }
                // The scene is supposed to return
                else if (curScene.ReturnValue != -1)
                {
                    DialogBoxBehaviour.CurrentInstance.Hide();
                }
            }
		}

		/// <summary>
		/// Used for initialization
		/// </summary>
		protected void Start()
		{
			var lines = Script.text.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
			this._conversationData = new Conversation(lines);

            this._sceneDictionary = new Dictionary<int, DialogScene>();
            foreach (var scene in this._conversationData.Scenes)
            {
                this._sceneDictionary[scene.SceneId] = scene;
            }

			this._curSceneId = 1;
			this._curDialogIndex = 0;

			DialogBoxBehaviour.CurrentInstance.RenderDialog(this._sceneDictionary[1].Dialogues[0], this);
		}
	}
}
