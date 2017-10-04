using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assets.GeneralScripts.Dialogue;

namespace SpaceRPGTests_new.Assets.GeneralScripts.Dialogue
{
	[TestClass]
	public class ScriptStringExtensionsTest
	{
		[TestMethod]
		public void ScriptStringExtension_IsTag_True()
		{
			var s = "<IF>";
			Assert.AreEqual(s.IsTag(TagNames.IF), true);
		}

		[TestMethod]
		public void ScriptStringExtension_IsTag_False()
		{
			var s = "<IF>";
			Assert.AreEqual(s.IsTag(TagNames.ENDIF), false);
		}

		[TestMethod]
		public void ScriptStringExtension_GetTag_Valid()
		{
			var s = "<IF>";
			Assert.AreEqual(s.GetTag(), "IF");
		}

		[TestMethod]
		public void ScriptStringExtension_GetTag_Valid_WithExpression()
		{
			var s = "<IF A > 5>";
			Assert.AreEqual(s.GetTag(), "IF");
		}

		[TestMethod]
		public void ScriptStringExtension_GetTag_Invalid()
		{
			var s = "abcde";
			Assert.AreEqual(s.GetTag(), null);
		}


		[TestMethod]
		public void ScriptStringExtension_Expression_IF()
		{
			var s = "<IF A > 5>";
			Assert.AreEqual(s.GetExpression(), "A > 5");
		}

		[TestMethod]
		public void ScriptStringExtension_Expression_NONE()
		{
			var s = "<BAH>";
			Assert.AreEqual(s.GetExpression(), null);
		}
	}
}
