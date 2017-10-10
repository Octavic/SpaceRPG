//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="DialogScene.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.GeneralScripts.Dialogue
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

	/// <summary>
	/// Describes a scene in the dialog
	/// </summary>
	public class DialogScene
	{
		/// <summary>
		/// Id of the scene
		/// </summary>
		public int SceneId;

		/// <summary>
		/// The default return value when dialog runs out
		/// </summary>
		public int ReturnValue;

		/// <summary>
		/// Possible scene to jump to
		/// </summary>
		public int JumpSceneId;

		/// <summary>
		/// Gets the dialogs
		/// </summary>
		public IList<IDialogItem> Dialogues { get; private set; }

		/// <summary>
		/// Index of the next dialog item type
		/// </summary>
		private bool _isNextItemNpc;

		/// <summary>
		/// arrow tags for trimming
		/// </summary>
		private static readonly char[] _arrowTags = { '<', '>' };

		/// <summary>
		/// The scene tag
		/// </summary>
		private const string _sceneTag = "SCENE";

		/// <summary>
		/// The return tag
		/// </summary>
		private const string _returnTag = "RETURN";

        /// <summary>
        /// Creates a new instance of the <see cref="DialogScene"/> class
        /// </summary>
        public DialogScene()
        {
            this.Dialogues = new List<IDialogItem>();
			this._isNextItemNpc = true;
            this.JumpSceneId = -1;
            this.ReturnValue = -1;
        }

        /// <summary>
        /// Try to consume the data and construct a scene
        /// </summary>
        /// <param name="data">the input data. the list is modified as data is consumed</param>
        /// <returns>True if the consumed data was parsed with no error.</returns>
        public bool TryConsume(Queue<string> data)
        {
			// If reached end of stream
			if (data.Count == 0)
			{
				return false;
			}

			// Check if the first line is a valid scene starter
			string firstTagName;
			string firstTagValue;
			if (!data.Peek().TryGetTagNameValue(out firstTagName, out firstTagValue) || firstTagName != _sceneTag)
			{
				return false;
			}

			this.SceneId = int.Parse(firstTagValue);
			data.Dequeue();

			// Loop through. If the top line isn't <RETURN>, then pass it off and create a new IDialogItem, alternating between NPC and Player
			while (data.Count > 0)
            {
				string curTagName;
				string curTagValue;
				if (!data.Peek().IsPlayerLine() &&  data.Peek().TryGetTagNameValue(out curTagName, out curTagValue))
				{
					if (curTagName == TagNames.RETURN.ToString())
					{
						this.ReturnValue = int.Parse(curTagValue);
						data.Dequeue();
						return true;
					}
					if (curTagName == TagNames.JUMP.ToString())
					{
						this.JumpSceneId = int.Parse(curTagValue);
						data.Dequeue();
						return true;
					}
				}

				IDialogItem newItem;

                if (this._isNextItemNpc)
                {
                    this._isNextItemNpc = false;
                    newItem = new NpcDialogItem();
                }
                else
                {
                    this._isNextItemNpc = true;
                    newItem = new PlayerDialogItem();
                }

                if (!newItem.TryConsume(data))
                {
                    return false;
                }

                this.Dialogues.Add(newItem);
            }

			// End of the data reached without finding return, error
			return false;
        }
    }
}
