//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="DialogBoxBehaviour.cs">
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
	using UnityEngine.UI;
	using Utility;

	/// <summary>
	/// Behaviour for the dialog box
	/// </summary>
	public class DialogBoxBehaviour : MonoBehaviour
	{
		/// <summary>
		/// The text box
		/// </summary>
		public Text OnScreenText;

		/// <summary>
		/// Prefab for the player's option
		/// </summary>
		public Text PlayerOptionPrefab;

		/// <summary>
		/// The Y difference in the player's options
		/// </summary>
		public float PlayerOptionYDiff;

		/// <summary>
		/// Gets the current instance
		/// </summary>
		public static DialogBoxBehaviour CurrentInstance
		{
			get
			{
				if (DialogBoxBehaviour._currentInstancec == null)
				{
					DialogBoxBehaviour._currentInstancec = GameObjectFinder.FindGameObjectWithTag(Tags.DialogBox).GetComponent<DialogBoxBehaviour>();
				}

				return DialogBoxBehaviour._currentInstancec;
			}
		 }

		/// <summary>
		/// The current instance
		/// </summary>
		private static DialogBoxBehaviour _currentInstancec;

		/// <summary>
		/// The item to be rendered
		/// </summary>
		private IDialogItem _item;

		/// <summary>
		/// Items to stored the changed item
		/// </summary>
		private NpcDialogItem _npcItem;
		private PlayerDialogItem _playerItem;

		/// <summary>
		/// How fast the text scrolls
		/// </summary>
		private float _scrollSpeed;

		/// <summary>
		/// Index for the NPC dialog or the player option
		/// </summary>
		private int _curIndex;

		/// <summary>
		/// The current string to be rendered
		/// </summary>
		private string _curString;

		/// <summary>
		/// The current index
		/// </summary>
		private float _scrolled;

		/// <summary>
		/// If the current dialog item is NPC
		/// </summary>
		private bool _isNPC;

		/// <summary>
		/// The Y position for the current player option
		/// </summary>
		private float _curOptionY;

		/// <summary>
		/// Where to render the text to
		/// </summary>
		private Text _renderTarget;

		/// <summary>
		/// If the text is done scrolling
		/// </summary>
		private bool _doneScrolling;

		/// <summary>
		/// The conversation to call back to
		/// </summary>
		private ConversationBehaviour _conversation;

		/// <summary>
		/// Called when the player clicks on the dialog box
		/// </summary>
		public void OnPlayerClick()
		{
			if (!this._doneScrolling)
			{
				this._scrolled = this._curString.Length;
			}
			else
			{
				this._curIndex++;
				this.RenderLine();
			}
		}

		/// <summary>
		/// Renders a new dialog
		/// </summary>
		/// <param name="item">the item to be rendered</param>
		/// <param name="scrollSpeed">how fast to scroll in characters per second</param>
		public void RenderDialog(IDialogItem item, ConversationBehaviour conversation = null, float scrollSpeed = 30)
		{
			// Get rid of all old player dialog options
			this.gameObject.DestroyAllChildren();

			//  Assign and initialize values
			this._item = item;
			this._scrollSpeed = scrollSpeed;
			this._doneScrolling = false;
			this._scrolled = 0;
			this._curIndex = 0;
			this._curOptionY = 0;
			this._curString = item.GetContent(0);

			if (conversation != null)
			{
				this._conversation = conversation;
			}

			if (item is NpcDialogItem)
			{
				this._isNPC = true;
				this._renderTarget = this.OnScreenText;
			}
			else
			{
				this._isNPC = false;
				this._curIndex = -1;
				this.RenderLine();
			}
		}

		public void OnPlayerSelectOption(int index)
		{
			
		}

		/// <summary>
		/// When the scrolling is finished
		/// </summary>
		public void RenderLine()
		{
			// Reset progress
			this._curString = this._item.GetContent(this._curIndex);
			if (this._curString == null)
			{
				this._conversation.ProgressDialog();
				return;
			}

			this._doneScrolling = false;
			this._scrolled = 0;

			// If this is a player dialog item, create new option
			if (!this._isNPC)
			{
				this._curOptionY += this.PlayerOptionYDiff;
				var newOption = Instantiate(this.PlayerOptionPrefab.gameObject, this.transform);
				newOption.transform.localPosition = new Vector3(0, this._curOptionY);
				this._renderTarget = newOption.GetComponent<Text>();
			}
		}

		/// <summary>
		/// Used for initialization
		/// </summary>
		protected void Start()
		{
			if (DialogBoxBehaviour._currentInstancec == null)
			{
				DialogBoxBehaviour._currentInstancec = this;
			}
		}

		/// <summary>
		/// Called once per frame
		/// </summary>
		protected void Update()
		{
			if (!this._doneScrolling)
			{
				this._scrolled += this._scrollSpeed * Time.deltaTime;
				this._renderTarget.text = this._curString.Substring(0, (int)this._scrolled);

				if (this._scrolled > this._curString.Length)
				{
					this._doneScrolling = true;
					if (!this._isNPC)
					{
						this._curIndex++;
						this.RenderLine();
					}
				}
			}
		}
	}
}
