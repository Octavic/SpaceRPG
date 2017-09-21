using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assets.GeneralScripts.Utility;

namespace SpaceRPGTests_new.Assets.GeneralScripts.Utility
{
	[TestClass]
	public class CharExtensionTest
	{
		[TestMethod]
		public void CharExtension_IsDigitTrue()
		{
			var c = '0';
			Assert.AreEqual(c.IsDigit(), true);
		}

		[TestMethod]
		public void CharExtension_IsDigitFalse()
		{
			var c = 'a';
			Assert.AreEqual(c.IsDigit(), false);
		}

		[TestMethod]
		public void CharExtension_IsLowerTrue()
		{
			var c = 'a';
			Assert.AreEqual(c.IsLowercaseLetter(), true);
		}

		[TestMethod]
		public void CharExtension_IsLowerFalse()
		{
			var c = 'A';
			Assert.AreEqual(c.IsLowercaseLetter(), false);
		}

		[TestMethod]
		public void CharExtension_IsUpperrTrue()
		{
			var c = 'A';
			Assert.AreEqual(c.IsUppercaseLetter(), true);
		}

		[TestMethod]
		public void CharExtension_IsUpperrFalse()
		{
			var c = 'a';
			Assert.AreEqual(c.IsUppercaseLetter(), false);
		}

		[TestMethod]
		public void CharExtension_IsLetterTrue()
		{
			var c = 'a';
			Assert.AreEqual(c.IsLetter(), true);
			c = 'A';
			Assert.AreEqual(c.IsLetter(), true);
		}

		[TestMethod]
		public void CharExtension_IsLetterFalse()
		{
			var c = '1';
			Assert.AreEqual(c.IsLetter(), false);
		}
	}
}
