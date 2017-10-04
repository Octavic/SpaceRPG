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
		public Conversation(Queue<string> input)
		{
			var parsed = new Queue<string>();
			while (input.Count > 0)
			{
				var line = input.Dequeue();
				if (line.ShouldBeIgnored())
				{
					continue;
				}


			}

			this.Scenes = new List<DialogScene>();
			DialogScene newScene = new DialogScene();
			while (newScene.TryConsume(parsed))
			{
				this.Scenes.Add(newScene);
				newScene = new DialogScene();
			}
		}
	}
}
