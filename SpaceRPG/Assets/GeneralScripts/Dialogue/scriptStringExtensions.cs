using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.GeneralScripts.Dialogue
{
    public static class ScriptStringExtensions
    {
        /// <summary>
        /// returns true if the given string is a comment
        /// </summary>
        /// <param name="s">Target string</param>
        /// <returns>True if the string is a comment</returns>
        public static bool ShouldBeIgnored(this string s)
        {
            return (string.IsNullOrEmpty(s) || s.StartsWith("#"));
        }

		/// <summary>
		/// Gets the tag of a certain line
		/// </summary>
		/// <param name="s">extension string</param>
		/// <returns>The tag of the string, null if unavailable</returns>
		public static bool TryGetTagNameValue(this string s, out string tagName, out string tagValue)
		{
			var trimmed = s.Trim();
			bool foundStart = false;
			bool nameOver = false;
			StringBuilder nameBuilder = new StringBuilder();
			StringBuilder valueBuilder = new StringBuilder();
			for (int i = 0; i < trimmed.Count(); i++)
			{
				var curChar = trimmed[i];

				// If current char is a starter
				if (curChar == '<')
				{
					// Duplicate, flush and restart name
					if (foundStart)
					{
						foundStart = true;
						nameBuilder = new StringBuilder();
						valueBuilder = new StringBuilder();
					}
					// First starter character found, ready to read tag
					else
					{
						foundStart = true;
						continue;
					}
				}
				// Current char is not a starter, and we have yet to find one. Ignore
				else if (!foundStart)
				{
					continue;
				}

				// Found ending tag
				if (curChar == '>')
				{
					// If we already found a starter, then complete
					if (foundStart)
					{
						tagName = nameBuilder.ToString();
						tagValue = valueBuilder.ToString();
						return true;
					}
				}
				// No starting tag, > is just part of dialog. Check for space
				else if (curChar == ' ')
				{
					// If name is not over, it is now
					if (!nameOver)
					{
						nameOver = true;
					}
					// Part of condition, append
					else
					{
						valueBuilder.Append(curChar);
					}
				}
				// Random character, append to name/value based on bool
				else
				{
					if (!nameOver)
					{
						nameBuilder.Append(curChar);
					}
					else
					{
						valueBuilder.Append(curChar);
					}
				}
			}

			// Searched through whole string and cannot find matching pair, fail
			tagName = null;
			tagValue = null;
			return false;
		}
    }
}
