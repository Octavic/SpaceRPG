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
			for(int i =0;i<input.Count;i++)
			{
				var line = input[i];
				if (line.ShouldBeIgnored())
				{
					continue;
				}

				if (line.StartsWith("\t"))
				{
					line = line.Replace("\t", "    ");
				}

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
	}
}
