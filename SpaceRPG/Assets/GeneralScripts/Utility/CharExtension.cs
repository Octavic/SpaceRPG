using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.GeneralScripts.Utility
{
	/// <summary>
	/// The char extension 
	/// </summary>
	public static class CharExtension
	{
		/// <summary>
		/// Values to compare
		/// </summary>
		private const char _zero = '0';
		private const char _nine = '9';
		private const char _lowA = 'a';
		private const char _lowZ = 'z';
		private const char _upA = 'A';
		private const char _upZ = 'Z';

		/// <summary>
		/// Returns true if the character is a digit from 0 to 9
		/// </summary>
		/// <param name="c">extension char</param>
		/// <returns>True if t he character is a digit</returns>
		public static bool IsDigit(this char c)
		{
			return (c >= _zero && c <= _nine);
		}

		/// <summary>
		/// Checks if the character  is a lower case letter
		/// </summary>
		/// <param name="c">Extension char</param>
		/// <returns>True if the char is a lower case letter</returns>
		public static bool IsLowercaseLetter(this char c)
		{
			return (c >= _lowA && c <= _lowZ);
		}

		/// <summary>
		/// Check is the char is an uppercase  letter
		/// </summary>
		/// <param name="c">extension char</param>
		/// <returns>True if the char is an upper case letter</returns>
		public static bool IsUppercaseLetter(this char c)
		{
			return (c >= _upA && c <= _upZ);
		}

		/// <summary>
		/// Returns true if the char is a letter
		/// </summary>
		/// <param name="c">extension char</param>
		/// <returns>true if the char is a letter</returns>
		public static bool IsLetter(this char c)
		{
			return (c.IsLowercaseLetter() || c.IsUppercaseLetter());
		}
	}
}
