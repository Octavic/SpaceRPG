using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.GeneralScripts.Dialogue
{
    public class PlayerDialogItem : IDialogItem
    {
        /// <summary>
        /// Class about player dialog option
        /// </summary>
        public class PlayerDialogOption
        {
            /// <summary>
            /// The string of player's option
            /// </summary>
            public string Option { get; set; }

            /// <summary>
            /// Possible branch into a difference scene. -1 if no
            /// </summary>
            public int ChangeSceneId { get; set;}

            /// <summary>
            /// Creates a new instance of the <see cref="PlayerDialogOption"/> class 
            /// </summary>
            /// <param name="option">The player's option</param>
            /// <param name="changeSceneId">Possible scene to switch off to</param>
            public PlayerDialogOption(string option, int changeSceneId = -1)
            {
                this.Option = option;
                this.ChangeSceneId = changeSceneId;
            }
        }

        /// <summary>
        /// A collection of player's options
        /// </summary>
        public IList<PlayerDialogOption> Options;

        /// <summary>
        /// Creates a new instance of the <see cref="PlayerDialogItem"/> class 
        /// </summary>
        public PlayerDialogItem()
        {
            this.Options = new List<PlayerDialogOption>();
        }

        /// <summary>
        /// Try to consume the given data and construct a dialog
        /// </summary>
        /// <param name="data">Target data</param>
        /// <returns>True if successful</returns>
        public bool TryConsume(Queue<string> data)
        {
			// Keep reading as long as there's data
			while (data.Count > 0)
			{
				var curLine = data.Peek();

				// if we  see a return statement or the line is an NPC line
				if (!curLine.IsPlayerLine())
				{
					return this.Options.Count > 0;
				}

				string tagName;
				string tagValue;
				string content = curLine;

				// Check if the current line contains a <JUMMP #> tag
				if (curLine.TryGetTagNameValue(out tagName, out tagValue))
				{
					if (tagName == TagNames.JUMP.ToString())
					{
						content = curLine.Substring(0, curLine.Length - tagName.Length - tagValue.Length - 3);
					}
				}

				this.Options.Add(new PlayerDialogOption(content.Trim(), string.IsNullOrEmpty(tagValue)?-1: int.Parse(tagValue)));
				data.Dequeue();
			}

			// Unexpected end of data reached, false
            return false;
        }

		/// <summary>
		/// Get the string at index
		/// </summary>
		/// <param name="index">Target index</param>
		/// <returns>Result string, null if out of range</returns>
		public string GetContent(int index = 0)
		{
			if (index < 0 || index > this.Options.Count)
			{
				return null;
			}

			return this.Options[index].Option;
		}
	}
}
