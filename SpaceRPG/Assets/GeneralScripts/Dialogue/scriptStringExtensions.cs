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
		/// Returns true if the string is a given tag
		/// </summary>
		/// <param name="s">extension string</param>
		/// <param name="tag">target tag</param>
		/// <returns>True if the string is this tag
		/// Example: "<IF Morale > 5>" </returns>
		public static bool IsTag(this string s, TagNames tag)
		{
			return (!string.IsNullOrEmpty(s) && s.StartsWith("<" + tag.ToString()));
		}

		/// <summary>
		/// Gets the tag of a certain line
		/// </summary>
		/// <param name="s">extension string</param>
		/// <returns>The tag of the string, null if unavailable</returns>
		public static string GetTag(this string s)
		{
			var trimmed = s.Trim();
			if (trimmed.First() != '<' || trimmed.Last() != '>')
			{
				return null;
			}

			var split = trimmed.Split(new char[] { ' ', '<', '>' }, StringSplitOptions.RemoveEmptyEntries);
			return split.Count() > 0 ? split[0] : null;
		}


		/// <summary>
		/// Try to get the expression of a tag, null if unavailable
		/// Example: "Morale > 5" from <If Morale > 5 >
		///			 "4" from <JUMP 4>
		/// </summary>
		/// <param name="s">extension string</param>
		/// <returns>thie result string, null if unsuccessful</returns>
		public static string GetExpression(this string s)
		{
			if (!s.IsTag(TagNames.IF))
			{
				return null;
			}

			var trimmed = s.Trim();
			var split = trimmed.Split(' ');
			var withEnd = trimmed.Substring(split[0].Length + 1);
			if (withEnd.Length == 1)
			{
				return null;
			}

			return withEnd.TrimEnd('>');
		}
    }
}
