using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.GeneralScripts.Dialogue
{
    public class NpcDialogItem : IDialogItem
    {
        /// <summary>
        /// Pages of text
        /// </summary>
        public IList<string> Pages { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NpcDialogItem"/> item 
        /// </summary>
        public NpcDialogItem()
        {
            this.Pages = new List<string>();
        }

        /// <summary>
        /// Try to consume the given data and construct a dialog
        /// </summary>
        /// <param name="data">Target data</param>
        /// <returns>True if successful</returns>
        public bool TryConsume(Queue<string> data)
        {
			// While there is data to be read
			while (data.Count > 0)
			{
				var curLine = data.Peek();

				// If we get a return statement or a player dialog, we are done
				if (curLine.StartsWith("    ") || curLine.StartsWith("<RETURN ") || curLine.StartsWith("<JUMP "))
				{
					//  If we read anything, then it's a success
					return this.Pages.Count > 0;
				}

				this.Pages.Add(curLine);
				data.Dequeue();
			}

			// Unexpected end of data
            return false;
        }

		/// <summary>
		/// Get the string at index
		/// </summary>
		/// <param name="index">Target index</param>
		/// <returns>Result string, null if out of range</returns>
		public string GetContent(int index = 0)
		{
			if (index < 0 || index >= this.Pages.Count)
			{
				return null;
			}

			return this.Pages[index];
		}
	}
}
