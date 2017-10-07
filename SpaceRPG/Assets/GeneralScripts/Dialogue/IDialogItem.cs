
namespace Assets.GeneralScripts.Dialogue
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Describes a dialog item from an NPC or a player
    /// </summary>
    public interface IDialogItem
    {
        /// <summary>
        /// Try to consume the given data and construct a dialog
        /// </summary>
        /// <param name="data">Target data</param>
        /// <returns>True if successful</returns>
        bool TryConsume(Queue<string> data);
    }
}
