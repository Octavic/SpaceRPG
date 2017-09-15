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
            return true;
        }
    }
}
