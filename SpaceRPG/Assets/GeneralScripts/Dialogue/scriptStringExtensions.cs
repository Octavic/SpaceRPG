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
    }
}
