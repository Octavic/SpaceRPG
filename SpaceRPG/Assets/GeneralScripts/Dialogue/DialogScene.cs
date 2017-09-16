
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
        }

        /// <summary>
        /// Try to consume the data and construct a scene
        /// </summary>
        /// <param name="data">the input data. the list is modified as data is consumed</param>
        /// <returns>True if the consumed data was parsed with no error.</returns>
        public bool TryConsume(Queue<string> data)
        {
			// Skip leading comment
			if (data.Peek() == null)
			{
				return false;
			}

			// Check if the first line is a valid scene starter
			string tagName;
			string tagValue;
			if (!DialogTag.TryParse(data.Peek(), out tagName, out tagValue) || tagName != _sceneTag)
			{
				return false;
			}

			this.SceneId = int.Parse(tagValue);
			data.Dequeue();

			// Loop through. If the top line isn't {RETURN}, then pass it off and create a new IDialogItem, alternating between NPC and Player
			while (data.Count > 0)
            {
				if (DialogTag.TryParse(data.Peek(), out tagName, out tagValue))
				{
					if (tagName == TagNames.RETURN)
					{
						this.ReturnValue = int.Parse(tagValue);
						return true;
					}

					if (tagName == TagNames.JUMP)
					{
						this.JumpSceneId = int.Parse(tagValue);
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
