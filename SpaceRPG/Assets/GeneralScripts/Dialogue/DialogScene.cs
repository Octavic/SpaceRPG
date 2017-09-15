
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
        /// Gets the dialogs
        /// </summary>
        public IList<IDialogItem> Dialogues { get; private set; }

        /// <summary>
        /// Index of the next dialog item type
        /// </summary>
        private bool _isNextItemNpc;

        /// <summary>
        /// Creates a new instance of the <see cref="DialogScene"/> class
        /// </summary>
        public DialogScene()
        {
            this.Dialogues = new List<IDialogItem>();
        }

        /// <summary>
        /// Try to consume the data and construct a scene
        /// </summary>
        /// <param name="data">the input data. the list is modified as data is consumed</param>
        /// <returns>True if the consumed data was parsed with no error.</returns>
        public bool TryConsume(Queue<string> data)
        {
            while (data.Count > 0)
            {
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

            return true;
        }
    }
}
