//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Conversation.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.GeneralScripts.Dialogue
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	public class Conversation
	{
		/// <summary>
		/// the root scene
		/// </summary>
		public IList<DialogScene> Scenes { get; private set; }

		/// <summary>
		/// Creates a  new instance  of the <see cref="Conversation"/> class 
		/// </summary>
		/// <param name="input">A list of unfiltered strings</param>
		public Conversation(IList<string> input)
		{
			var parsed = new Queue<string>();
			var deleteModeStack = new Stack<bool>();
			for (int i = 0; i < input.Count; i++)
			{
				// Check every line
				var line = input[i];
				//  Ignore comments or white spaces
				if (line.ShouldBeIgnored())
				{
					continue;
				}

				// Try to see if the line contains a tag
				string tagName;
				string tagValue;

				// If there's no tag
				if (!line.TryGetTagNameValue(out tagName, out tagValue))
				{
					// No tag and current mode is deletion, ignore current line
					if (deleteModeStack.Count >0 && deleteModeStack.Peek())
					{
						continue;
					}
				}

				// Line does contain a tag, check 
				if (tagName == TagNames.IF.ToString())
				{
					// Current mode is deletion, <IF> cannot overwrite
					if (deleteModeStack.Count > 0 && deleteModeStack.Peek())
					{
						deleteModeStack.Push(true);
						continue;
					}

					// Check the condition
					deleteModeStack.Push(this.EvaluateExpression(tagValue));
					continue;
				}
				// If the tag is an <ENDIF> tag
				else if (tagName == TagNames.ENDIF.ToString())
				{
					deleteModeStack.Pop();
					continue;
				}

				// Convert accidental tabs to four spaces for player dialogue
				if (line.StartsWith("\t"))
				{
					line = line.Replace("\t", "    ");
				}

				// All looks good, add line
				parsed.Enqueue(line);
			}

			this.Scenes = new List<DialogScene>();
			DialogScene newScene = new DialogScene();
			while (newScene.TryConsume(parsed))
			{
				this.Scenes.Add(newScene);
				newScene = new DialogScene();
			}
		}

		// Evaluate the expression
		private bool EvaluateExpression(string expressioin)
		{
			return false;
		}
	}
}
