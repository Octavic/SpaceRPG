using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.GeneralScripts.Dialogue
{
	/// <summary>
	/// Describes a tag in dialog
	/// Ex. <SCENE 5>
	/// </summary>
	public static class DialogTag
	{
		/// <summary>
		/// Try to parse the input
		/// </summary>
		/// <param name="input">input input</param>
		/// <param name="name">name of the tag as output</param>
		/// <param name="value">value of the tag as output (if available)</param>
		/// <returns>True if the input can be parsed</returns>
		public static bool TryParse(string input, out string name, out string value)
		{
			name = string.Empty;
			value = string.Empty;

			// Trim leading and ending white spaces
			input.Trim();

			// Must start and end with '<' and '>'
			if (input.First() != '<' || input.Last() != '>')
			{
				return false;
			}

			input = input.Substring(1, input.Length - 2);

			// Grab name and value that's split by ' '
			var split = input.Split(' ');
			name = split[0];
			if (split.Length > 1)
			{
				value = split[1];
			}

			return true;
		}
	}
}
