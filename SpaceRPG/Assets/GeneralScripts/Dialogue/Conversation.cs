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
                line.TryGetTagNameValue(out tagName, out tagValue);
                bool isDeleteMode = deleteModeStack.Count == 0 || !deleteModeStack.Peek();

                // If the current tag is an <IF> statement, push it onto the stack
                if (tagName == TagNames.IF.ToString())
                {
                    deleteModeStack.Push(isDeleteMode || !this.EvaluateExpression(tagValue));
                    continue;
                }
                // If the current tag is an <END IF> statement, pop the stack
                else if (tagName == TagNames.ENDIF.ToString())
                {
                    deleteModeStack.Pop();
                    continue;
                }
                // Random tag, not important here. treat it as a normal item

                // If the current mode is not delete mode, just append
                if (isDeleteMode)
                {
                    // Convert accidental tabs to four spaces for player dialog
                    if (line.StartsWith("\t"))
                    {
                        line = line.Replace("\t", "    ");
                    }

                    // All looks good, add line
                    parsed.Enqueue(line);
                    continue;
                }

                // Delete mode, check if
				
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
